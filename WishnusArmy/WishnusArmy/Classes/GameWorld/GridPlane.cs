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

public class GridPlane: GameObjectList
{
    public GridNode[,] grid;
    public Camera.Plane planeType;
        
    public GridPlane(Camera.Plane planeType) : base()
    {
        this.planeType = planeType;
        //Initialize the grid with the size of the level
        grid = new GridNode[LEVEL_SIZE, LEVEL_SIZE]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                if (y % 2 == 0)
                {
                    grid[x, y] = new GridNode(planeType, new Vector2(NODE_SIZE.X * x, NODE_SIZE.Y/2 * y), 0);
                }
                else
                {
                    grid[x, y] = new GridNode(planeType, new Vector2(NODE_SIZE.X * x + NODE_SIZE.X/2, NODE_SIZE.Y/2 * y), 0);
                }
                Add(grid[x, y]);
            }
        }

        //ADD LEVEL OBJECTS IN THE CAMERA CLASS.
        //ADDING THEM HERE WILL ADD THEM TO EVERY PLANE AL THE SAME
    }

    public GridNode NodeAt(Vector2 pos)
    {
        for (int x = 0; x < LEVEL_SIZE; ++x)
        {
            for (int y = 0; y < LEVEL_SIZE; ++y)
            {
                if (grid[x,y].HoversMe(pos))
                {
                    return grid[x, y];
                }
            }
        }
        return null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //Draw the grid outlines
        for (int i = 0; i < LEVEL_SIZE * 2; ++i)
        {
            //Right to left
           DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(LEVEL_SIZE * NODE_SIZE.X - NODE_SIZE.X * i, 0), GlobalPosition + new Vector2(LEVEL_SIZE * NODE_SIZE.X, NODE_SIZE.Y * i), Color.Black, 2, 0.0f);
            //Left to right
           DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(NODE_SIZE.X * i, 0), GlobalPosition + new Vector2(0, NODE_SIZE.Y * i), Color.Black, 2, 0.0f);
        }
    }
}