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

        /// <summary>
        /// Gets/sets the current name of the blocktype
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Gets/sets the block linked left to this one. Mostly useless functionality at this point
        /// </summary>
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
        /// <summary>
        /// Gets/sets the block linked right to this one. Mostly useless functionality at this point.
        /// </summary>
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

        /// <summary>
        /// Returns a bool representing if the block is currently clicked
        /// </summary>
        public bool IsClicked
        {
            get { return positionToClick != -Microsoft.Xna.Framework.Vector2.One; }
        }
        /// <summary>
        /// Returns the current positionToClick value, set to -Vector2.One (xna vector) to unclick (i think)
        /// </summary>
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

        /// <summary>
        /// Used to get the set of modifiers that should be affecting the player given their current position
        /// </summary>
        /// <param name="playerXPosition">current x position of the player, the mods of the block directly under which will be returned</param>
        /// <returns>A set of mods dependent on the current position of the player</returns>
        public abstract List<int> GetCurrentMods(float playerXPosition);
    }
}
