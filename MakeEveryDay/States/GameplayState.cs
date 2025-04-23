﻿using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShapeUtils;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MakeEveryDay.States
{
    internal class GameplayState : State
    {
        internal static SpriteFont defaultText = default;

        internal static Texture2D garbageTexture;

        private static Vector2 defaultPlayerPosition = new Vector2(100, 100);

        private static Texture2D defaultImage;

        private static Rectangle spawnableArea = new Rectangle(100, 50, 400, 250);

        private static float lineSpeed;

        internal static List<List<Block>> allBlocks = new List<List<Block>>();
        private List<Block> activeBlocks;
        private List<BlockGroup> activeGroups;
        private List<BlockType> theLine;
        private List<bool> checkingPlacement;

        //private Block testBlock1;
        private Player player;
        private StatusBar[] statusBars;
        private GameObject[] statIcons;

        private bool debug;
        private float totalWidth;
        private float spawnTimer;

        private bool gameOver;

        private int score;

        private static int targetWidth;

        private static bool grabbingBlock;

        private float positionToCheckStats;

        private float garbageCooldown;
        private float garbageCooldownMax;

        private GameObject garbageBin;

        //private List<BlockGroup> blockGroups;

        private BlockType LastBlockOnLine
        {
            get { return theLine[theLine.Count - 1]; }
        }

        public static int TargetWidth => targetWidth;
        public static bool GrabbingBlock { get => grabbingBlock; set => grabbingBlock = value; }

        public GameplayState(bool debug)
        {
            this.debug = debug;
        }

        public override void Enter()
        {
            gameOver = false;

            positionToCheckStats = 80;

            score = 0;

            lineSpeed = 8f;

            Game1.Width = 1000;
            targetWidth = 1000;

            theLine = new List<BlockType>();
            activeBlocks = new List<Block>();
            
            activeGroups = new List<BlockGroup>();
            statusBars = new StatusBar[4];

            SoundsUtils.InitializeBackgroundMusic();

            // Reading in blocks
            allBlocks.Clear();
            StreamReader reader = null;
            try
            {
                reader = new("Content\\gameBlocks.blocks");
                while (!reader.EndOfStream)
                {
                    string[] blockData = reader.ReadLine().Split('|');

                    // Color needs to be read seperately
                    Color color = Color.White;
                    color.PackedValue = (uint)int.Parse(blockData[2]);

                    // Splitting line into data that fits the block's constructor
                    allBlocks.Add(new List<Block> {
                        new Block(
                        blockData[0],
                        Vector2.Zero,
                        int.Parse(blockData[1]),
                        color,
                        int.Parse(blockData[3]),
                        int.Parse(blockData[4]),
                        int.Parse(blockData[5]),
                        int.Parse(blockData[6]),
                        new CustomRange(int.Parse(blockData[7].Split(',')[0]), int.Parse(blockData[7].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[8].Split(',')[0]), int.Parse(blockData[8].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[9].Split(',')[0]), int.Parse(blockData[9].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[10].Split(',')[0]), int.Parse(blockData[10].Split(',')[1])),
                        new CustomRange(int.Parse(blockData[11].Split(',')[0]), int.Parse(blockData[11].Split(',')[1])),
                        int.Parse(blockData[12]),
                        Block.ReadDependencyString(blockData[13])
                    ) });
                }
                checkingPlacement = new List<bool>(allBlocks.Count);
                for (int i = 0; i < allBlocks.Count; i++)
                    checkingPlacement.Add(false);
            }
            catch
            {
                throw new Exception("gameBlocks.blocks couldn't be read!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            theLine.Add(new Block(
                "start",
                new Vector2(0, Game1.BridgePosition),
                500));
            totalWidth -= 500;
            targetWidth -= (int)(500 / 15f);
            player = new Player();

            // Create status bars
            statusBars[0] = new StatusBar(new Vector2(7, 0), new Point(200, 80), player.Health, Color.Red);
            statusBars[1] = new StatusBar(new Vector2(7, 80), new Point(200, 80), 0, Color.Yellow);
            statusBars[2] = new StatusBar(new Vector2(7, 160), new Point(200, 80), 0, Color.Blue);
            statusBars[3] = new StatusBar(new Vector2(7, 240), new Point(200, 80), 0, Color.Green);

            // Icons to display next to status bars
            statIcons = new GameObject[4];
            statIcons[0] = new GameObject(Block.statIcons[0], new Rectangle(statusBars[0].Position.ToPoint()
                + new Point(statusBars[0].Width + 7, statusBars[0].Height / 4), new Point(statusBars[0].Height / 2, statusBars[0].Height / 2)));
            statIcons[1] = new GameObject(Block.statIcons[1], new Rectangle(statusBars[1].Position.ToPoint()
                + new Point(statusBars[1].Width + 7, statusBars[1].Height / 4), new Point(statusBars[1].Height / 2, statusBars[1].Height / 2)));
            statIcons[2] = new GameObject(Block.statIcons[2], new Rectangle(statusBars[2].Position.ToPoint()
                + new Point(statusBars[2].Width + 7, statusBars[2].Height / 4), new Point(statusBars[2].Height / 2, statusBars[2].Height / 2)));
            statIcons[3] = new GameObject(Block.statIcons[3], new Rectangle(statusBars[3].Position.ToPoint()
                + new Point(statusBars[3].Width + 7, statusBars[3].Height / 4), new Point(statusBars[3].Height / 2, statusBars[3].Height / 2)));

            garbageBin = new(garbageTexture, new Rectangle(Point.Zero, new Point(100, 100)));

            // 10000 milliseconds == 10 seconds
            garbageCooldownMax = 10000;
            garbageCooldown = 0;
            spawnTimer = 0;
        }

        public override void Exit()
        {
            SoundsUtils.backgroundMusic.Stop();
            base.Exit();
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            float scaleFactor = Game1.Width / Game1.ScreenSize.X;
            spawnableArea = new Rectangle((int)(scaleFactor * statusBars[0].Width),
                (int)(100 - .75f * (Game1.Width - Game1.ScreenSize.X) * (Game1.ScreenSize.Y / Game1.ScreenSize.X)),
                Game1.Width - (int)(2 * scaleFactor * statusBars[0].Width) - 400, (int)(Game1.ScreenSize.Y / 2.5f * scaleFactor));

            if (MouseUtils.CurrentKBState.IsKeyDown(Keys.Tab))
            {
                return new MenuState();
            }

            if (debug)
            {
                if (MouseUtils.KeyJustPressed(Keys.Up))
                {
                    player.Health += 5;
                    player.Wealth += 5;
                    player.Happiness += 5;
                    player.Education += 5;

                    player.Health = Math.Clamp(player.Health, 0, 100);
                    player.Happiness = Math.Clamp(player.Happiness, 0, 100);
                    player.Education = Math.Clamp(player.Education, 0, 100);
                    player.Wealth = Math.Clamp(player.Wealth, 0, 100);

                    statusBars[0].CurrentValue = player.Health;
                    statusBars[1].CurrentValue = player.Education;
                    statusBars[2].CurrentValue = player.Happiness;
                    statusBars[3].CurrentValue = player.Wealth;
                }

                if (MouseUtils.CurrentKBState.IsKeyDown(Keys.Right))
                    targetWidth = Math.Clamp(targetWidth + 25, 100, 3500);

                if (MouseUtils.CurrentKBState.IsKeyDown(Keys.Left))
                    targetWidth = Math.Clamp(targetWidth - 25, 100, 3500);

                if (MouseUtils.KeyJustPressed(Keys.Enter))
                {
                    TrySpawnBlock();
                }
                if (MouseUtils.KeyJustPressed(Keys.B))
                {
                    return new GroupMakerState(player);
                }

                Vector2 adjustVector = new Vector2(-lineSpeed, 0);
                for (int i = 0; i < theLine.Count; i++)
                {
                    if (MouseUtils.CurrentKBState.IsKeyDown(Keys.A))
                    {
                        theLine[i].Position += adjustVector;
                    }
                }
            }
            else if (!gameOver)
            {
                // Automatic spawning of blocks / bridge movement
                spawnTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (spawnTimer >= (1500 - (50 * player.Age + 1)))
                {
                    spawnTimer = 0;
                    TrySpawnBlock();
                }
            }
            if (player.Animation.Texture != Player.Fall && !debug)
            {
                lineSpeed = 8 + player.Age / 2.5f;
                Vector2 adjustVector = new Vector2((-lineSpeed * gameTime.ElapsedGameTime.Milliseconds) / 250 * (LastBlockOnLine.Right > Game1.Width ? 25 : 1), 0);
                for (int i = 0; i < theLine.Count; i++)
                {
                    theLine[i].Position += adjustVector;
                }
            }
            if (!gameOver || debug)
            {
                if (activeBlocks.Count > 0)
                {
                    for (int i = activeBlocks.Count - 1; i >= 0; i--)
                    {
                        activeBlocks[i].Update(gameTime);
                        if (activeBlocks[i].IsClicked)
                        {
                            Block frontBlock = activeBlocks[i];
                            activeBlocks.Add(frontBlock);
                            activeBlocks.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < activeBlocks.Count; i++)
                    {
                        if (LastBlockOnLine.Right + 100 * scaleFactor > activeBlocks[i].Left &&
                            LastBlockOnLine.Right <= Game1.Width &&
                            LastBlockOnLine.Top - LastBlockOnLine.Height < activeBlocks[i].Top &&
                            activeBlocks[i].WasJustHeld)
                        {
                            for (int j = 0; j < allBlocks.Count; j++)
                            {
                                if (activeBlocks[i].EqualsOther(allBlocks[j]))
                                {
                                    checkingPlacement[j] = true;
                                    break;
                                }
                            }
                            theLine.Add(activeBlocks[i]);
                            activeBlocks.RemoveAt(i);
                            theLine[theLine.Count - 1].Position = new Vector2(theLine[theLine.Count - 2].Right, theLine[theLine.Count - 2].Top);
                            grabbingBlock = false;
                            i--;
                        }
                        else if (activeBlocks[i].ScaledRectangle.X + activeBlocks[i].ScaledRectangle.Width >= garbageBin.ScaledRectangle.X
                            && activeBlocks[i].ScaledRectangle.Y <= garbageBin.ScaledRectangle.Y + garbageBin.ScaledRectangle.Height
                            && activeBlocks[i].WasJustHeld
                            && garbageCooldown == 0)
                        {
                            activeBlocks.RemoveAt(i);
                            grabbingBlock = false;
                            garbageCooldown = 1;
                            i--;
                        }
                    }
                }

                if (Game1.Width < targetWidth)
                    Game1.Width = Math.Clamp(Game1.Width + 1, 100, 3500);
                else if (Game1.Width > targetWidth)
                    Game1.Width = Math.Clamp(Game1.Width - 1, 100, 3500);

                positionToCheckStats = 80 + (Game1.Width - 1000) * .0675f;

                garbageBin.Size = new((int)(100 * scaleFactor), (int)(100 * scaleFactor));
                garbageBin.Position = new Vector2(Game1.Width - garbageBin.Size.X * 1.5f,
                    garbageBin.Size.Y * 1.25f - .75f * (Game1.Width - Game1.ScreenSize.X) * (Game1.ScreenSize.Y / Game1.ScreenSize.X)
                    );

                if (garbageCooldown != 0)
                {
                    garbageCooldown += gameTime.ElapsedGameTime.Milliseconds;
                    if (garbageCooldown >= garbageCooldownMax)
                        garbageCooldown = 0;
                }
            }
            // When game over occurs, wait for animation to play before going to game over screen
            if (gameOver)
                if (player.Animation.Ended)
                    return new GameOverState(score);

            UpdatePlayer(gameTime);

            foreach (StatusBar bar in statusBars)
            {
                bar.Update();
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Paper, new Rectangle(Point.Zero, Game1.ScreenSize.ToPoint()), Color.White);

            player.Draw(sb);

            for (int i = 0; i < theLine.Count; i++)
            {
                theLine[i].Draw(sb);
            }

            for (int i = 0; i < activeBlocks.Count; i++)
            {
                activeBlocks[i].Draw(sb);
            }

            foreach (StatusBar bar in statusBars)
            {
                bar.DrawUnscaled(sb);
            }
            foreach(BlockGroup group in activeGroups)
            {
                group.Draw(sb);
            }

            foreach (GameObject icon in statIcons)
            {
                icon.DrawUnscaled(sb);
            }

            Color binColor = new Color(1, 1, 1, (garbageCooldown == 0) ? 1 : garbageCooldown / garbageCooldownMax / 1.5f);
            garbageBin.Draw(sb, binColor, 0, Vector2.Zero, SpriteEffects.None, 1);

            if (garbageCooldown != 0)
                sb.DrawString(defaultText, ((garbageCooldownMax - garbageCooldown) / 1000).ToString().Substring(0, 3), new Vector2(Game1.ScreenSize.X - 120, 150), Color.Black);

            sb.DrawString(defaultText, "Age: " + player.Age.ToString(), statusBars[statusBars.Length - 1].Position + new Vector2(6, statusBars[statusBars.Length - 1].Height * 1.2f), Color.Black);
            sb.DrawString(defaultText, "Score: " + score.ToString(), new Vector2(Game1.ScreenSize.X - 50 - defaultText.MeasureString("Score: " + score.ToString()).X, 50), Color.Black);

            if (debug)
            {
                sb.DrawString(defaultText, "Width: " + Game1.Width.ToString(), statusBars[statusBars.Length - 1].Position + new Vector2(6, statusBars[statusBars.Length - 1].Height * 1.8f), Color.Black);
                sb.DrawString(defaultText, "Target Width: " + TargetWidth.ToString(), statusBars[statusBars.Length - 1].Position + new Vector2(6, statusBars[statusBars.Length - 1].Height * 2.2f), Color.Black);
            }
        }

        /// <summary>
        /// Updates the player's stats based on the block beneath their feet
        /// </summary>
        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (Block block in theLine)
            {
                if (block.Left <= positionToCheckStats && block.Checked == false) //This is the block currently being stood on
                {
                    //Edit stats
                    player.Health += block.HealthMod;
                    player.Happiness += block.HappyMod;
                    player.Education += block.EducationMod;
                    player.Wealth += block.WealthMod;

                    player.Health = Math.Clamp(player.Health, 0, 100);
                    player.Happiness = Math.Clamp(player.Happiness, 0, 100);
                    player.Education = Math.Clamp(player.Education, 0, 100);
                    player.Wealth = Math.Clamp(player.Wealth, 0, 100);

                    //Set the status bars
                    statusBars[0].CurrentValue = player.Health;
                    statusBars[1].CurrentValue = player.Education;
                    statusBars[2].CurrentValue = player.Happiness;
                    statusBars[3].CurrentValue = player.Wealth;

                    // Aging - MODIFY VALUES HERE TO INCREASE OR DECREASE RATE OF AGING AND WIDTH
                    totalWidth += block.Width;
                    player.Age = (int)(totalWidth / 750);
                    targetWidth = Math.Clamp(targetWidth + (int)(block.Width / 15f), 100, 3500);

                    block.Checked = true; //Ensures the block isn't checked again

                    UpdateScore();
                    break;
                }
            }

            if (player.Animation == Player.Running)
            {
                if (LastBlockOnLine.Right <= positionToCheckStats) //Goes off if there is no block under the player
                {
                    player.StartFalling();
                    if (!debug)
                        gameOver = true;
                }

                if (player.Health <= 0 || player.Happiness <= 0 || player.Education <= 0 || player.Wealth <= 0) //Kills the player if their stats get too low. Can be updated to include more values
                {
                    player.Die();
                    if(!debug)
                        gameOver = true;
                }
            }
        }

        /// <summary>
        /// Returns a new block (or blocks) depending on the player's stats.
        /// </summary>
        /// <returns>The new block (or blocks) if possible, otherwise returns an empty block list.</returns>
        private List<Block> GenerateNewBlocks()
        {
            List<List<Block>> potentialBlocks = new List<List<Block>>(allBlocks.Count);
            foreach (List<Block> blockList in allBlocks)
            {
                bool success = true;
                foreach (Block block in blockList)
                {
                    foreach (int index in block.Dependencies)
                    {
                        if (index > -1)
                        {
                            if (!checkingPlacement[index])
                            {
                                success = false;
                                break;
                            }
                        }
                    }
                    if (!block.CheckAgainstPlayerStats(player) || !(block.NumSpawns == -1 || block.NumSpawns > 0))
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                    potentialBlocks.Add(blockList);
            }
            if (potentialBlocks.Count > 0)
            {
                Random rand = new();
                int index = rand.Next(0, potentialBlocks.Count);
                List<Block> newBlockList = new List<Block>(potentialBlocks[index].Count);
                foreach (Block block in potentialBlocks[index])
                {
                    if (block.NumSpawns > 0)
                        block.NumSpawns--;
                    newBlockList.Add(Block.CloneBlock(block));
                }
                return newBlockList;
            }
            return new List<Block>(1);
        }

        /// <summary>
        /// Holds all logic for attempting to spawn a block.
        /// </summary>
        private void TrySpawnBlock()
        {
            // Only spawn under a certain amount of blocks on screen, excluding the bridge
            if (activeBlocks.Count < 10 + player.Age / 5 || debug)
            {
                Random rand = new Random();

                // Spawns a new block (or group of blocks) depending on player stats, gives random position
                List<Block> newBlocks = GenerateNewBlocks();
                if (newBlocks.Count > 0)
                {
                    float groupWidth = 0;
                    for (int i = 0; i < newBlocks.Count; i++)
                    {
                        activeBlocks.Add(newBlocks[i]);
                        if (i == 0)
                            newBlocks[0].Position = new Vector2(rand.Next(spawnableArea.Left, spawnableArea.Right), rand.Next(spawnableArea.Top, spawnableArea.Bottom));
                        else
                            newBlocks[i].Position = newBlocks[0].Position + new Vector2(groupWidth, 0);
                        groupWidth += newBlocks[i].Width;
                        newBlocks[i].IsClicked = false;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the game's score to match current stats.
        /// </summary>
        private void UpdateScore()
        {
            score = (int)(totalWidth / 1000 * (player.Health * 5 + player.Happiness * 3 + player.Education * 3 + player.Wealth * 3));
        }
    }
}
