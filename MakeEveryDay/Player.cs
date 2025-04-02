using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MakeEveryDay
{
    internal class Player : GameObject
    {
        public static Texture2D sprite;

        public int Health { get; set; }
        public int Wealth { get; set; }
        public int Happiness { get; set; }
        public int Education { get; set; }
        public int Age { get; set; }
        public static AnimationState Animation { get; set; }

        public Player() : base(Animation.Texture, new Vector2(0, Game1.BridgePosition - 50), new Point(50, 50))
        {
            Health = 50;
            Wealth = 0;
            Happiness = 0;
            Education = 0;
            Age = 0;
        }

        /// <summary>
        /// Code to run when the player runs off a block
        /// </summary>
        public void StartFalling()
        {
            base.PresetColor = Color.Red;
            //will eventually switch the animation being used to the falling animation
        }

        /// <summary>
        /// Code to run to kill the player while they're on a block
        /// </summary>
        public void Die()
        {
            base.PresetColor = Color.Red;
            //will eventually switch the animation being used to a tripping and falling animation
        }

        internal override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch sb)
        {
            //base.Draw(sb);
            Animation.Draw(sb, base.Position, 50/360f, 1f);
        }
    }
}
