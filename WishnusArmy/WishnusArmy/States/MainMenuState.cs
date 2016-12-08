using static ContentImporter.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MainMenuState : IGameLoopObject
{
    MainMenu menu;

    public MainMenuState()
    {
        menu = new MainMenu();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        menu.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        menu.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        menu.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}