using static ContentImporter.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HelpState : IGameLoopObject
{
    HelpMenu help;

    public HelpState()
    {
        help = new HelpMenu();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        help.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        help.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        help.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}