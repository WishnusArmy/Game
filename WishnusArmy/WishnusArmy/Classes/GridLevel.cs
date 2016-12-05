using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GridLevel : GameObjectList
{
    const int LEVEL_SIZE = 25;
    const int NODE_SIZE = 64;
    GridNode[,] grid;
        
    public GridLevel(ContentManager content) : base()
    {
        //Initialize the grid with the size of the level
        grid = new GridNode[LEVEL_SIZE, LEVEL_SIZE]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y] = new GridNode(content, new Vector2(NODE_SIZE*x, NODE_SIZE*y));
                grid[x, y].Parent = this;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y].Update(gameTime);
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
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(NODE_SIZE*i, 0), GlobalPosition + new Vector2(NODE_SIZE*i, LEVEL_SIZE * NODE_SIZE), Color.Black, 2, 0.2f);
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(0, NODE_SIZE*i), GlobalPosition + new Vector2(LEVEL_SIZE * NODE_SIZE, NODE_SIZE*i), Color.Black, 2, 0.2f);
        }
    }
}