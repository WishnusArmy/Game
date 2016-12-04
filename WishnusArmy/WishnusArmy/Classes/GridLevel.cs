using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GridLayer : GameObjectList
{
    const int LEVEL_SIZE = 5;
    const int NODE_SIZE = 64;
    GridNode[,] grid;
        
    public GridLayer(ContentManager Content)
    {
        //Initialize the grid with the size of the level
        grid = new GridNode[LEVEL_SIZE, LEVEL_SIZE]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y] = new GridNode(Content, new Vector2(NODE_SIZE*x, NODE_SIZE*y));
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //Execute the draw event for every GridItem in the grid
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y].Draw(gameTime, spriteBatch);
            }
        }

        //Draw the grid outlines
        for (int i = 0; i < LEVEL_SIZE + 1; ++i)
        {
            DrawingHelper.DrawLine(spriteBatch, Position + new Vector2(NODE_SIZE*i, 0), Position + new Vector2(NODE_SIZE*i, LEVEL_SIZE * NODE_SIZE), Color.Black, 2, 0.4f);
            DrawingHelper.DrawLine(spriteBatch, Position + new Vector2(0, NODE_SIZE*i), Position + new Vector2(LEVEL_SIZE * NODE_SIZE, NODE_SIZE*i), Color.Black, 2, 0.4f);
        }
    }
}