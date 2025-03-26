using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class StatusBar : GameObject
    {
        public static Texture2D sprite;

        private int currentValue;
        private GameObject innerBar;
        private float scaling;

        public int CurrentValue { get { return currentValue; } set { currentValue = value; } }

        /// <summary>
        /// Constructor for a status bar
        /// </summary>
        /// <param name="position">The vector2 representing the position of the bar</param>
        /// <param name="size">The point representing the size of the bar</param>
        /// <param name="startValue">The starting value of the bar</param>
        public StatusBar(Vector2 position, Point size, int startValue) : base(sprite, position, size, Color.White, .5f)
        {
            currentValue = startValue;
            innerBar = new GameObject(sprite, new Vector2(position.X + 3, position.Y + 3), new Point(size.X - 6, size.Y - 6), Color.Green, .6f);
            scaling = 100 / (size.X - 6);
        }

        /// <summary>
        /// Updates the position of the inner bar
        /// </summary>
        public void Update()
        {
            innerBar.Width = (int)(currentValue * scaling);
        }

        /// <summary>
        /// Draws the inner and outer bars
        /// </summary>
        /// <param name="sb">The sprite batch to draw with</param>
        internal override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            innerBar.Draw(sb);
        }
        
    }
}
