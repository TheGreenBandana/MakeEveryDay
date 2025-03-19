using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MakeEveryDay
{
    internal class Player : GameObject
    {
        public static Texture2D sprite;

        private int health;
        private int wealth;
        private int happiness;
        private int education;
        //private AnimationState animation;

        public int Health { get { return health; } set { health = value; } }
        public int Wealth { get { return wealth; } set { wealth = value; } }
        public int Happiness { get { return happiness; } set { happiness = value; } }
        public int Education { get { return education; } set { education = value; } }
        //public AnimationState Animation { get { return animation; } set { animation = value; } }

        public Player() : base(sprite, new Vector2(0, 300), new Point(50, 50))
        {
            health = 50;
            wealth = 0;
            happiness = 0;
            education = 0;
            //animation = new AnimationState(sprite, 1, true, 1);
        }

        internal override void Update(GameTime gameTime)
        {
            //animation.Update(gameTime);
            base.Update(gameTime);
        }

        internal override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            //animation.Draw(sb, base.Position, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }
    }
}
