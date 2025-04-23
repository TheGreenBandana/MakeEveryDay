using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal class GroupMakerState : State
    {
        //private BlockGroup mainGroup;
        private Player player;

        private List<Block> activeBlocks;
        

        public GroupMakerState(Player player)
        {
            this.player = player;
        }

        public override void Enter()
        {
            Random rand = new Random();
            //mainGroup = new BlockGroup("test",new Vector2(200, 400));
            activeBlocks = new List<Block>();
            foreach (List<Block> blockList in GameplayState.allBlocks)
            {
                Block block = blockList[0];
                if (block.AgeRange.Min <= player.Age)
                {
                    Block newBlock;
                    if (block.PresetColor != null)
                    {
                        newBlock = new Block(
                            block.Name,
                            block.Position,
                            block.Width,
                            (Microsoft.Xna.Framework.Color) block.PresetColor,
                            block.HealthMod,
                            block.EducationMod,
                            block.HappyMod,
                            block.WealthArrows,
                            block.HealthRange,
                            block.EducationRange,
                            block.HappyRange,
                            block.WealthRange,
                            block.AgeRange,
                            block.NumSpawns,
                            block.Dependencies,
                            block.DeathMessage);
                    }
                    else
                    {
                        
                        newBlock = new Block(
                            block.Name,
                            block.Position,
                            block.Width,
                            Microsoft.Xna.Framework.Color.White,
                            block.HealthMod,
                            block.EducationMod,
                            block.HappyMod,
                            block.WealthArrows,
                            block.HealthRange,
                            block.EducationRange,
                            block.HappyRange,
                            block.WealthRange,
                            block.AgeRange,
                            block.NumSpawns,
                            block.Dependencies,
                            block.DeathMessage);
                    }
                    
                    do
                    {
                        newBlock.Position = new Vector2(rand.Next((int)Game1.ScreenSize.X - block.AsRectangle.X), rand.Next((int)Game1.ScreenSize.Y - block.AsRectangle.Y));
                    }
                    while (BlockInsideOtherBlocks(newBlock, activeBlocks));
                    activeBlocks.Add(newBlock);
                }
            }
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            foreach(Block block in activeBlocks)
            {
                block.Update(gameTime);
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            //mainGroup.Draw(sb);
            
            foreach(Block block in activeBlocks)
            {
                block.Draw(sb);
            }

        }
        private bool BlockInsideOtherBlocks(Block check, List<Block> otherBlocks)
        {
            foreach(Block block in otherBlocks)
            {
                if (block.AsRectangle.Intersects(check.AsRectangle))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
