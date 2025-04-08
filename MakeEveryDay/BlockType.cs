using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal abstract class BlockType : GameObject
    {
        // Fields
        private static Texture2D defaultBlockTypeTexture;

        private string name;

        private BlockType? leftLink;
        private BlockType? rightLink;

        private Microsoft.Xna.Framework.Vector2 positionToClick;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public BlockType LeftLink
        {
            get
            {
                return leftLink;
            }
            set
            {
                leftLink = value;
                if (value != null)
                {
                    value.RightLink = this;
                }
            }
        }
        public BlockType? RightLink
        {
            get
            {
                return rightLink;
            }
            set
            {
                rightLink = value;
                if (value != null)
                {
                    value.LeftLink = this;
                }
            }
        }

        public bool IsClicked
        {
            get { return positionToClick != -Microsoft.Xna.Framework.Vector2.One; }
        }
        public Microsoft.Xna.Framework.Vector2 PositionToClick
        {
            get { return positionToClick; }
            set { positionToClick = value; }
        }

        public BlockType() : base() { }
        public BlockType(Texture2D sprite) : base(sprite) { }
        public BlockType(
            Texture2D baseBlockTexture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 size,
            Microsoft.Xna.Framework.Color color,
            float blockDrawLayer) : base(baseBlockTexture, position, size, color, blockDrawLayer) { }


        public abstract List<int> GetCurrentMods(float playerXPosition);
    }
}
