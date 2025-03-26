using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class BlockGroup : GameObject//, //BlockType
    {
        public static Texture2D blockGroupSprite;
        public static SpriteFont nameFont;

        private string name;
        private List<Block> blocks;

        //Properties

        public string Name
        {
            get => name;
            set => name = value;
        }

        public List<Block> Blocks
        {
            get => blocks;
        }

        // Constructors
        public BlockGroup(string name, List<Block> blocks) : base(blockGroupSprite)
        {
            this.name = name;
            this.blocks = blocks;

            //.Size = new Vector2()
        }
        public BlockGroup(string name) : this(name, new List<Block>()) { }

        // Methods
        public void Add(Block block)
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
        }
    }
}
