using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class LevelBuilderState : IGameLoopObject
{
    Level level;
    Hud Hud = new Hud();

    public LevelBuilderState()
    {
        level = new Level();
        level.Add(new LevelBuilder()); //Add the HUD to the level;
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        level.HandleInput(inputHelper);
    }

    public virtual void Reset()
    {

    }

    public virtual void Update(GameTime gameTime)
    {
        level.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
        Hud.Draw(gameTime, spriteBatch);
    }
}