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
        backButton.Position = new Vector2(1575, SCREEN_SIZE.Y - 125);
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

        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(25, 25), new Point(SCREEN_SIZE.X / 2, SCREEN_SIZE.Y - 50)), spriteBatch, Color.Black, 0.7f);
        
        //first column
        spriteBatch.DrawString(Fonts.FNT_GAMEOVER, "CREDITS", new Vector2(50, 40), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "TEAM MANAGER", new Vector2(55, 125), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter ubbink", new Vector2(55, 165), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEAD PROGRAMMER", new Vector2(55, 205), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "hugo peters", new Vector2(55, 245), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "ORIGINAL STORY", new Vector2(55, 285), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wishnus army", new Vector2(55, 325), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "ISOMETRIC ARTISTS", new Vector2(55, 365), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "abel van beek", new Vector2(55, 405), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "opengameart.org", new Vector2(55, 445), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "WEB DESIGN", new Vector2(55, 485), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "abel van beek", new Vector2(55, 525), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "jorden bakker", new Vector2(55, 565), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "moesia theme", new Vector2(55, 605), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wordpress", new Vector2(55, 645), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "MUSIC AND SOUND F/X", new Vector2(55, 685), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter castelein", new Vector2(55, 725), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "jorden bakker", new Vector2(55, 765), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEAD ARTIST", new Vector2(55, 805), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "abel van beek", new Vector2(55, 845), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "PROGRAMMING TOOLS", new Vector2(55, 885), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "microsoft visual studio", new Vector2(55, 925), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "CATERING", new Vector2(55, 965), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "sticky", new Vector2(55, 1005), Color.White);

        //Second column
        spriteBatch.DrawString(Fonts.FNT_MENU, "EXTERNAL ADVISOR", new Vector2(560, 45), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "amir vaxman", new Vector2(560, 85), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "PROJECT MANAGERS", new Vector2(560, 125), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "malte lorbach", new Vector2(560, 165), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "roland geraerts", new Vector2(560, 205), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "paul bergervoet", new Vector2(560, 245), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "WRITERS", new Vector2(560, 285), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "jorden bakker", new Vector2(560, 325), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "abel van beek", new Vector2(560, 365), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter castelein", new Vector2(560, 405), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "gerrit paalman", new Vector2(560, 445), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "hugo peters", new Vector2(560, 485), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter ubbink", new Vector2(560, 525), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "maurin voshol", new Vector2(560, 565), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEAD TOWER DESIGNERS", new Vector2(560, 605), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter castelein", new Vector2(560, 645), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "maurin voshol", new Vector2(560, 685), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEAD GAME DESIGNER", new Vector2(560, 725), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "hugo peters", new Vector2(560, 765), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEAD OVERSLEEPER", new Vector2(560, 805), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "maurin voshol", new Vector2(560, 845), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "LEVEL GENERATOR", new Vector2(560, 885), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "gerrit paalman", new Vector2(560, 925), Color.White);

        spriteBatch.DrawString(Fonts.FNT_MENU, "BALANCING", new Vector2(560, 965), Color.White);
        spriteBatch.DrawString(Fonts.FNT_MENU, "wouter ubbink", new Vector2(560, 1005), Color.White);

        base.Draw(gameTime, spriteBatch);
    }
}

