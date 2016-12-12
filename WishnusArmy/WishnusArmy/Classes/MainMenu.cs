using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

public class MainMenu : GameObjectList
{
    protected Button campaignButton;
    protected Button survivalButton;
    protected Button helpButton;
    protected Button creditsButton;

    public MainMenu()
    {
        //Add campaign button
        campaignButton = new Button("CAMPAIGN", Color.Red);
        campaignButton.Position = new Vector2((GameEnvironment.Screen.X - campaignButton.Dimensions.X)/2, (GameEnvironment.Screen.Y - campaignButton.Dimensions.Y) / 2);
        Add(campaignButton);

        //Add survival button
        survivalButton = new Button("SURVIVAL", Color.Red);
        survivalButton.Position = new Vector2((GameEnvironment.Screen.X - survivalButton.Dimensions.X) / 2, (GameEnvironment.Screen.Y - survivalButton.Dimensions.Y) / 2 + 2 * survivalButton.Dimensions.Y);
        Add(survivalButton);

        //Add help button
        helpButton = new Button("HELP", Color.Red);
        helpButton.Position = new Vector2((GameEnvironment.Screen.X - helpButton.Dimensions.X) / 2, (GameEnvironment.Screen.Y - helpButton.Dimensions.Y) / 2 + 4 * helpButton.Dimensions.Y);
        Add(helpButton);

        //Add credits button
        creditsButton = new Button("CREDITS", Color.Red);
        creditsButton.Position = new Vector2((GameEnvironment.Screen.X - creditsButton.Dimensions.X) / 2, (GameEnvironment.Screen.Y - creditsButton.Dimensions.Y) / 2 + 6 * creditsButton.Dimensions.Y);
        Add(creditsButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (campaignButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("LevelBuilderState");
        }else if (creditsButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("CreditsState");
        }else if (helpButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("HelpState");
        }

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SPR_MAINMENUBACKGROUND, new Vector2(0, 0), Color.Orange);
        base.Draw(gameTime, spriteBatch);
    }
}

