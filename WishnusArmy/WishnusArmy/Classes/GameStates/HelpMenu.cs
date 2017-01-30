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
    protected Button nextButton;
    protected Button returnButton;
    protected Texture2D currentHelp;

    public HelpMenu()
    {
        currentHelp = Sprites.SPR_HELP_1;

        //Add back button
        backButton = new Button("BACK", Color.LightGreen, Color.DarkGreen, Fonts.FNT_MENU);
        backButton.Position = new Vector2(1575, SCREEN_SIZE.Y - 125);
        Add(backButton);

        //Add next button
        nextButton = new Button(">", Color.LightGreen, Color.DarkGreen, Fonts.FNT_GAMEOVER);
        nextButton.Position = new Vector2(985, SCREEN_SIZE.Y - 75);
        Add(nextButton);

        //Add return button
        returnButton = new Button("<", Color.LightGreen, Color.DarkGreen, Fonts.FNT_GAMEOVER);
        returnButton.Position = new Vector2(20, SCREEN_SIZE.Y - 75);
        Add(returnButton);

    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        if (backButton.Pressed)
            GameEnvironment.GameStateManager.SwitchTo("MainMenuState");
        else if (nextButton.Pressed && currentHelp == Sprites.SPR_HELP_1)
            currentHelp = Sprites.SPR_HELP_2;
        else if (nextButton.Pressed && currentHelp == Sprites.SPR_HELP_2)
            currentHelp = Sprites.SPR_HELP_3;
        else if (returnButton.Pressed && currentHelp == Sprites.SPR_HELP_2)
            currentHelp = Sprites.SPR_HELP_1;
        else if (returnButton.Pressed && currentHelp == Sprites.SPR_HELP_3)
            currentHelp = Sprites.SPR_HELP_2;

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprites.SPR_MAINBACKGROUND, new Vector2(0, 0), Color.White);
        MainMenu.AddNoise(spriteBatch, 5);

        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(25, 25), new Point(SCREEN_SIZE.X / 2, SCREEN_SIZE.Y - 50)), spriteBatch, Color.Black, 0.7f);
        spriteBatch.Draw(currentHelp, new Vector2(25, 20), Color.White);

        base.Draw(gameTime, spriteBatch);
    }
}

