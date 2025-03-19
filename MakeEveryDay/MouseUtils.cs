using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed); 
        }
        /// <summary>
        /// Determines if the left button specifically was just released this frame
        /// </summary>
        /// <returns>bool representing if the left button was just released</returns>
        public static bool IsJustReleased()
        {
            MouseState ms = Mouse.GetState();
            return (previousState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released);
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
        public static bool KeyJustPressed(Keys key)
        {
            return (previousKBState.IsKeyUp(key) && currentKBState.IsKeyDown(key));
        }

        /// <summary>
        /// detects if a key was just released this frame
        /// </summary>
        /// <param name="key">which key you're testing for</param>
        /// <returns>bool representing if the key was just released</returns>
        public static bool KeyJustReleased(Keys key)
        {
            return (previousKBState.IsKeyDown(key) && currentKBState.IsKeyUp(key));
        }
    }
}
