using static ContentImporter.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LeaderBoardState : IGameLoopObject
{
    LeaderBoard leaderBoard;

    public LeaderBoardState()
    {
        leaderBoard = new LeaderBoard();
    }

    public void HandleInput(InputHelper inputHelper)
    {
        leaderBoard.HandleInput(inputHelper);
    }

    public void Update(object gameTime)
    {
        leaderBoard.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        leaderBoard.Draw(gameTime, spriteBatch);
    }

    public void Reset()
    {
    }
}