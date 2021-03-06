﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Constant;

public class GridLevel : GameObjectList
{
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
                grid[x, y].Parent = this; //Note that the grid array is not part of the standard children list
                                          //and must therefore be handled manually
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
                grid[x, y].Update(gameTime); //grid is seperate from children list
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
                grid[x, y].Draw(gameTime, spriteBatch); //Grid is seperate from children list
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