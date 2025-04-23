using MakeEveryDay.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;

namespace MakeEveryDay
{
    internal class Player : GameObject
    {
        public int Health { get; set; }
        public int Wealth { get; set; }
        public int Happiness { get; set; }
        public int Education { get; set; }
        public int Age { get; set; }
        public static AnimationState Running { get; set; }
        public AnimationState Animation { get; set; }
        public static Texture2D Fall { get; set; }
        public static Texture2D Trip { get; set; }

        public Player() : base(Running.Texture, new Vector2(50, Game1.BridgePosition - 170), new Point(50, 50))
        {
            Health = 50;
            Wealth = 25;
            Happiness = 25;
            Education = 25;
            Age = 0;
            Animation = Running;
        }

        /// <summary>
        /// Code to run when the player runs off a block
        /// </summary>
        public void StartFalling()
        {
            Animation = new AnimationState(Fall, 15, false, 12);
            SoundsUtils.screamSound.Play(volume:SoundsUtils.soundEffectsVolume,0,0);
            //will eventually switch the animation being used to the falling animation
        }

        /// <summary>
        /// Code to run to kill the player while they're on a block
        /// </summary>
        public void Die()
        {
            Animation = new AnimationState(Trip, 18, false, 12);
            SoundsUtils.thudSound.Play(volume:SoundsUtils.soundEffectsVolume, 0,0);
            //will eventually switch the animation being used to a tripping and falling animation
        }

        internal override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch sb)
        {
            Animation.Draw(sb, base.Position, 200/360f, 1f);
        }
    }
}
