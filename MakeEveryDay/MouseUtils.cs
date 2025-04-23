using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MakeEveryDay.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    internal class MouseUtils // Class might be better as a singleton
    {
        private static MouseState previousState;
        private static MouseState currentState;

        public static MouseState PreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }

        public static MouseState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        // This whole class checks to see if the mouse has been previously pressed or released
        /// <summary>
        /// Determines if the left button specifically was just pressed this frame
        /// </summary>
        /// <returns>bool representing if the left button was just pressed</returns>
        public static bool IsJustPressed() 
        {
            return (previousState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && currentState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed); 
        }
        /// <summary>
        /// Determines if the left button specifically was just released this frame
        /// </summary>
        /// <returns>bool representing if the left button was just released</returns>
        public static bool IsJustReleased()
        {
            return (previousState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && currentState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released);
        }

        // Gonna throw in some keyboard stuff here too

        private static KeyboardState previousKBState;
        private static KeyboardState currentKBState;

        public static KeyboardState PreviousKBState
        {
            get { return previousKBState; }
            set { previousKBState = value; }
        }
        public static KeyboardState CurrentKBState
        {
            get { return currentKBState; }
            set { currentKBState = value; }
        }
        
        /// <summary>
        /// detects if a key started being pressed this frame
        /// </summary>
        /// <param name="key">which key you're testing for</param>
        /// <returns>bool representing if the key was just pressed</returns>
        public static bool KeyJustPressed(Microsoft.Xna.Framework.Input.Keys key)
        {
            return (previousKBState.IsKeyUp(key) && currentKBState.IsKeyDown(key));
        }

        /// <summary>
        /// detects if a key was just released this frame
        /// </summary>
        /// <param name="key">which key you're testing for</param>
        /// <returns>bool representing if the key was just released</returns>
        public static bool KeyJustReleased(Microsoft.Xna.Framework.Input.Keys key)
        {
            return (previousKBState.IsKeyDown(key) && currentKBState.IsKeyUp(key));
        }

        /// <summary>
        /// Puts the screen space mouse position into world space.
        /// </summary>
        /// <returns>The mouse position in world space.</returns>
        public static Point ScaleMousePosition(Point realMousePosition)
        {
            return new Point((int)(realMousePosition.X * (Game1.Width / Game1.ScreenSize.X)),
                (int)(realMousePosition.Y * (Game1.Width / Game1.ScreenSize.X))
                );
        }

        /// <summary>
        /// Offset's the given point by the cursor's height.
        /// </summary>
        /// <param name="realMousePositon">The point to offset.</param>
        /// <returns>The offset point.</returns>
        public static Point OffsetMousePosition(Point realMousePositon)
        {
            return realMousePositon + new Point(0, Cursors.Arrow.Size.Height / 4);
        }

        /// <summary>
        /// Literally just for playing noises, called once in the update function of game1
        /// </summary>
        public static void Update()
        {
            if (IsJustPressed()) SoundsUtils.clickedBlockSound.Play(volume: SoundsUtils.mouseClickVolume, pitch: 0, 0);
            if (IsJustReleased()) SoundsUtils.connectedBlockSound.Play(volume:SoundsUtils.mouseClickVolume, pitch: 0, 0);
        }

    }
}
