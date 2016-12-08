using static ContentImporter.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CreditsState : IGameLoopObject
{
    CreditsMenu credits;

    public CreditsState()
    {
        credits = new CreditsMenu();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        credits.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        credits.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        credits.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}