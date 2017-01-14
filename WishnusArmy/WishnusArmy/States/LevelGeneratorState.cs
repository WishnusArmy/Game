using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Constant;
using static ContentImporter.Textures;

public class LevelGeneratorState : IGameLoopObject
{
    Level level;

    public LevelGeneratorState()
    {
        level = new Level();
        level.Add(new LevelGenerator());
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        level.HandleInput(inputHelper);
    }

    public virtual void Reset()
    {

    }

    public virtual void Update(object gameTime)
    {
        level.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        level.Draw(gameTime, spriteBatch);
    }
}