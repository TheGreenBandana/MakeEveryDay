using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class GameplayState:GameState
    {
        internal static SpriteFont defaultText = default;

        private static Vector2 defaultPlayerPosition = new Vector2(100, 100);

        private static Texture2D defaultImage;

        private static Rectangle spawnableArea = new Rectangle(100, 50, 400, 250);

        private static float lineSpeed = 5f;

        private List<List<Block>> allBlocks;
        private List<Block> activeBlocks;
        private List<Block> theLine;

        private Block testBlock1;
        private Player player;
        private StatusBar[] statusBars;

        private List<Block> loadedBlocks;


        private Block LastBlockOnLine
        {
            get { return theLine[theLine.Count - 1]; }
        }

        public GameplayState()
        {
            
        }

        public override void Enter() // Reading in blocks should happen here
        {
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
                    ) } );
                }

                // Creates a group of the 1st 3 blocks, can be removed once done testing
                allBlocks.Add(new List<Block>
                {
                    allBlocks[0][0],
                    allBlocks[1][0],
                    allBlocks[2][0]
                });

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
                new Vector2(0, 350),
                100));
            player = new Player();

            //Create status bars
            statusBars[0] = new StatusBar(new Vector2(0, 0), new Point(200, 80), player.Health, Color.Red);
            statusBars[1] = new StatusBar(new Vector2(0, 80), new Point(200, 80), 0, Color.Yellow);
            statusBars[2] = new StatusBar(new Vector2(0, 160), new Point(200, 80), 0, Color.Blue);
            statusBars[3] = new StatusBar(new Vector2(0, 240), new Point(200, 80), 0, Color.Green);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Tab))
            {
                return new MenuState();
            }

            if (MouseUtils.KeyJustPressed(Keys.Up))
            {
                player.Health += 5;
                player.Wealth += 5;
                player.Happiness += 5;
                player.Education += 5;
            }

            Random rand = new Random();

            if (MouseUtils.KeyJustPressed(Keys.Enter))
            {
                // Spawns a new block (or group of blocks) depending on player stats, gives random position if 
                List<Block> newBlocks = GenerateNewBlocks();
                if (newBlocks.Count > 0)
                {
                    float groupWidth = 0;
                    for(int i = 0; i < newBlocks.Count; i++)
                    {
                        activeBlocks.Add(newBlocks[i]);
                        if (i == 0)
                            newBlocks[0].Position = new Vector2(rand.Next(spawnableArea.Left, spawnableArea.Right), rand.Next(spawnableArea.Top, spawnableArea.Bottom));
                        else
                            newBlocks[i].Position = newBlocks[0].Position + new Vector2(groupWidth, 0);
                        groupWidth += newBlocks[i].Width;
                    }
                }

                //activeBlocks.Add(new Block(
                //    "test",
                //    new Vector2(rand.Next(spawnableArea.Left, spawnableArea.Right), rand.Next(spawnableArea.Top, spawnableArea.Bottom)),
                //    100));
            }

            for(int i = 0; i < activeBlocks.Count; i++)
            {
                activeBlocks[i].Update(gameTime);

                if (LastBlockOnLine.Right > activeBlocks[i].Left &&
                    LastBlockOnLine.Top - LastBlockOnLine.Height < activeBlocks[i].Top &&
                    LastBlockOnLine.Bottom + LastBlockOnLine.Height > activeBlocks[i].Bottom &&
                    activeBlocks[i].IsClicked == false)
                {
                    theLine.Add(activeBlocks[i]);
                    activeBlocks.RemoveAt(i);
                    i--;
                    theLine[theLine.Count - 1].Position = new Vector2(theLine[theLine.Count - 2].Right, theLine[theLine.Count - 2].Top);
                }
            }


            Vector2 adjustVector = new Vector2(-lineSpeed, 0);
            for(int i = 0; i < theLine.Count; i++)
            {
                if (kb.IsKeyDown(Keys.A))
                {
                    theLine[i].Position += adjustVector;
                }
            }

            UpdatePlayer();

            foreach(StatusBar bar in statusBars)
            {
                bar.Update();
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            for(int i = 0; i < theLine.Count; i++)
            {
                theLine[i].Draw(sb);
            }

            for(int i = 0; i < activeBlocks.Count; i++)
            {
                activeBlocks[i].Draw(sb);
            }

            foreach (StatusBar bar in statusBars)
            {
                bar.Draw(sb);
            }

            player.Draw(sb);
        }

        /// <summary>
        /// Updates the player's stats based on the block beneath their feet
        /// </summary>
        private void UpdatePlayer()
        {
            foreach(Block block in theLine)
            {
                if (block.Left <= 0 && block.Checked == false)
                {
                    player.Health += block.HealthMod;
                    player.Happiness += block.HappyMod;
                    player.Education += block.EducationMod;
                    player.Wealth += block.WealthMod;

                    player.Health = Math.Clamp(player.Health, 0, 100);
                    player.Happiness = Math.Clamp(player.Happiness, 0, 100);
                    player.Education = Math.Clamp(player.Education, 0, 100);
                    player.Wealth = Math.Clamp(player.Wealth, 0, 100);

                    statusBars[0].CurrentValue = player.Health;
                    statusBars[1].CurrentValue = player.Happiness;
                    statusBars[2].CurrentValue = player.Education;
                    statusBars[3].CurrentValue = player.Wealth;

                    player.Age += block.Width;

                    block.Checked = true;
                    break;
                }
            }

            if (LastBlockOnLine.Right <= 0)
            {
                //A man has fallen into the river in lego city!
                //player.Animation = new AnimationState(defaultImage, 1, true, 1);
                player.StartFalling();
            }

            if (player.Health <= 0)
            {
                player.Die();
            }
        }

        /// <summary>
        /// Returns a new block (or blocks) depending on the player's stats.
        /// </summary>
        /// <returns>The new block (or blocks) if possible, otherwise returns an empty block list.</returns>
        private List<Block> GenerateNewBlocks()
        {
            List<List<Block>> potentialBlocks = new List<List<Block>>(allBlocks.Count);
            foreach(List<Block> blockList in allBlocks)
            {
                bool success = true;
                foreach(Block block in blockList)
                {
                    if (!(block.HealthRange.IsInRange(player.Health)
                        && block.WealthRange.IsInRange(player.Wealth)
                        && block.HappyRange.IsInRange(player.Happiness)
                        && block.EducationRange.IsInRange(player.Education)
                        // && block.AgeRange.IsInRange(player.Age)
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
    }
}
