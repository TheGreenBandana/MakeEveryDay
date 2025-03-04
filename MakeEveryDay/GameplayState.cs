using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeEveryDay
{
    internal class GameplayState:GameState
    {
        internal static SpriteFont defaultText = default;

        private static Vector2 defaultPlayerPosition = new Vector2(100, 100);
        private Vector2 playerPosition;

        private static float playerSpeed = 10f;


        internal static GameObject testObject;


        public GameplayState()
        {
            playerPosition = defaultPlayerPosition;
        }

        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }

        public override State CustomUpdate(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            UpdatePlayer(kb);

            if (kb.IsKeyDown(Keys.Tab))
            {
                return new MenuState();
            }

            return null;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.DrawString(
                defaultText,
                "Me when I play the videogame\npress tab to go back\nwasd to move",
                playerPosition,
                Color.White);

            testObject.Draw(sb);
        }

        /// <summary>
        /// Modifies the player's position according to keyboard input
        /// </summary>
        /// <param name="kb">current keyboard state</param>
        private void UpdatePlayer(KeyboardState kb)
        {
            if (kb.IsKeyDown(Keys.W))
            {
                playerPosition.Y -= playerSpeed;
            }
            if (kb.IsKeyDown(Keys.S))
            {
                playerPosition.Y += playerSpeed;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                playerPosition.X -= playerSpeed;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                playerPosition.X += playerSpeed;
            }
        }
    }
}
