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
        internal static Texture2D playButtonTexture;
        internal static Texture2D quitButtonTexture;
        internal static Texture2D titleTexture;
        internal static Texture2D debugButtonTexture;

        private Button titleScreen;
        private Button playButton;

        private Button blockMakerButton;

        private Button debugButton;
        private Button quitButton;

        public MenuState() { }

        public override void Enter()
        {

            playButton = new Button(playButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2, 400, 200));
            debugButton = new Button(debugButtonTexture, new Rectangle((int)Game1.ScreenSize.X - 165, 30, 100, 50));
            quitButton = new Button(quitButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2 +300, 400, 200));
            titleScreen = new Button(titleTexture, new Rectangle((int)Game1.ScreenSize.X / 2 - 400, (int)Game1.ScreenSize.Y / 2 - 500, 800, 300));


            Game1.Width = (int)Game1.ScreenSize.X;
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            if (quitButton.IsPressed())
            {
                game1Reference.ExitGame();
            }
            if (playButton.IsPressed())
            {
                return new GameplayState(false);
            }
            if (debugButton.IsPressed())
            {
                return new GameplayState(true);
            }
            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Paper, new Rectangle(Point.Zero, Game1.ScreenSize.ToPoint()), Color.Wheat);

            playButton.Draw(sb);

            debugButton.Draw(sb);
            quitButton.Draw(sb);
            titleScreen.Draw(sb);
        }
    }
}
