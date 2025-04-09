using Accessibility;
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
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MakeEveryDay.States
{
    internal class GameplayState : State
    {
        internal static SpriteFont defaultText = default;

        private static Vector2 defaultPlayerPosition = new Vector2(100, 100);

        private static Texture2D defaultImage;

        private static Rectangle spawnableArea = new Rectangle(100, 50, 400, 250);

        private static float lineSpeed;

        private List<List<Block>> allBlocks;
        private List<Block> activeBlocks;
        private List<Block> theLine;

        private Block testBlock1;
        private Player player;
        private StatusBar[] statusBars;

        private List<Block> loadedBlocks;

        private bool debug;
        private float totalWidth;
        private float spawnTimer;

        private bool gameOver;

        private int score;

        private static int targetWidth;

        private static bool grabbingBlock;

        private float positionToCheckStats;

        private Block LastBlockOnLine
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

            lineSpeed = 6.5f;

            Game1.Width = 1000;
            targetWidth = 1000;

            theLine = new List<Block>();
            activeBlocks = new List<Block>();
            allBlocks = new List<List<Block>>();
            statusBars = new StatusBar[4];

            // Reading in blocks
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
                        new CustomRange(int.Parse(blockData[11].Split(',')[0]), int.Parse(blockData[11].Split(',')[1]))
                    ) });
                }
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

            //Create status bars
            statusBars[0] = new StatusBar(new Vector2(25, 0), new Point(200, 80), player.Health, Color.Red);
            statusBars[1] = new StatusBar(new Vector2(25, 80), new Point(200, 80), 0, Color.Yellow);
            statusBars[2] = new StatusBar(new Vector2(25, 160), new Point(200, 80), 0, Color.Blue);
            statusBars[3] = new StatusBar(new Vector2(25, 240), new Point(200, 80), 0, Color.Green);

            spawnTimer = 0;
        }

        public override void Exit()
        {
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
            if (player.Animation.Texture != Player.Fall)
            {
                Vector2 adjustVector = new Vector2(-lineSpeed * gameTime.ElapsedGameTime.Milliseconds / (150 - (player.Age + 1) * 5), 0);
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
                        if (activeBlocks[i].IsClicked && activeBlocks.Count > 1)
                        {
                            Block frontBlock = activeBlocks[i];
                            activeBlocks.RemoveAt(i);
                            activeBlocks.Add(frontBlock);
                            break;
                        }
                    for (int i = 0; i < activeBlocks.Count; i++)
                    {
                        activeBlocks[i].Update(gameTime);

                        if (LastBlockOnLine.Right > activeBlocks[i].Left &&
                            LastBlockOnLine.Top - LastBlockOnLine.Height < activeBlocks[i].Top &&
                            LastBlockOnLine.Bottom + LastBlockOnLine.Height > activeBlocks[i].Bottom)
                        {
                            theLine.Add(activeBlocks[i]);
                            activeBlocks.RemoveAt(i);
                            theLine[theLine.Count - 1].Position = new Vector2(theLine[theLine.Count - 2].Right, theLine[theLine.Count - 2].Top);
                            grabbingBlock = false;
                            i--;
                        }
                    }
                }

                if (Game1.Width < targetWidth)
                    Game1.Width = Math.Clamp(Game1.Width + 1, 100, 3500);
                else if (Game1.Width > targetWidth)
                    Game1.Width = Math.Clamp(Game1.Width - 1, 100, 3500);

                positionToCheckStats = 80 + (Game1.Width - 1000) * .0675f;
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
                    gameOver = true;
                }

                if (player.Health <= 0 || player.Happiness <= 0 || player.Education <= 0 || player.Wealth <= 0) //Kills the player if their stats get too low. Can be updated to include more values
                {
                    player.Die();
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
                    if (!(block.HealthRange.IsInRange(player.Health)
                        && block.WealthRange.IsInRange(player.Wealth)
                        && block.HappyRange.IsInRange(player.Happiness)
                        && block.EducationRange.IsInRange(player.Education)
                        && block.AgeRange.IsInRange(player.Age)
                        ))
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
                    newBlockList.Add(Block.CloneBlock(block));
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
            if (activeBlocks.Count < 15 + player.Age / 5 || debug)
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
