using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class BlockGroup : BlockType
    {
        public static Texture2D blockGroupSprite;
        public static SpriteFont nameFont;

        private string name;
        private List<BlockType> blocks;

        //Properties
        public List<BlockType> Blocks
        {
            get => blocks;
        }

        // Constructors
        public BlockGroup(string name, Vector2 position) : this(name, new List<BlockType>(), position, new Vector2(200, 100)) { }
        public BlockGroup(string name, List<BlockType> blocks, Vector2 position, Vector2 size) : base(blockGroupSprite, position, size, Color.White, .5f)
        {
            this.name = name;
            this.blocks = blocks;
        }
        

        // Methods
        /// <summary>
        /// Add a block to the block group
        /// </summary>
        /// <param name="block"></param>
        public void Add(BlockType block)
        {
            blocks.Add(block);
        }


        
        internal override void Draw(SpriteBatch sb)
        {
            Vector2 e1 = new Vector2(0, 1);
            int currentXPosition = 0;

            base.DrawUnscaled(sb);

            sb.DrawString(
                nameFont,
                name,
                Position + e1 * 50,
                Color.DarkRed, //NEEDS TO BE CHANGED BACK TO WHITE
                0,
                Microsoft.Xna.Framework.Vector2.Zero,
                Math.Clamp((Width - 10) / nameFont.MeasureString(name).X, 0, (Height / 2) / nameFont.MeasureString(name).Y),
                SpriteEffects.None,
                1);
            
            foreach(BlockType block in blocks)
            {
                block.Position = new Vector2(base.AsRectangle.X + currentXPosition, base.AsRectangle.Y);
                currentXPosition += block.Width;

                block.DrawUnscaled(sb);
                /*block.Draw(sb, 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    1, //Scaling math I'm too tired to figure out (should make all the blocks fit into the group) (width should be equal to the blocks anyway, so this should focus on height not width)
                    SpriteEffects.None,
                    0.5f);*/
            }
            
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            base.Width = 0;

            foreach(BlockType block in blocks)
            {
                base.Width += block.Width;
            }

            if(base.Width == 0)
            {
                base.Width = 100;
            }
        }

        /// <summary>
        /// Gets the mods for which block we're currently above
        /// </summary>
        /// <param name="playerXPosition">current horizontal position of the player</param>
        /// <returns>a set of mods in a list in the following format: health, education, happiness, wealth</returns>
        public override List<int>? GetCurrentMods(float playerXPosition)
        {
            foreach (BlockType block in blocks)
            {
                if (block.Left < playerXPosition && block.Right >= playerXPosition)
                {
                    return block.GetCurrentMods(playerXPosition);
                }
            }
            return null;
        }
    }
}
