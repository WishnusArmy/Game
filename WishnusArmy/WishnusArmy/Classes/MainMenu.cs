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
    protected Button playButton;

    public MainMenu()
    {
        //Add button
        playButton = new Button(SPR_PLAYBUTTON);
        playButton.Position = new Vector2(100,100);
        Add(playButton);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (playButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("LevelBuilderState");
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SPR_MAINMENUBACKGROUND, new Vector2(0, 0), Color.Orange);
        base.Draw(gameTime, spriteBatch);
    }
}

