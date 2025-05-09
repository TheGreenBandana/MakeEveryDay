﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MakeEveryDay.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace MakeEveryDay
{
    internal class Block: BlockType
    {

        // Fields - static
        internal static int presetHeight = 80; // Arbitrary number, controls how tall the blocks are
        internal static Texture2D baseBlockTexture = default;
        internal static Texture2D arrowTexture = default;

        internal static Texture2D[] statIcons = new Texture2D[4];

        internal static SpriteFont nameFont;

        internal static float blockDrawLayer = .5f;
        //internal static DrawLayer blockDrawLayer = DrawLayer.GameObjects1;

        internal static Rectangle blockSpawnArea = new Rectangle(100, 50, 500, 300);

        internal static Point iconSize = new Point(15, 15);

        // Fields - non-static        
        private int[] statMods;

        private int[] statArrows;

        private CustomRange healthRange;
        private CustomRange educationRange;
        private CustomRange happyRange;
        private CustomRange wealthRange;
        private CustomRange ageRange;

        private int numSpawns;
        private int[] dependencies;
        private string deathMessage;

        private Microsoft.Xna.Framework.Vector2 positionToClick;
        private Microsoft.Xna.Framework.Vector2 previousPosition;
        private Microsoft.Xna.Framework.Vector2 widthChangingOffset;

        private bool mouseHovering;
        private bool mouseHoveringReal;
        private bool mouseHoveringPrevious;
        private bool currentlyHeld;
        private bool previouslyHeld;

        // Properties
        public bool Checked
        {
            get;
            set;
        }
        public bool MouseHovering => mouseHovering;
        public bool WasJustHeld => !currentlyHeld && previouslyHeld;
        public string DeathMessage => deathMessage;

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

        public int NumSpawns { get => numSpawns; set => numSpawns = value; }
        public int[] Dependencies { get => dependencies; set => dependencies = value; }

        public bool IsClicked { get => currentlyHeld; set => currentlyHeld = value; }

        public Rectangle HoveredRectangle
        {
            get
            {
                Rectangle scaledRect = ScaledRectangle;
                float scaleFactor = Game1.Width / Game1.ScreenSize.X * 1.5f;
                if (scaleFactor > 1)
                {
                    scaledRect.Width = (int)(scaledRect.Width * scaleFactor);
                    scaledRect.Height = (int)(scaledRect.Height * scaleFactor);
                    scaledRect.X = scaledRect.X - scaledRect.Width / 2 + ScaledRectangle.Width / 2;
                    scaledRect.Y = scaledRect.Y - scaledRect.Height / 2 + ScaledRectangle.Height / 2;
                }
                return scaledRect;
            }
        }

        public Point IconSize
        {
            get
            {
                float scaleFactor = Game1.Width / Game1.ScreenSize.X * 1.5f;
                return mouseHovering ? new Point((int)(iconSize.X * (scaleFactor > 1 ? scaleFactor : 1)), (int)(iconSize.Y * (scaleFactor > 1 ? scaleFactor : 1))) : iconSize;
            }
        }

        public Point ArrowSize
        {
            get
            {
                Point storedIconSize = iconSize;
                float scaleFactor = Game1.Width / Game1.ScreenSize.X * 1.5f;
                int totalIcons = 0;
                foreach (int value in statArrows)
                    totalIcons += Math.Abs(value) + 1;
                while (storedIconSize.X * totalIcons > Width / scaleFactor * 1.5f && storedIconSize.X >= 5)
                    storedIconSize.X--;
                return mouseHovering ? new Point((int)(storedIconSize.X * (scaleFactor > 1 ? scaleFactor : 1)), (int)(storedIconSize.Y * (scaleFactor > 1 ? scaleFactor : 1))) : storedIconSize;
            }
        }

        public float TotalInfoSize
        {
            get
            {
                float scaleFactor = Game1.Width / Game1.ScreenSize.X;
                float totalSize = 0;
                int count = 0;
                foreach (int value in statArrows)
                {
                    totalSize += Math.Abs(value) * ArrowSize.X;
                    if (value != 0)
                    {
                        totalSize += iconSize.X;
                        count++;
                        if (count > 1)
                            totalSize += iconSize.X;
                    }
                }
                return totalSize;
            }
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
        /// <param name="numSpawns">Number of times this block can spawn (0 == infinite)</param>
        /// <param name="dependencies">The array of block index values required for this block to spawn</param>
        /// <param name="deathMessage">The message to show when this block causes a game over</param>
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
            CustomRange ageRange,
            int numSpawns,
             int[] dependencies,
             string deathMessage)
            : base(baseBlockTexture, position, new Microsoft.Xna.Framework.Vector2(width,presetHeight), color, blockDrawLayer)
        {
            this.Name = name;

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

            this.numSpawns = numSpawns <= 0 ? -1 : numSpawns;
            this.dependencies = dependencies;
            this.deathMessage = deathMessage;

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
                  CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, CustomRange.Infinite, 0, new int[1] {-1}, ""){}

        // Methods

        internal override void Update(GameTime gameTime)
        {
            previouslyHeld = currentlyHeld;
           
            base.Update(gameTime);

            Point realMousePosition = MouseUtils.CurrentState.Position;
            Point currentScaledMousePosition = MouseUtils.ScaleMousePosition(realMousePosition);

            // Mouse hovering scaling
            mouseHoveringReal = ScaledRectangle.Contains(realMousePosition);
            mouseHovering = HoveredRectangle.Contains(realMousePosition);

            if (!mouseHoveringPrevious && !GameplayState.GrabbingBlock)
            {
                mouseHovering = mouseHoveringReal;
                mouseHoveringPrevious = mouseHovering;
            }
            else
                mouseHovering = HoveredRectangle.Contains(realMousePosition);
            if (!mouseHovering)
                mouseHoveringPrevious = false;

            // Block detection
            if (MouseUtils.IsJustPressed() && mouseHovering && !GameplayState.GrabbingBlock)
            {
                PositionToClick = Position - currentScaledMousePosition.ToVector2();
                widthChangingOffset = Microsoft.Xna.Framework.Vector2.Zero;
                GameplayState.GrabbingBlock = true;
                currentlyHeld = true;
            }

            if (MouseUtils.CurrentState.LeftButton == ButtonState.Pressed && currentlyHeld) {
                mouseHovering = false;
                // Maintain offset when width changes
                if (Game1.Width < GameplayState.TargetWidth)
                {
                    float scaleFactor = Game1.Width / Game1.ScreenSize.X;
                    widthChangingOffset -= (previousPosition - MouseUtils.ScaleMousePosition(new Point((int)Game1.ScreenSize.X, Game1.BridgePosition)).ToVector2())
                        * new Microsoft.Xna.Framework.Vector2(0, -Game1.ScreenSize.Y / Game1.ScreenSize.X / scaleFactor);
                }
                Position = currentScaledMousePosition.ToVector2() + PositionToClick + widthChangingOffset;
            }

            if (MouseUtils.CurrentState.LeftButton == ButtonState.Released)
            {
                if (MouseUtils.PreviousState.LeftButton == ButtonState.Pressed)
                {
                    mouseHovering = false;
                    GameplayState.GrabbingBlock = false;
                }
                currentlyHeld = false;
            }

            previousPosition = MouseUtils.ScaleMousePosition(new Point((int)Game1.ScreenSize.X, Game1.BridgePosition)).ToVector2();
        }

        internal override void Draw(SpriteBatch sb)
        {
            // Draw the box
            base.Draw(sb);

            // Only do scaling calculation once
            Rectangle scaledRectangle = mouseHovering ? HoveredRectangle : ScaledRectangle;
            float scaleFactor = Game1.Width / Game1.ScreenSize.X;

            // Scaling
            float scale;
            try
            {
                scale = Math.Clamp((scaledRectangle.Width - (10 * scaleFactor)) / nameFont.MeasureString(Name).X, .1f,
                    (scaledRectangle.Height - (10 * scaleFactor)) / nameFont.MeasureString(Name).Y);
            }
            catch
            {
                scale = .1f;
            }

            // Draw the name
            sb.DrawString(
                nameFont,
                Name,
                scaledRectangle.Location.ToVector2() + Microsoft.Xna.Framework.Vector2.One * 5 * scaleFactor,
                Color.Black,
                0.001f,
                Microsoft.Xna.Framework.Vector2.Zero,
                scale,
                SpriteEffects.None,
                1);

            // Draw the icons

            float nextX = (Width - TotalInfoSize) / 2 / (mouseHovering && scaleFactor > 2f/3f ? 2f/3f : scaleFactor);

            for (int i = 0; i < statIcons.Length; i++)
            {
                if (statArrows[i] != 0)
                {
                    sb.Draw(
                    statIcons[i],
                    new Rectangle((scaledRectangle.Location.ToVector2() + new Microsoft.Xna.Framework.Vector2(nextX, (AsRectangle.Height * (mouseHovering && scaleFactor * 1.5f > 1 ? scaleFactor * 1.5f : 1) - IconSize.Y) / scaleFactor)).ToPoint(),
                        new Point((int)(IconSize.X / scaleFactor), (int)(IconSize.Y / scaleFactor))),
                    Microsoft.Xna.Framework.Color.White);

                    nextX += IconSize.X / scaleFactor;

                    DrawArrowsHelper(sb, statArrows[i], ref nextX);
                }
            }
        }

        /// <summary>
        /// Returns a list of the mods for this block in following order: "Health","Edu","Happy","Money." Required method for BlockType interface, more necessary for BlockGroup class
        /// </summary>
        /// <param name="playerXPosition">X position to check the block for</param>
        /// <returns>List of the mods for the block as listed above</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<int> GetCurrentMods(float playerXPosition)
        {
            return statMods.ToList<int>();
        }


        /// <summary>
        /// Turns the block into something storable.
        /// </summary>
        /// <returns>The string of the block.</returns>
        public override string ToString()
        {
            return $"{Name}|{Size.X}|{PresetColor.GetHashCode}|{statMods[0]}|{statMods[1]}|{statMods[2]}|{statMods[3]}|" +
                $"{healthRange.ToString()}|{educationRange.ToString()}|{happyRange.ToString()}|{wealthRange.ToString()}|{ageRange.ToString()}";
        }

        // Helper Methods
        /// <summary>
        /// Called when the value for Arrows needs to be initialized 
        /// </summary>
        /// <param name="mod">-100 to 100 modifier for the stat</param>
        /// <returns>arrow value for the associated modifier</returns>
        /// <exception cref="Exception">Shouldn't happen, talk to Kyle or read/fix the code yourself</exception>
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

        /// <summary>
        /// Draws arrows to the screen depending on the value of ___Arrows for each stat
        /// </summary>
        /// <param name="sb">SpriteBatch object for the frame</param>
        /// <param name="arrows">the Arrows value for the stat</param>
        /// <param name="nextX">current value of nextX (see Draw() for more details)</param>
        private void DrawArrowsHelper(SpriteBatch sb, int arrows, ref float nextX)
        {
            if (arrows == 0) return;
            int arrowsNormal = arrows / Math.Abs(arrows);

            // Only do scaling calculation once
            Rectangle scaledRectangle = mouseHovering ? HoveredRectangle : ScaledRectangle;
            float scaleFactor = Game1.Width / Game1.ScreenSize.X;

            switch (arrowsNormal)
            {
                case 1:
                    for (int i = 0; i < arrows; i++)
                    {
                        sb.Draw(
                        arrowTexture,
                            new Rectangle((scaledRectangle.Location.ToVector2()
                            + new Microsoft.Xna.Framework.Vector2(nextX, (base.Height * (mouseHovering && scaleFactor * 1.5f > 1 ? scaleFactor * 1.5f : 1) - ArrowSize.Y) / scaleFactor)).ToPoint(),
                            new Point(Math.Clamp((int)(ArrowSize.X / scaleFactor), 1, int.MaxValue), Math.Clamp((int)(ArrowSize.Y / scaleFactor), 1, int.MaxValue))), new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height),
                            Microsoft.Xna.Framework.Color.White, 0, Microsoft.Xna.Framework.Vector2.Zero, SpriteEffects.FlipVertically, .5f);
                        nextX += ArrowSize.X / scaleFactor;
                    }
                    break;
                case -1:
                    for (int i = 0; i < Math.Abs(arrows); i++)
                    {
                        sb.Draw(
                        arrowTexture,
                            new Rectangle((scaledRectangle.Location.ToVector2()
                            + new Microsoft.Xna.Framework.Vector2(nextX, (base.Height * (mouseHovering && scaleFactor * 1.5f > 1 ? scaleFactor * 1.5f : 1) - ArrowSize.Y) / scaleFactor)).ToPoint(),
                            new Point(Math.Clamp((int)(ArrowSize.X / scaleFactor), 1, int.MaxValue), Math.Clamp((int)(ArrowSize.Y / scaleFactor), 1, int.MaxValue))),
                            Microsoft.Xna.Framework.Color.White);
                        nextX += ArrowSize.X / scaleFactor;
                    }
                    break;
            }
            nextX += IconSize.X;

        }

        /// <summary>
        /// Returns a clone of a block.
        /// </summary>
        /// <param name="block">The block to clone.</param>
        /// <returns>The clone of the given block.</returns>
        public static Block CloneBlock(Block block)
        {
            return new Block(
                block.Name, 
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
                block.ageRange,
                block.numSpawns,
                block.dependencies,
                block.deathMessage);
        }

        public bool CheckAgainstPlayerStats(Player player)
        {
            return
                HealthRange.IsInRange(player.Health)
                && WealthRange.IsInRange(player.Wealth)
                && HappyRange.IsInRange(player.Happiness)
                && EducationRange.IsInRange(player.Education)
                && AgeRange.IsInRange(player.Age);
        }

        /// <summary>
        /// Reads the dependency string and turns it into an array.
        /// </summary>
        /// <param name="theString">The dependency string.</param>
        /// <returns>The array of dependencies.</returns>
        public static int[] ReadDependencyString(string theString)
        {
            string[] values = theString.Split(',');
            int[] array = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
                array[i] = int.Parse(values[i]);
            return array;
        }

        /// <summary>
        /// Check to see if 2 blocks (or more) have the same values.
        /// </summary>
        /// <param name="other">The other block to check.</param>
        /// <returns>Whether or not the 2 blocks are the same.</returns>
        public bool EqualsOther(List<Block> list)
        {
            foreach (Block other in list)
            {
                if (Name != other.Name ||
                HealthMod != other.HealthMod ||
                EducationMod != other.EducationMod ||
                HappyMod != other.HappyMod ||
                WealthMod != other.WealthMod)
                    return false;
            }
            return true;
        }
    }
}
