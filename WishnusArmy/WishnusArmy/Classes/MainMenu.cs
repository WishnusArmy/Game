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
        campaignButton = new Button(SPR_CAMPAIGNBUTTON);
        campaignButton.Position = new Vector2((GameEnvironment.Screen.X - SPR_CAMPAIGNBUTTON.Width)/2, (GameEnvironment.Screen.Y - SPR_CAMPAIGNBUTTON.Height) / 2);
        Add(campaignButton);

        //Add survival button
        survivalButton = new Button(SPR_SURVIVALBUTTON);
        survivalButton.Position = new Vector2((GameEnvironment.Screen.X - SPR_SURVIVALBUTTON.Width) / 2, (GameEnvironment.Screen.Y - SPR_SURVIVALBUTTON.Height) / 2 + 2 * SPR_SURVIVALBUTTON.Height);
        Add(survivalButton);

        //Add help button
        helpButton = new Button(SPR_HELPBUTTON);
        helpButton.Position = new Vector2((GameEnvironment.Screen.X - SPR_HELPBUTTON.Width) / 2, (GameEnvironment.Screen.Y - SPR_HELPBUTTON.Height) / 2 + 4 * SPR_SURVIVALBUTTON.Height);
        Add(helpButton);

        //Add credits button
        creditsButton = new Button(SPR_CREDITSBUTTON);
        creditsButton.Position = new Vector2((GameEnvironment.Screen.X - SPR_CREDITSBUTTON.Width) / 2, (GameEnvironment.Screen.Y - SPR_CREDITSBUTTON.Height) / 2 + 6 * SPR_SURVIVALBUTTON.Height);
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

