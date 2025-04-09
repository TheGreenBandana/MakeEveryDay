using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay.States
{
    internal class GameOverState : State
    {
        private int score;

        private Button exitButton;

        public GameOverState(int score)
        {
            this.score = score;
        }

        public override void Enter()
        {
            Game1.Width = (int)Game1.ScreenSize.X;
            exitButton = new Button(MenuState.quitButtonTexture, new Rectangle((int)Game1.ScreenSize.X / 2 - 200, (int)Game1.ScreenSize.Y / 2 + 150, 400, 200));
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            if (exitButton.IsPressed())
                return new MenuState();
            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Paper, new Rectangle(Point.Zero, Game1.ScreenSize.ToPoint()), Color.PaleVioletRed);

            sb.DrawString(GameplayState.defaultText, "GAME OVER", new Vector2(
                Game1.ScreenSize.X / 2 - GameplayState.defaultText.MeasureString("GAME OVER").X * 2.5f,
                Game1.ScreenSize.Y / 5), Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            sb.DrawString(GameplayState.defaultText, "Your score: " + score.ToString(), new Vector2(
                Game1.ScreenSize.X / 2 - GameplayState.defaultText.MeasureString("Your score: " + score.ToString()).X * 1.5f,
                Game1.ScreenSize.Y / 1.95f), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
            exitButton.DrawUnscaled(sb);
        }
    }
}
