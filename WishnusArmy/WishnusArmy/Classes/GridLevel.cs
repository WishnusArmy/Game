using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GridLevel : IGameLoopObject
{
    const int LEVEL_SIZE = 5;
    const int GRID_SIZE = 64;
    GridItem[,] grid;
        
    public GridLevel(ContentManager Content)
    {
        //Initialize the grid with the size of the level
        grid = new GridItem[LEVEL_SIZE, LEVEL_SIZE]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y] = new GridItem(Content);
            }
        }
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {

    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Execute the draw event for every GridItem in the grid
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y].Draw(gameTime, spriteBatch, new Vector2(GRID_SIZE*x, GRID_SIZE*y));
            }
        }
    }

    public virtual void Reset()
    {

    }
}