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
            base.Draw(sb);

            sb.DrawString(
                nameFont,
                name,
                base.Position + Microsoft.Xna.Framework.Vector2.One * 5,
                Color.White,
                0,
                Microsoft.Xna.Framework.Vector2.Zero,
                Math.Clamp((Width - 10) / nameFont.MeasureString(name).X, 0, (Height - 10) / nameFont.MeasureString(name).Y),
                SpriteEffects.None,
                1);

            foreach(BlockType block in blocks)
            {
                ((GameObject) block).Draw(sb);
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
