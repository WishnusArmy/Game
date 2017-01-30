using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter;
using static ContentImporter.Fonts;
using static Constant;

public class GameOverState : IGameLoopObject
{
    GameOver gameOver;

    public GameOverState()
    {
        gameOver = new GameOver();
    }

    public void Update(object gameTime)
    {
        gameOver.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        gameOver.Draw(gameTime, spriteBatch);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        gameOver.HandleInput(inputHelper);
    }

    public void Reset()
    {
        gameOver.Reset();
    }
}

