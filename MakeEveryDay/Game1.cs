using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MakeEveryDay
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Game state-related
        private GameState currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            currentState = new MenuState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Menu-State content initialization
            MenuState.titleFont = Content.Load<SpriteFont>("Times24");
            MenuState.subtitleFont = Content.Load<SpriteFont>("Times24");

            // Gameplay-state content intitialization

            GameplayState.defaultText = Content.Load<SpriteFont>("Times24");

            // Note: requires content to be loaded, cannot be done in Initialize()
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            GameState newState = (GameState) currentState.CustomUpdate(gameTime: gameTime);

            if (newState != null)
            {
                ChangeState(newState: newState);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MediumPurple);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            currentState.Draw(sb: _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Called whenever a gameplay state needs to be exchanged for a new state, calls the appropriate enter and exit functions
        /// </summary>
        /// <param name="newState">new GameState to enter</param>
        private void ChangeState(GameState newState)
        {
            // The king is dead
            currentState.Exit();

            currentState = newState;

            // Long live the king
            currentState.Enter();
        }
    }
}
