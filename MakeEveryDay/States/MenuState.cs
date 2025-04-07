using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal class MenuState : State
    {
        internal static SpriteFont titleFont = default;
        internal static SpriteFont subtitleFont = default;

        internal static Texture2D blockTexture;

        private Block testBlock;

        private Button playButton;

        private Button blockMakerButton;

        public MenuState() { }

        public override void Enter()
        {
            playButton = new Button(blockTexture, new Rectangle(300, 200, 200, 100));

            blockMakerButton = new Button(blockTexture, new Rectangle(600, 400, 200, 100));

            testBlock = new Block(
                "test",
                new Vector2(300, 200),
                100);

            Game1.Width = (int)Game1.ScreenSize.X;

        }

        public override State CustomUpdate(GameTime gameTime)
        {
            if (playButton.IsPressed())
            {
                return new GameplayState();
            }
            if (blockMakerButton.IsPressed())
            {
                return new GroupMakerState();
            }
            return null;

            /*
            MouseState ms = Mouse.GetState();
            
            if (ms.LeftButton == ButtonState.Pressed)
            {
                return new GameplayState();
            }
            */

            //testBlock.Update(gameTime);

            /*
            if (MouseUtils.IsJustPressed() && testBlock.AsRectangle.Contains(MouseUtils.CurrentState.Position.ToVector2()))
            {
                return new GameplayState();
            }

            // This return statement should always come at the end
            // Unless there is a change in state that should happen,
            // it will always return this.
            return null;
            */
        }

        public override void Draw(SpriteBatch sb)
        {
            playButton.Draw(sb);
            sb.DrawString(
                titleFont,
                "This is a title\nleft click to start",
                Vector2.One * 10,
                Color.White);

            blockMakerButton.Draw(sb);

            //testBlock.Draw(sb);
        }

        /// <summary>
        /// Exit override - Blank to avoid error from base state class exit method
        /// </summary>
        public override void Exit()
        {
            
        }

    }
}
