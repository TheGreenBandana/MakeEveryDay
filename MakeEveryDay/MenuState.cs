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

        internal static Texture2D blockTexture;

        private Block testBlock;
        
        public MenuState() { }

        public override void Enter()
        {
            testBlock = new Block(
                "test",
                new Vector2(300, 200),
                100);
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            /*
            MouseState ms = Mouse.GetState();
            
            if (ms.LeftButton == ButtonState.Pressed)
            {
                return new GameplayState();
            }
            */

            testBlock.Update(gameTime);

            if (MouseUtils.IsJustPressed() && testBlock.AsRectangle.Contains(MouseUtils.CurrentState.Position.ToVector2()))
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
            

            testBlock.Draw(sb);
        }


    }
}
