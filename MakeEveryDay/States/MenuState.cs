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

        public MenuState() { }

        public override void Enter()
        {
            SoundsUtils.menuMusic.Play();


            playButton = new Button(playButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2, 400, 200));
            debugButton = new Button(debugButtonTexture, new Rectangle((int)Game1.ScreenSize.X - 165, 30, 100, 50));
            quitButton = new Button(quitButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2 +300, 400, 200));
            titleScreen = new GameObject(titleTexture, new Rectangle((int)Game1.ScreenSize.X / 2 - 600, (int)Game1.ScreenSize.Y / 2 - 500, 1200, 450));

            Game1.Width = 1920;

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
            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Paper, new Rectangle(Point.Zero, Game1.ScreenSize.ToPoint()), Color.Wheat);

            playButton.Draw(sb);

            debugButton.Draw(sb);
            quitButton.Draw(sb);
            titleScreen.Draw(sb);

            sb.DrawString(titleFont, "High Scores:", new Vector2(15, 15), Color.Black, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            sb.DrawString(titleFont, scores, new Vector2(15, 15 + titleFont.MeasureString("High Scores:").Y * 1.5f), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
