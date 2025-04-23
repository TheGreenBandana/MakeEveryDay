using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MakeEveryDay.States
{
    internal class GameOverState : State
    {
        private int score;

        private Button exitButton;

        // Score name buttons
        private Button upArrowChar1;
        private Button upArrowChar2;
        private Button upArrowChar3;
        private Button downArrowChar1;
        private Button downArrowChar2;
        private Button downArrowChar3;

        internal static Texture2D arrowButtonTexture;
        internal static string deathMessage;

        private int letter1;
        private int letter2;
        private int letter3;

        public GameOverState(int score)
        {
            this.score = score;
        }

        public override void Enter()
        {
            Game1.Width = (int)Game1.ScreenSize.X;
            exitButton = new Button(MenuState.quitButtonTexture, new Rectangle((int)Game1.ScreenSize.X / 2 - 200, (int)Game1.ScreenSize.Y / 2 + 300, 400, 200));

            upArrowChar1 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3 - 100), (int)(Game1.ScreenSize.Y / 2 - 75), 95, 95));
            upArrowChar2 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3), (int)(Game1.ScreenSize.Y / 2 - 75), 95, 95));
            upArrowChar3 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3 + 100), (int)(Game1.ScreenSize.Y / 2 - 75), 95, 95));
            downArrowChar1 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3 - 100), (int)(Game1.ScreenSize.Y / 2 + 125), 95, 95));
            downArrowChar2 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3), (int)(Game1.ScreenSize.Y / 2 + 125), 95, 95));
            downArrowChar3 = new(arrowButtonTexture, new Rectangle((int)(Game1.ScreenSize.X / 3 + 100), (int)(Game1.ScreenSize.Y / 2 + 125), 95, 95));

            letter1 = 65;
            letter2 = 65;
            letter3 = 65;

            SoundsUtils.deathMusic.Play();
        }
        public override void Exit()
        {
            SoundsUtils.deathMusic.Stop();
            base.Exit();
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            if (exitButton.IsPressed())
            {
                TrySaveScore();
                return new MenuState();
            }

            // Letters for score
            if (downArrowChar1.IsPressed())
            {
                letter1--;
                if (letter1 < 65)
                    letter1 = 90;
            }
            else if (downArrowChar2.IsPressed())
            {
                letter2--;
                if (letter2 < 65)
                    letter2 = 90;
            }
            else if (downArrowChar3.IsPressed())
            {
                letter3--;
                if (letter3 < 65)
                    letter3 = 90;
            }
            else if (upArrowChar1.IsPressed())
            {
                letter1++;
                if (letter1 > 90)
                    letter1 = 65;
            }
            else if (upArrowChar2.IsPressed())
            {
                letter2++;
                if (letter2 > 90)
                    letter2 = 65;
            }
            else if (upArrowChar3.IsPressed())
            {
                letter3++;
                if (letter3 > 90)
                    letter3 = 65;
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.Paper, new Rectangle(Point.Zero, Game1.ScreenSize.ToPoint()), Color.PaleVioletRed);

            sb.DrawString(GameplayState.defaultText, "GAME OVER", new Vector2(
                Game1.ScreenSize.X / 2 - GameplayState.defaultText.MeasureString("GAME OVER").X * 2.5f,
                Game1.ScreenSize.Y / 10), Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            sb.DrawString(GameplayState.defaultText, deathMessage, new Vector2(
                Game1.ScreenSize.X / 2 - GameplayState.defaultText.MeasureString(deathMessage).X,
                Game1.ScreenSize.Y / 3), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            sb.DrawString(GameplayState.defaultText, "Your score: " + score.ToString(), new Vector2(
                Game1.ScreenSize.X / 3 + 200,
                Game1.ScreenSize.Y / 1.95f), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
            exitButton.Draw(sb);

            // Drawing buttons - mostly the same thing with different positions and rotations

            upArrowChar1.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            upArrowChar2.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            upArrowChar3.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.FlipVertically, null);
            downArrowChar1.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.None, null);
            downArrowChar2.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.None, null);
            downArrowChar3.Draw(sb, null, 0, Vector2.Zero, SpriteEffects.None, null);

            // Drawing the letters, which correspond to the buttons
            sb.DrawString(GameplayState.defaultText, ((char)letter1).ToString(), upArrowChar1.Position + new Vector2(15, 100), Color.White, 0, Vector2.Zero, 100 / GameplayState.defaultText.MeasureString(((char)letter1).ToString()).Y, SpriteEffects.None, 0);
            sb.DrawString(GameplayState.defaultText, ((char)letter2).ToString(), upArrowChar2.Position + new Vector2(15, 100), Color.White, 0, Vector2.Zero, 100 / GameplayState.defaultText.MeasureString(((char)letter2).ToString()).Y, SpriteEffects.None, 0);
            sb.DrawString(GameplayState.defaultText, ((char)letter3).ToString(), upArrowChar3.Position + new Vector2(15, 100), Color.White, 0, Vector2.Zero, 100 / GameplayState.defaultText.MeasureString(((char)letter3).ToString()).Y, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Attempt to save a new score.
        /// </summary>
        private void TrySaveScore()
        {
            string name = ((char)letter1).ToString() + ((char)letter2).ToString() + ((char)letter3).ToString();
            
            // Read score lines into array
            string[] scoreLines = new string[10];
            int index = 0;
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
                throw new Exception("Reading high scores failed!");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            // Turning text into numbers
            int[] numbers = new int[10];
            for (int i = 0; i < 10; i++)
                numbers[i] = int.Parse(scoreLines[i].Split(' ')[1]);

            // Find where new score fits
            index = -1;
            for (int i = 0; i < 10; i++)
                if (score > numbers[i])
                {
                    index = i;
                    break;
                }

            // If score fits, overwrite score file
            if (index != -1)
            {
                string newHighScoreText = "";
                for (int i = 0; i < 10; i++)
                {
                    if (i == index)
                    {
                        string number = score.ToString();
                        for (int j = number.Length; j < 9; j++)
                            number = '0' + number;
                        newHighScoreText += name + ": " + number;
                    }
                    else
                    {
                        if (i > index)
                            newHighScoreText += scoreLines[i - 1];
                        else
                            newHighScoreText += scoreLines[i];
                    }
                    if (i < 9)
                        newHighScoreText += '\n';
                }
                StreamWriter writer = null;
                try
                {
                    writer = new(Game1.Path);
                    writer.Write(newHighScoreText);
                }
                catch
                {
                    throw new Exception("highScores.scores couldn't be updated!");
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }
    }
}
