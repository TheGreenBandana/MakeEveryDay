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
using static System.Net.Mime.MediaTypeNames;

namespace MakeEveryDay
{
    internal class Block: GameObject
    {

        // Fields - static
        internal static int presetHeight = 80; // Arbitrary number, controls how tall the blocks are
        internal static Texture2D baseBlockTexture = default;
        internal static Texture2D arrowTexture = default;

        internal static Texture2D[] statIcons = new Texture2D[4];

        internal static SpriteFont nameFont;

        internal static float blockDrawLayer = .5f;

        internal static Rectangle blockSpawnArea = new Rectangle(100, 50, 500, 300);

        internal static Point iconSize = new Point(15, 15);

        // Fields - non-static
        private string name;
        
        private int[] statMods;

        private int[] statArrows;

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
            get { return statMods[0]; }
            set { statMods[0] = value; }
        }

        public int EducationMod
        {
            get { return statMods[1]; }
            set { statMods[1] = value; }
        }

        public int HappyMod
        {
            get { return statMods[2]; }
            set { statMods[2] = value; }
        }

        public int WealthMod
        {
            get { return statMods[3]; }
            set { statMods[3] = value; }
        }

        //Arrows
        public int HealthArrows
        {
            get { return statArrows[0];}
            set { statArrows[0] = value; }
        }

        public int EducationArrows
        {
            get { return statArrows[1]; }
            set { statArrows[1] = value; }
        }

        public int HappyArrows 
        {
            get { return statArrows[2]; }
            set { statArrows[2] = value; }
        }

        public int WealthArrows
        {
            get { return statArrows[3]; }
            set { statArrows[3] = value; }
        }

        //Custom Ranges
        public CustomRange HealthRange
        {
            get { return healthRange; }
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

            statMods = new int[4];
            statMods[0] = healthMod;
            statMods[1] = educationMod;
            statMods[2] = happyMod;
            statMods[3] = wealthMod;

            this.healthRange = healthRange;
            this.educationRange = educationRange;
            this.happyRange = happyRange;
            this.wealthRange = wealthRange;
            this.ageRange = ageRange;

            statArrows = new int[4];
            for(int i = 0; i < statArrows.Length; i++)
            {
                statArrows[i] = ArrowFromModHelper(statMods[i]);
            }
            

        }
        /// <summary>
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

            Point currentScaledMousePosition = MouseUtils.ScaleMousePosition(MouseUtils.CurrentState.Position);

            if (MouseUtils.IsJustPressed() && ScaledRectangle.Contains(currentScaledMousePosition))
            {
                positionToClick = new Microsoft.Xna.Framework.Vector2(ScaledRectangle.X, ScaledRectangle.Y) - currentScaledMousePosition.ToVector2();
            }

            if (MouseUtils.CurrentState.LeftButton == ButtonState.Pressed && positionToClick != -Microsoft.Xna.Framework.Vector2.One) {
                Position = currentScaledMousePosition.ToVector2() + positionToClick;
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

            for (int i = 0; i < statIcons.Length; i++)
            {
                sb.Draw(
                    statIcons[i],
                    new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                    Microsoft.Xna.Framework.Color.White);

                nextX += iconSize.X;

                DrawArrowsHelper(sb, statArrows[i], ref nextX);

                nextX += iconSize.X * Math.Abs(statArrows[i]);
            }
        }

        /// <summary>
        /// Turns the block into something storable.
        /// </summary>
        /// <returns>The string of the block.</returns>
        public override string ToString()
        {
            return $"{name}|{Size.X}|{PresetColor.GetHashCode}|{statMods[0]}|{statMods[1]}|{statMods[2]}|{statMods[3]}|" +
                $"{healthRange.ToString()}|{educationRange.ToString()}|{happyRange.ToString()}|{wealthRange.ToString()}|{ageRange.ToString()}";
        }

        // Helper Methods
        private int ArrowFromModHelper(int mod)
        {
            

            if (mod == 0)
            {
                return 0;
            }
            else if (mod> 0)
            {
                return mod / 25 + 1;
            } else if (mod < 0)
            {
                return mod / 25 - 1;
            }
            else
            {
                throw new Exception("Oopsie Kyle fucked it.");
            }


        }

        private void DrawArrowsHelper(SpriteBatch sb, int arrows, ref float nextX)
        {
            if (arrows == 0) return;
            int arrowsNormal = arrows / Math.Abs(arrows);

            switch (arrowsNormal)
            {
                case 1:
                    for (int i = 0; i < arrows; i++)
                    {
                        sb.Draw(
                        arrowTexture,
                            new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                            Microsoft.Xna.Framework.Color.White);
                    }
                    break;
                case -1:
                    for (int i = 0; i < Math.Abs(arrows); i++)
                    {
                        sb.Draw(
                        arrowTexture,
                            new Rectangle((base.Position + new Microsoft.Xna.Framework.Vector2(nextX, base.Height - iconSize.Y)).ToPoint(), iconSize),
                            Microsoft.Xna.Framework.Color.White);
                    }
                    break;
            }
            
        }

        /// <summary>
        /// Returns a clone of a block.
        /// </summary>
        /// <param name="block">The block to clone.</param>
        /// <returns>The clone of the given block.</returns>
        public static Block CloneBlock(Block block)
        {
            return new Block(
                block.name, 
                block.Position, 
                block.Width, 
                (Color)block.PresetColor,
                block.HealthMod, 
                block.EducationMod, 
                block.HappyMod, 
                block.WealthMod, 
                block.healthRange, 
                block.educationRange, 
                block.happyRange, 
                block.wealthRange, 
                block.ageRange);
        }
    }
}
