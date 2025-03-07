using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    internal class Block: GameObject
    {

        // Fields - static
        internal static int presetHeight = 80; // Arbitrary number, controls how tall the blocks are
        internal static Texture2D baseBlockTexture = default;
        internal static Texture2D arrrowTexture = default;

        internal static Texture2D healthIcon;
        internal static Texture2D educationIcon;
        internal static Texture2D happyIcon;
        internal static Texture2D wealthIcon;

        internal static SpriteFont nameFont;

        internal static float blockDrawLayer = .5f;

        internal static Rectangle blockSpawnArea = new Rectangle(100, 50, 500, 300);

        internal static Point iconSize = new Point(15, 15);

        // Fields - non-static
        private string name;

        private int healthMod;
        private int educationMod;
        private int happyMod;
        private int wealthMod;

        private int healthArrows;
        private int educationArrows;
        private int happyArrows;
        private int wealthArrows;

        private CustomRange healthRange;
        private CustomRange educationRange;
        private CustomRange happyRange;
        private CustomRange wealthRange;
        private CustomRange ageRange;

        // Properties

        //Modifiers
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

        public int HappyMod
        {
            get { return happyMod; }
            set { happyMod = value; }
        }

        public int WealthMod
        {
            get { return wealthMod; }
            set { wealthMod = value; }
        }

        //Arrows
        public int HealthArrows
        {
            get { return healthArrows;}
            set { healthArrows = value; }
        }

        public int EducationArrows
        {
            get { return educationArrows; }
            set { educationArrows = value; }
        }

        public int HappyArrows 
        {
            get { return happyArrows; }
            set { happyArrows = value; }
        }

        public int WealthArrows
        {
            get { return wealthArrows; }
            set { wealthArrows = value; }
        }

        //Custom Ranges
        public CustomRange HealthRange
        {
            get { return HealthRange; }
            set { HealthRange = value; }
        }

        public CustomRange EducationRange 
        {
            get { return educationRange; }
            set { educationRange = value; }
        }

        public CustomRange HappyRange
        {
            get { return happyRange; }
            set { happyRange = value; }
        }

        public CustomRange WealthRange
        {
            get { return wealthRange; }
            set { wealthRange = value; }
        }

        public CustomRange AgeRange 
        {
            get { return ageRange; }
            set { ageRange = value; }
        }

        // Constructors

        // Constructor that takes everything
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
        //Default Constructor
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width)
            : this(name, position, width, Microsoft.Xna.Framework.Color.White, 
                  0, 0, 0, 0, 
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite){}
        
        //Constructor that takes only mods and no ranges
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width, Microsoft.Xna.Framework.Color color, int healthMod, int educationMod, int happyMod, int wealthMod)
            : base(baseBlockTexture, position, new Microsoft.Xna.Framework.Vector2(width, presetHeight), color, blockDrawLayer)
        {
            this.name = name;
            this.healthMod = healthMod;
            this.educationMod = educationMod;
            this.happyMod = happyMod;
            this.wealthMod = wealthMod;
            healthRange = CustomRange.Infinite;
            educationRange = CustomRange.Infinite;
            happyRange = CustomRange.Infinite;
            wealthRange = CustomRange.Infinite;
            ageRange = CustomRange.Infinite;
        }
        //Constructor that takes ONLY an Age Range
        //Figured we'd use age exclusively more than the others
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width, Microsoft.Xna.Framework.Color color, int healthMod, int educationMod, int happyMod, int wealthMod, CustomRange ageRange)
            : base(baseBlockTexture, position, new Microsoft.Xna.Framework.Vector2(width, presetHeight), color, blockDrawLayer)
        {
            this.name = name;
            this.healthMod = healthMod;
            this.educationMod = educationMod;
            this.happyMod = happyMod;
            this.wealthMod = wealthMod;
            healthRange = CustomRange.Infinite;
            educationRange = CustomRange.Infinite;
            happyRange = CustomRange.Infinite;
            wealthRange = CustomRange.Infinite;
            this.ageRange = ageRange;
        }


        // Methods


        internal override void Draw(SpriteBatch sb)
        {
            
            // Draw the box
            base.Draw(sb);

            // Draw the name
            sb.DrawString(
                nameFont,
                name,
                base.Position + Microsoft.Xna.Framework.Vector2.One * 5,
                Microsoft.Xna.Framework.Color.White);

            // Draw the icons

            float nextX = 0;

            sb.Draw(
                healthIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            nextX += iconSize.X; 

            sb.Draw(
                educationIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            nextX += iconSize.X;

            sb.Draw(
                happyIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            nextX += iconSize.X;

            sb.Draw(
                wealthIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);
        }

    }
}
