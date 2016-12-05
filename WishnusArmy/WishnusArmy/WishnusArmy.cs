using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;



namespace WishnusArmy
{
    public class WishnusArmy : GameEnvironment
    {
        enum GameStates { Menu, Game, Pause};
        public static Texture2D PIXEL;

        [DllImport("kernel32")]
        static extern bool AllocConsole();

        public WishnusArmy()
        {
            //graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            Mouse.WindowHandle = Window.Handle;
            AllocConsole();
            Console.WriteLine("Hello World");
            DrawingHelper.Initialize(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            base.LoadContent();


            screen = new Point(1920, 1080);
            windowSize = new Point(1920/2, 1080/2);
            ApplyResolutionSettings();
            //FullScreen = true;

            gameStateManager.AddGameState("playingState", new PlayingState(Content));
            gameStateManager.SwitchTo("playingState");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (inputHelper.IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
