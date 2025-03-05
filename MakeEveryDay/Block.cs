using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    internal class Block: GameObject
    {

        // Fields - static
        internal static int presetHeight = 40; // Arbitrary number, controls how tall the blocks are
        internal static Texture2D baseBlockTexture = default;
        internal static Texture2D arrrowTexture = default;

        internal static Texture2D healthIcon;
        internal static Texture2D educationIcon;
        internal static Texture2D happyIcon;
        internal static Texture2D wealthIcon;

        internal static SpriteFont nameFont;

        internal static float blockDrawLayer = .5f;

        internal static Rectangle blockSpawnArea = new Rectangle(100, 50, 500, 300);

        // Fields - non-static
        private string name;

        private int healthMod;
        private int educationMod;
        private int happyMod;
        private int wealthMod;

        private CustomRange healthRange;
        private CustomRange educationRange;
        private CustomRange happyRange;
        private CustomRange wealthRange;
        private CustomRange ageRange;

        // Properties

        public int HealthMod
        {
            get { return healthMod; }
            set { healthMod = value; }
        }

        public int EducationMod
        {
            get { return educationMod; }
            set { educationMod = value; }
        }
        // I'm too lazy to do the rest of this rn

        // Constructors
        public Block(
            string name,
            Microsoft.Xna.Framework.Vector2 position,
            int width,
            Microsoft.Xna.Framework.Color color,
            int healthMod,
            int educationMod,
            int happyMod,
            int wealthMod,
            CustomRange healthRange,
            CustomRange educationRange,
            CustomRange happyRange,
            CustomRange wealthRange,
            CustomRange ageRange)
            : base(baseBlockTexture, position, new Microsoft.Xna.Framework.Vector2(width,presetHeight), color, blockDrawLayer)
        {
            this.name = name;

            this.healthMod = healthMod;
            this.educationMod = educationMod;
            this.happyMod = happyMod;
            this.wealthMod = wealthMod;

            this.healthRange = healthRange;
            this.educationRange = educationRange;
            this.happyRange = happyRange;
            this.wealthRange = wealthRange;
            this.ageRange = ageRange;
        }

        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width)
            : this(name, position, width, Microsoft.Xna.Framework.Color.White, 
                  0, 0, 0, 0, 
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite){}



        // Methods


        internal override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            sb.DrawString(
                nameFont,
                name,
                base.Position + Microsoft.Xna.Framework.Vector2.One * 5,
                Microsoft.Xna.Framework.Color.White);
        }

    }
}
