using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static Constant;

public class CreditsMenu : GameObjectList
{
    protected Button backButton;
    protected Vector2 planeposition;
    protected Vector2 planespeed;

    public CreditsMenu()
    {
        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2((SCREEN_SIZE.X - backButton.Dimensions.X) / 2, SCREEN_SIZE.Y - 150);
        Add(backButton);

        //Set plane values
        planeposition = new Vector2(SCREEN_SIZE.X, 40);
        planespeed = new Vector2(-5, 0);

    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        if (backButton.Pressed)
        {
            planeposition = new Vector2(SCREEN_SIZE.X, 40);
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
        }

        //Respawn the plane when it reaches the end of the screen
        if (planeposition.X < -Sprites.SPR_CREDITSPLANE.Width)
            planeposition = new Vector2(SCREEN_SIZE.X, 40);
        else
            planeposition += planespeed;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_MAINBACKGROUND, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(Sprites.SPR_CREDITSPLANE, planeposition, Color.White);
        MainMenu.AddNoise(spriteBatch, 5);
        base.Draw(gameTime, spriteBatch);
    }
}

