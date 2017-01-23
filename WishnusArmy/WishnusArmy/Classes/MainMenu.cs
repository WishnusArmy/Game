using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static Constant;
using Microsoft.Xna.Framework.Media;

public class MainMenu : GameObjectList
{
    protected Button playButton;
    protected Button helpButton;
    protected Button creditsButton;

    protected Vector2 buttonPosition;
    protected Color buttonColor;
    protected Color hoverColor;
    protected SpriteFont buttonFont;

    public MainMenu()
    {
        buttonPosition = new Vector2(1275, SCREEN_SIZE.Y - 125);
        buttonColor = Color.LightGreen;
        hoverColor = Color.DarkGreen;
        buttonFont = Fonts.FNT_MENU;

        //Add campaign button
        playButton = new Button("PLAY", buttonColor, hoverColor, buttonFont);
        playButton.Position = buttonPosition;
        Add(playButton);

        //Add help button
        helpButton = new Button("HELP", buttonColor, hoverColor, buttonFont);
        helpButton.Position = new Vector2(buttonPosition.X + playButton.Dimensions.X + 70, buttonPosition.Y);
        buttonPosition = helpButton.Position;
        Add(helpButton);

        //Add credits button
        creditsButton = new Button("CREDITS", buttonColor, hoverColor, buttonFont);
        creditsButton.Position = new Vector2(buttonPosition.X + helpButton.Dimensions.X + 90, buttonPosition.Y);
        Add(creditsButton);        
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        if (playButton.Pressed)
        {
            GameEnvironment.GameStateManager.AddGameState("PlayingState", new PlayingState());
            GameEnvironment.GameStateManager.SwitchTo("PlayingState");
            GameEnvironment.GameStateManager.SwitchTo("GameOverState");
        }
        else if (creditsButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("CreditsState");
        else if (helpButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("HelpState");

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_MAINBACKGROUND, new Vector2(0, 0), Color.White);
        AddNoise(spriteBatch, 5);
        base.Draw(gameTime, spriteBatch);
    }

    public static void AddNoise(SpriteBatch spriteBatch, int noiseAmount)
    {

        for (int a = 0; a <= noiseAmount; a++)
        {
            for (int i = 0; i <= SCREEN_SIZE.X; i++)
            {
                spriteBatch.Draw(Sprites.SPR_WHITEPIXEL, new Vector2(i + RANDOM.Next(-5, 5), RANDOM.Next(SCREEN_SIZE.Y)), null, null, null, (float)RANDOM.NextDouble() * 2f, null, Color.Gray);
            }
        }
    }
}

