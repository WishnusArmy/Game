using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

public class HelpMenu : GameObjectList
{
    protected Button backButton;

    public HelpMenu()
    {
        //Add back button
        backButton = new Button("BACK", Color.Red);
        backButton.Position = new Vector2((GameEnvironment.Screen.X - backButton.Dimensions.X) / 2, (GameEnvironment.Screen.Y - backButton.Dimensions.Y) / 1.25f);
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
        spriteBatch.Draw(SPR_HELPBACKGROUND, new Vector2(0, 0), Color.Blue);
        base.Draw(gameTime, spriteBatch);
    }
}

