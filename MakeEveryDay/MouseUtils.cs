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
        public static bool IsJustPressed() 
        {
            return (previousState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed); 
        }
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

        public static bool KeyJustPressed(Keys key)
        {
            return (previousKBState.IsKeyUp(key) && currentKBState.IsKeyDown(key));
        }

        public static bool KeyJustReleased(Keys key)
        {
            return (previousKBState.IsKeyDown(key) && currentKBState.IsKeyUp(key));
        }
    }
}
