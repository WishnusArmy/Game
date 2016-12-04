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
    protected ContentManager content;

    public PlayingState(ContentManager content)
    {
        this.content = content;
        currentLevel = 0;
        levels = new List<Level>();
        
        levels.Add(new Level(content));
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {

    }

    public virtual void Reset()
    {

    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        for(int i=0; i<levels.Count; ++i)
        {
            levels[i].Draw(gameTime, spriteBatch);
        }
    }
}