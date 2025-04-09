using MakeEveryDay.States;
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
        private State currentState;

        public static int Width;

        public static bool toggleKyle;

        private static Texture2D paper;

        /// <summary>
        /// Stores current clicked object at any given time
        /// </summary>
        internal static GameObject clickedObject;

        public static Vector2 ScreenSize { get => new Vector2(
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height
            );
        }

        // Change in Visual Studio to modify bridge position, also changes how objects are scaled on the Y axis
        public static int BridgePosition => (int)ScreenSize.Y / 4 * 3;
        public static Texture2D Paper => paper;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            toggleKyle = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            currentState = new MenuState();

            //This is intentional, don't want to have any errors on frame one when mouseUtils tries to reference this variable
            MouseUtils.PreviousState = new MouseState();
            MouseUtils.PreviousKBState = new KeyboardState();

            DrawLayerUtils.InitializeDrawLayerUtils();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Setting game to fullscreen at screen's size
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // LEAVE FALSE WHILE DEVELOPING - UNHANDLED EXCEPTIONS WHILE FULLSCREENED CAUSE FLASHBANG
            _graphics.IsFullScreen = false;
            // LEAVE FALSE WHILE DEVELOPING - UNHANDLED EXCEPTIONS WHILE FULLSCREENED CAUSE FLASHBANG

            _graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here

            // Background initialization
            paper = Content.Load<Texture2D>("paper");

            // Base "state" content initialization
            State.DefaultGameFont = Content.Load<SpriteFont>("Times24");
            State.DefaultGameTexture = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");
            State.game1Reference = this;

            // GameObject class content initialization
            GameObject.gameObjectDefaultTexture = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");

            // Block class content initialization
            Block.baseBlockTexture = Content.Load<Texture2D>("Block");
            Block.nameFont = Content.Load<SpriteFont>("Times24");

            Block.arrowTexture = Content.Load<Texture2D>("Icons/StatChangeArrow");
            Block.statIcons[0] = Content.Load<Texture2D>("Icons/HealthStatSmall");
            Block.statIcons[1] = Content.Load<Texture2D>("Icons/HappyStatSmall");
            Block.statIcons[2] = Content.Load<Texture2D>("Icons/SmartStatSmall");
            Block.statIcons[3] = Content.Load<Texture2D>("Icons/MoneyStatSmall");

            // Menu-State content initialization
            MenuState.titleFont = Content.Load<SpriteFont>("Times24");
            MenuState.subtitleFont = Content.Load<SpriteFont>("Times24");
            MenuState.playButtonTexture = Content.Load<Texture2D>("Playbutton");
            MenuState.quitButtonTexture = Content.Load<Texture2D>("Quitbutton");
            MenuState.titleTexture = Content.Load<Texture2D>("TitleScreenTitle");

            MenuState.blockTexture = Content.Load<Texture2D>("WIN_20191225_10_46_57_Pro (2)");

            // Gameplay-state content intitialization
            GameplayState.defaultText = Content.Load<SpriteFont>("Times24");

            // Player class content initialization
            Player.Running = new AnimationState(Content.Load<Texture2D>("LilGuyRun"), 6, true, 12);
            Player.Fall = Content.Load<Texture2D>("LilGuyGoDeath");
            Player.Trip = Content.Load<Texture2D>("LilGuyEatShit");

            // Status bar class content initialization
            StatusBar.sprite = Content.Load<Texture2D>("Status Bar");

            // Block Group class content initialization
            BlockGroup.nameFont = Content.Load<SpriteFont>("Times24");

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

            State newState = currentState.CustomUpdate(gameTime: gameTime);

            if (newState != null)
            {
                ChangeState(newState: newState);
            }

            if (MouseUtils.KeyJustPressed(Keys.K))
            {
                toggleKyle = !toggleKyle;
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
        private void ChangeState(State newState)
        {
            // The king is dead
            currentState.Exit();

            currentState = newState;

            // Long live the king
            currentState.Enter();
        }

        public void ExitGame()
        {
            Exit(); 
        }
    }
}
