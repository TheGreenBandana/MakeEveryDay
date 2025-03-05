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
    internal class MouseUtils
    {
        // This whole class checks to see if the mouse has been previously pressed or released
        public static bool IsJustPressed(MouseState previous) 
        {
            MouseState ms = Mouse.GetState();
            return (previous.LeftButton == ButtonState.Released && ms.LeftButton == ButtonState.Pressed); 
        }
        public static bool IsJustReleased(MouseState previous)
        {
            MouseState ms = Mouse.GetState();
            return (previous.LeftButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Released);
        }

    }
}
