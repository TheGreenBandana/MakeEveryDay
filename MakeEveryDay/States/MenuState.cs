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

        private Button titleScreen;
        private Button playButton;

        //private Button blockMakerButton;

        private Button debugButton;
        private Button quitButton;

        private string scores;

        public MenuState() { }

        public override void Enter()
        {
            //blockMakerButton = new Button(blockTexture, new Rectangle(600, 400, 200, 100));

            playButton = new Button(playButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2, 400, 200));
            debugButton = new Button(debugButtonTexture, new Rectangle((int)Game1.ScreenSize.X - 165, 30, 100, 50));
            quitButton = new Button(quitButtonTexture, new Rectangle((int)Game1.ScreenSize.X/2-200, (int)Game1.ScreenSize.Y/2 +300, 400, 200));
            titleScreen = new Button(titleTexture, new Rectangle((int)Game1.ScreenSize.X / 2 - 400, (int)Game1.ScreenSize.Y / 2 - 500, 800, 300));

            Game1.Width = 1920;

            // High score reading
            bool clear = false;
            StreamReader reader = null;
            string[] scoreLines = new string[10];
            int index = 0;

            // Finding/creating the high score file
            // Path cannot be directly in the content folder: Content manager loads a copy, making writing impossible
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "highScores.scores");
            if (!File.Exists(path))
                File.Create(path);

            // Read score lines into array
            try
            {
                reader = new(path);
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
                for (int i = 0; i < 10; i++)
                    text += "###: 000000000\n";
                StreamWriter writer = null;
                try
                {
                    writer = new(path);
                    writer.Write(text);
                }
                catch
                {
                    throw new Exception("highScores.Scores couldn't be overwritten!");
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
                    if (line.Split(' ')[1] != "000000000")
                        scores += line + '\n';
            }

            if (scores == "")
                scores = "No saved scores.";

            scores = path;
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
            /*
            if (blockMakerButton.IsPressed())
            {
                return new GroupMakerState();
            } */
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
