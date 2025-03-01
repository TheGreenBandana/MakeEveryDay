using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class MenuState:GameState
    {
        internal static SpriteFont titleFont = default;
        internal static SpriteFont subtitleFont = default;
        
        public MenuState() { }

        public override State CustomUpdate(GameTime gameTime)
        {

            MouseState ms = Mouse.GetState();
            
            if (ms.LeftButton == ButtonState.Pressed)
            {
                return new GameplayState();
            }

            // This return statement should always come at the end
            // Unless there is a change in state that should happen,
            // it will always return this.
            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(
                titleFont,
                "This is a title\nleft click to start",
                Vector2.One * 10,
                Color.White);
        }


    }
}
