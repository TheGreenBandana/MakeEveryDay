using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

        private GameObject titleScreen;
        private Button playButton;

        private Button blockMakerButton;

        private Button debugButton;
        private Button quitButton;

        private string scores;


        internal static Texture2D arrowButtonTexture;
        private Button upMusicVol;
        private Button downMusicVol;
        private Button upSFXVol;
        private Button downSFXVol;
        private Button upClickVol;
        private Button downClickVol;

        public MenuState() { }

        public override void Enter()
        {
            SoundsUtils.menuMusic.Play();


            playButton =    new Button(playButtonTexture,   new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2, 400, 200));
            debugButton =   new Button(debugButtonTexture,  new Rectangle((int)Game1.ScreenSize.X - 165, 30, 100, 50));
            quitButton =    new Button(quitButtonTexture,   new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2 +300, 400, 200));


            Point titleSize = new Point(1200, 450);
            titleScreen =   new GameObject(titleTexture, new Rectangle((int)Game1.ScreenSize.X/2 - titleSize.X/2, (int)Game1.ScreenSize.Y / 2 - titleSize.Y, titleSize.X, titleSize.Y));

            int hOffset =       (int) (Game1.ScreenSize.X) / 192;
            int vOffset =       (int) (Game1.ScreenSize.Y) / 6;
            int buttonSize =    (int) (Game1.ScreenSize.X) / (192/5);

            upMusicVol =    new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize)       - vOffset,  buttonSize, buttonSize));
            downMusicVol =  new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize*2)     - vOffset,  buttonSize, buttonSize));
            upSFXVol =      new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize*3.5)   - vOffset,  buttonSize, buttonSize));
            downSFXVol =    new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize*4.5)   - vOffset,  buttonSize, buttonSize));
            upClickVol =    new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize*6)     - vOffset,  buttonSize, buttonSize));
            downClickVol =  new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / hOffset), (int)(2 * Game1.ScreenSize.Y / 3 + buttonSize*7)     - vOffset,  buttonSize, buttonSize));

            Game1.Width = (int)Game1.ScreenSize.X;

            // High score reading
            bool clear = false;
            string[] scoreLines = new string[10];
            int index = 0;

            // Read score lines into array
            StreamReader reader = null;
            try
            {
                reader = new(Game1.Path);
                while (!reader.EndOfStream)
                {
                    scoreLines[index] = reader.ReadLine();
                    index++;
                }
            }
            catch
            {
                clear = true;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            if (index < 9)
                clear = true;

            scores = "";

            // If requried, clear the scores file
            if (clear)
            {
                string text = "";
                for (int i = 0; i < 9; i++)
                    text += "###: 000000000\n";
                text += "###: 000000000";
                StreamWriter writer = null;
                try
                {
                    writer = new(Game1.Path);
                    writer.Write(text);
                }
                catch (Exception e)
                {
                    throw new Exception("highScores.scores couldn't be reset!");
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }

            // Preparing for printing
            else
            {
                foreach (string line in scoreLines)
                    if (line.Split(' ')[0] != "###:")
                        scores += line + '\n';
            }

            if (scores == "")
                scores = "No saved scores.";
        }
        public override void Exit()
        {
            SoundsUtils.menuMusic.Stop();
            base.Exit();
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

            { // Volume adjusters
                if (upMusicVol.IsPressed())
                {
                    SoundsUtils.MusicVolume += .1f;
                }
                if (downMusicVol.IsPressed())
                {
                    SoundsUtils.MusicVolume -= .1f;
                }
                if (upSFXVol.IsPressed())
                {
                    SoundsUtils.SFXVolume += .1f;
                }
                if (downSFXVol.IsPressed())
                {
                    SoundsUtils.SFXVolume -= .1f;
                }
                if (upClickVol.IsPressed())
                {
                    SoundsUtils.ClickVolume += .1f;
                }
                if (downClickVol.IsPressed())
                {
                    SoundsUtils.ClickVolume -= .1f;
                }
            }

            if (MouseUtils.KeyJustPressed(Keys.G))
            {
                GameOverState.deathMessage = "DEBUG MODE ACTIVATED. WELCOME TO HELL MORTAL";
                return new GameOverState(0);
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

            upMusicVol.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            downMusicVol.Draw(sb);
            upSFXVol.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            downSFXVol.Draw(sb);
            upClickVol.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            downClickVol.Draw(sb);

            sb.DrawString(
                DefaultGameFont,
                ((int)((SoundsUtils.MusicVolume * 10))).ToString(),
                new Vector2(upMusicVol.Right + upMusicVol.Size.X, upMusicVol.Top - 10),
                Color.Black,
                0,
                Vector2.Zero,
                3,
                SpriteEffects.None,
                0);

            sb.DrawString(
                DefaultGameFont,
                ((int)((SoundsUtils.SFXVolume * 10))).ToString(),
                new Vector2(upSFXVol.Right + upMusicVol.Size.X, upSFXVol.Top - 10),
                Color.Black,
                0,
                Vector2.Zero,
                3,
                SpriteEffects.None,
                0);

            sb.DrawString(
                DefaultGameFont,
                ((int)((SoundsUtils.ClickVolume * 10))).ToString(),
                new Vector2(upClickVol.Right + upMusicVol.Size.X, upClickVol.Top - 10),
                Color.Black,
                0,
                Vector2.Zero,
                3,
                SpriteEffects.None,
                0);

            sb.DrawString(DefaultGameFont, "Music\nVolume", upMusicVol.Position - DefaultGameFont.MeasureString("Music\nVolume") * new Vector2(1.25f, 0), Color.Black);
            sb.DrawString(DefaultGameFont, "SFX\nVolume", upSFXVol.Position - DefaultGameFont.MeasureString("SFX\nVolume") * new Vector2(1.25f, 0), Color.Black);
            sb.DrawString(DefaultGameFont, "Click\nVolume", upClickVol.Position - DefaultGameFont.MeasureString("Click\nVolume") * new Vector2(1.25f, 0), Color.Black);


            sb.DrawString(titleFont, "High Scores:", new Vector2(15, 60), Color.Black, 0, Vector2.Zero, .45f, SpriteEffects.None, 0);
            sb.DrawString(titleFont, scores, new Vector2(15, 60 + titleFont.MeasureString("High Scores:").Y * .45f), Color.Black, 0, Vector2.Zero, .45f, SpriteEffects.None, 0);

        }
    }
}
