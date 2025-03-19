using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    internal class Player : GameObject
    {
        public static Texture2D sprite;

        private int health;
        private int wealth;
        private int happiness;
        private int education;

        public int Health { get { return health; } set { health = value; } }
        public int Wealth { get { return wealth; } set { wealth = value; } }
        public int Happiness { get { return happiness; } set { happiness = value; } }
        public int Education { get { return education; } set { education = value; } }

        public Player() : base(sprite)
        {
            base.Position = new Vector2(0, 350 - sprite.Height);
            health = 50;
            wealth = 0;
            happiness = 0;
            education = 0;
        }

        //Call kyle's update and draw
    }
}
