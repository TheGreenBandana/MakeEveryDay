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

        private GameObject innerBar;
        private float scaling;

        /// <summary>
        /// The current value of the status bar
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// Constructor for a status bar
        /// </summary>
        /// <param name="position">The vector2 representing the position of the bar</param>
        /// <param name="size">The point representing the size of the bar
        /// (try to keep it's width a multiple of 100, scaling may break if it isn't)</param>
        /// <param name="startValue">The starting value of the bar</param>
        /// <param name="color">The color of the bar in question</param>
        public StatusBar(Vector2 position, Point size, int startValue, Color color) : base(sprite, position, new Point(size.X + 6, size.Y), Color.Gray, .5f)
        {
            CurrentValue = startValue;
            innerBar = new GameObject(sprite, new Vector2(position.X + 3, position.Y + 3), new Point(size.X - 6, size.Y - 6), color, .6f);
            scaling = size.X / 100;
        }

        /// <summary>
        /// Updates the position of the inner bar
        /// </summary>
        public void Update()
        {
            innerBar.Width = (int)(CurrentValue * scaling);
        }

        /// <summary>
        /// Draws the inner and outer bars
        /// </summary>
        /// <param name="sb">The sprite batch to draw with</param>
        internal override void DrawUnscaled(SpriteBatch sb)
        {
            base.DrawUnscaled(sb);
            innerBar.DrawUnscaled(sb);
        }
        
    }
}
