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

        private List<Block> allBlocks;
        private List<Block> activeBlocks;
        private List<Block> theLine;

        private Block testBlock1;
        private Block prevBlock;
        private Player player;

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
            allBlocks = new List<Block>();

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
                    allBlocks.Add(new Block(
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
                    ));
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
                new Vector2(0, 350),
                100));
            player = new Player();
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

            Random rand = new Random();

            if (MouseUtils.KeyJustPressed(Keys.Enter))
            {
                Random rng = new();
                activeBlocks.Add(Block.CloneBlock(allBlocks[rng.Next(0, allBlocks.Count)]));
                activeBlocks[activeBlocks.Count - 1].Position = new Vector2(rand.Next(spawnableArea.Left, spawnableArea.Right), rand.Next(spawnableArea.Top, spawnableArea.Bottom));
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

            player.Draw(sb);
        }

        private void UpdatePlayer(KeyboardState kb)
        {
            foreach(Block block in theLine)
            {
                if(block.Left == 0 && block != prevBlock)
                {
                    player.Health += block.HealthMod;
                    player.Wealth += block.WealthMod;
                    player.Happiness += block.HappyMod;
                    player.Education += block.EducationMod;
                    prevBlock = block;
                }
            }
            if (LastBlockOnLine.Right == 0)
            {
                //A man has fallen into the river in lego city!
                //player.Animation = new AnimationState(defaultImage, 1, true, 1);
            }
        }
    }
}
