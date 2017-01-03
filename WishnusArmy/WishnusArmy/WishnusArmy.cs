﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices;
using static Constant;


namespace WishnusArmy
{
    public class WishnusArmy : GameEnvironment
    {
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
            ContentImporter.Initialize(Content);
            DrawingHelper.Initialize(GraphicsDevice);
            Economy.Initialize();
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
            gameStateManager.SwitchTo("LevelGeneratorState");
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