using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static ContentImporter.Fonts;
using static Constant;

public class HighScoreState : GameObjectList
{
    protected Button backButton;
    private HandleHighscores handler;
    

    public HighScoreState()
    {
        handler = new HandleHighscores();

        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2((SCREEN_SIZE.X - backButton.Dimensions.X) / 2, SCREEN_SIZE.Y - 70) + new Vector2(30, 0);
        Add(backButton);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        handler.Update(gameTime);
        
        if (backButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_MAINBACKGROUND, new Vector2(0, 0), Color.White);
        base.Draw(gameTime, spriteBatch);
        //All gamestats can be written here
        handler.Draw(gameTime, spriteBatch);
    }
}

