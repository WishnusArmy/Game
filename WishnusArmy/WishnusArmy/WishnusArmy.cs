using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.InteropServices;
using static Constant;


namespace WishnusArmy
{
    public class WishnusArmy : GameEnvironment
    {
        static bool consoleON = true; //Console is only for testing purposes

        [DllImport("kernel32")]
        static extern bool AllocConsole();

        public static GameEnvironment self;
        public static bool startSorting;

        public WishnusArmy()
        {
            self = this;
            //graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            Mouse.WindowHandle = Window.Handle;
            if(consoleON)
                AllocConsole(); 
            Functions.Initialize(GraphicsDevice);
            ContentImporter.Initialize(Content);
            DrawingHelper.Initialize(GraphicsDevice, Content);
            PopupScreen.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            base.LoadContent();

            screen = SCREEN_SIZE;
            windowSize = WINDOW_SIZE;
            ApplyResolutionSettings();
            FullScreen = false;

            gameStateManager.AddGameState("MainMenuState", new MainMenuState());
            gameStateManager.AddGameState("CreditsState", new CreditsState());
            gameStateManager.AddGameState("HelpState", new HelpState());
            gameStateManager.AddGameState("PlayingState", new PlayingState());
            gameStateManager.AddGameState("LevelBuilderState", new LevelBuilderState());
            gameStateManager.AddGameState("LevelGeneratorState", new LevelGeneratorState());
            gameStateManager.AddGameState("GameOverState", new GameOverState());
            gameStateManager.AddGameState("LeaderBoardState", new LeaderBoardState());
            gameStateManager.SwitchTo("MainMenuState");
            startSorting = true;
        }

        public static void ExitGame()
        {
            self.Exit();
        }
    }
}