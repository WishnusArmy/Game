using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

public class CreditsMenu : GameObjectList
{
    protected Button backButton;

    public CreditsMenu()
    {
        //Add back button
        backButton = new Button(SPR_BACKBUTTON);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - SPR_BACKBUTTON.Width)/2, (GameEnvironment.Screen.Y - 3 *SPR_BACKBUTTON.Height));
        Add(backButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
        }

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SPR_CREDITSBACKGROUND, new Vector2(0, 0), Color.Red);
        base.Draw(gameTime, spriteBatch);
    }
}

