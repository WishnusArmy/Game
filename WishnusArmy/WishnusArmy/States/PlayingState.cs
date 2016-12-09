using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class PlayingState : IGameLoopObject
{
    protected List<Level> levels;
    protected int currentLevel;

    public PlayingState()
    {
        currentLevel = 0;
        levels = new List<Level>();
        levels.Add(new Level());
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        levels[currentLevel].HandleInput(inputHelper);
    }

    public virtual void Reset()
    {

    }

    public virtual void Update(GameTime gameTime)
    {
        levels[currentLevel].Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        levels[currentLevel].Draw(gameTime, spriteBatch);
    }
}