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

        public static int Width;

        public static Vector2 ScreenSize { get => new Vector2(
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            );
        }

        // Change in Visual Studio to modify bridge position, also changes how objects are scaled on the Y axis
        public static int BridgePosition => (int)ScreenSize.Y / 4 * 3;

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

            //This is intentional, don't want to have any errors on frame one when mouseUtils tries to reference this variable
            MouseUtils.PreviousState = new MouseState();
            MouseUtils.PreviousKBState = new KeyboardState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Setting game to fullscreen at screen's size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here

            // Block class content initialization
            Block.baseBlockTexture = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");
            Block.nameFont = Content.Load<SpriteFont>("Times24");

            Block.arrowTexture = Content.Load<Texture2D>("Icons/StatChangeArrow");
            Block.statIcons[0] = Content.Load<Texture2D>("Icons/HealthStatSmall");
            Block.statIcons[1] = Content.Load<Texture2D>("Icons/HappyStatSmall");
            Block.statIcons[2] = Content.Load<Texture2D>("Icons/SmartStatSmall");
            Block.statIcons[3] = Content.Load<Texture2D>("Icons/MoneyStatSmall");

            // Menu-State content initialization
            MenuState.titleFont = Content.Load<SpriteFont>("Times24");
            MenuState.subtitleFont = Content.Load<SpriteFont>("Times24");

            MenuState.blockTexture = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");

            // Gameplay-state content intitialization
            GameplayState.defaultText = Content.Load<SpriteFont>("Times24");

            // Player class content initialization
            Player.sprite = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");

            // Note: requires content to be loaded, cannot be done in Initialize()
            currentState.Enter();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // DON'T DELETE THESE DON'T DELETE THESE
            MouseUtils.CurrentState = Mouse.GetState();
            MouseUtils.CurrentKBState = Keyboard.GetState();
            // DON'T DELETE THESE DON'T DELETE THESE

            GameState newState = (GameState) currentState.CustomUpdate(gameTime: gameTime);

            if (newState != null)
            {
                ChangeState(newState: newState);
            }

            // DON'T DELETE THESE DON'T DELETE THESE
            MouseUtils.PreviousState = MouseUtils.CurrentState;
            MouseUtils.PreviousKBState = MouseUtils.CurrentKBState;
            // DON'T DELETE THESE DON'T DELETE THESE

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
