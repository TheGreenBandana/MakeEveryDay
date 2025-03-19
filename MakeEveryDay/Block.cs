using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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
        internal static Texture2D arrowTexture = default;

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

        private Microsoft.Xna.Framework.Vector2 positionToClick;

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

        // Misc.
        public bool IsClicked
        {
            get { return positionToClick != - Microsoft.Xna.Framework.Vector2.One; }
        }

        // Constructors

        /// <summary>
        /// Constructor that takes everything
        /// </summary>
        /// <param name="name">Name of the block</param>
        /// <param name="position">Position of the block on the screen</param>
        /// <param name="width">Width of the block</param>
        /// <param name="color">Color of the block</param>
        /// <param name="healthMod">Amount block changes the "Health" stat</param>
        /// <param name="educationMod">Amount block changes the "Education" stat</param>
        /// <param name="happyMod">Amount block changes the "Happiness" stat</param>
        /// <param name="wealthMod">Amount block changes the "Wealth" stat</param>
        /// <param name="healthRange">Range of the "Health" stat required for the block to appear</param>
        /// <param name="educationRange">Range of the "Education" stat required for the block to appear</param>
        /// <param name="happyRange">Range of the "Happiness" stat required for the block to appear</param>
        /// <param name="wealthRange">Range of the "Wealth" stat required for the block to appear</param>
        /// <param name="ageRange">Range of the "Age" stat required for the block to appear</param>
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


            
            this.healthArrows = (int)(Math.Round(healthMod / 25.0));
            this.educationArrows = (int)(educationMod / 25.0 + 1);
            this.happyArrows = (int)(happyMod / 25.0 + 1);
            this.wealthArrows = (int)(wealthMod / 25.0 + 1);
            

        }/// <summary>
         /// Default Constructor
         /// </summary>
         /// <param name="name">Name of the Block</param>
         /// <param name="position">Position of the block on the screen</param>
         /// <param name="width">Width of the block</param>
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width)
            : this(name, position, width, Microsoft.Xna.Framework.Color.White, 
                  0, 0, 0, 0, 
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite){}


        /// <summary>
        /// Constructor for block that takes only mods and no ranges
        /// </summary>
        /// <param name="name">Name of the block</param>
        /// <param name="position">Position of the block on the screen</param>
        /// <param name="width">Width of the block</param>
        /// <param name="color">Color of the block</param>
        /// <param name="healthMod">Amount block changes the "Health" stat</param>
        /// <param name="educationMod">Amount block changes the "Education" stat</param>
        /// <param name="happyMod">Amount block changes the "Happiness" stat</param>
        /// <param name="wealthMod">Amount block changes the "Wealth" stat</param>
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width, Microsoft.Xna.Framework.Color color, int healthMod, int educationMod, int happyMod, int wealthMod)
            : this(name, position, width, color,
                  healthMod, educationMod, happyMod, wealthMod,
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite) {}

        /// <summary>
        /// Constructor that takes ONLY an Age Range
        /// Figured we'd use age exclusively more than the others
        /// </summary>
        /// <param name="name">Name of the block</param>
        /// <param name="position">Position of the block on the screen</param>
        /// <param name="width">Width of the block</param>
        /// <param name="color">Color of the block</param>
        /// <param name="healthMod">Amount block changes the "Health" stat</param>
        /// <param name="educationMod">Amount block changes the "Education" stat</param>
        /// <param name="happyMod">Amount block changes the "Happiness" stat</param>
        /// <param name="wealthMod">Amount block changes the "Wealth" stat</param>
        /// <param name="ageRange">Range of the "Age" stat required for the block to appear</param>
        public Block(string name, Microsoft.Xna.Framework.Vector2 position, int width, Microsoft.Xna.Framework.Color color, int healthMod, int educationMod, int happyMod, int wealthMod, CustomRange ageRange)
            : this(name, position, width, color,
                  healthMod, educationMod, happyMod, wealthMod,
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, ageRange) { }


        // Methods

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MouseUtils.IsJustPressed() && AsRectangle.Contains(MouseUtils.CurrentState.Position))
            {
                positionToClick = Position - MouseUtils.CurrentState.Position.ToVector2();
            }

            if (MouseUtils.CurrentState.LeftButton == ButtonState.Pressed && positionToClick != -Microsoft.Xna.Framework.Vector2.One) {
                Position = MouseUtils.CurrentState.Position.ToVector2() + positionToClick;
            }

            if (MouseUtils.CurrentState.LeftButton == ButtonState.Released)
            {
                positionToClick = -Microsoft.Xna.Framework.Vector2.One;
            }
        }

        internal override void Draw(SpriteBatch sb)
        {
            
            // Draw the box
            base.Draw(sb);

            // Draw the name
            sb.DrawString(
                nameFont,
                name,
                base.Position + Microsoft.Xna.Framework.Vector2.One * 5,
                Color.White,
                0,
                Microsoft.Xna.Framework.Vector2.Zero,
                Math.Clamp((Width - 10) / nameFont.MeasureString(name).X, 0, (Height - 10) / nameFont.MeasureString(name).Y), 
                SpriteEffects.None, 
                1);

            // Draw the icons

            float nextX = 0;

            sb.Draw(
                healthIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            for(int i = 0; i< healthArrows; i++)
            {
                nextX += iconSize.X;
                
                sb.Draw(
                    arrowTexture,
                    new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                    Microsoft.Xna.Framework.Color.White);
            }

            nextX += iconSize.X; 

            sb.Draw(
                educationIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            for (int i = 0; i < educationArrows; i++)
            {
                nextX += iconSize.X;

                sb.Draw(
                    arrowTexture,
                    new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                    Microsoft.Xna.Framework.Color.White);
            }

            nextX += iconSize.X;

            sb.Draw(
                happyIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            for (int i = 0; i < happyArrows; i++)
            {
                nextX += iconSize.X;

                sb.Draw(
                    arrowTexture,
                    new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                    Microsoft.Xna.Framework.Color.White);
            }

            nextX += iconSize.X;

            sb.Draw(
                wealthIcon,
                new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                Microsoft.Xna.Framework.Color.White);

            for (int i = 0; i < wealthArrows; i++)
            {
                nextX += iconSize.X;

                sb.Draw(
                    arrowTexture,
                    new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                    Microsoft.Xna.Framework.Color.White);
            }
        }

        /// <summary>
        /// Turns the block into something storable.
        /// </summary>
        /// <returns>The string of the block.</returns>
        public override string ToString()
        {
            return $"{name}|{Size.X}|{PresetColor.GetHashCode}|{healthMod}|{educationMod}|{happyMod}|{wealthMod}|" +
                $"{healthRange.ToString()}|{educationRange.ToString()}|{happyRange.ToString()}|{wealthRange.ToString()}|{ageRange.ToString()}";
        }
    }
}
