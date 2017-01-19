using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static Constant;

public class HelpMenu : GameObjectList
{
    protected Button backButton;

    public HelpMenu()
    {
        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2((SCREEN_SIZE.X - backButton.Dimensions.X) / 2, SCREEN_SIZE.Y - 150);
        Add(backButton);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
        }

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_MAINBACKGROUND, new Vector2(0, 0), Color.Blue);
        MainMenu.AddNoise(spriteBatch, 5);
        base.Draw(gameTime, spriteBatch);
    }
}

