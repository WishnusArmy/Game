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
        grid = new GridNode[LEVEL_SIZE.X, LEVEL_SIZE.Y]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE.X; ++x)
        {
            for(int y=LEVEL_SIZE.Y-1; y>=0; --y) //Make sure the grid gets build up from the bottom
            {
                if (y % 2 == 0) //All odd rows will be shifted half a node in order to lock together
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
        //SET THE NEIGHBOURS
        for(int x=0; x<LEVEL_SIZE.X; ++x)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; ++y)
            {
                if (y%2==0) //Even rows (4 cases)
                {
                    if (x > 0 && y > 0) { grid[x, y].neighbours.Add(grid[x - 1, y - 1]); } //TopLeft
                    if (y > 0) { grid[x, y].neighbours.Add(grid[x, y - 1]); } //TopRight
                    if (y < LEVEL_SIZE.Y-1) { grid[x, y].neighbours.Add(grid[x , y + 1]); } //BottomRight
                    if (x > 0 && y < LEVEL_SIZE.X - 1) { grid[x, y].neighbours.Add(grid[x - 1, y + 1]); } //BottomLeft
                }
                else //Odd rows (4 cases)
                {
                    if (y > 0) { grid[x, y].neighbours.Add(grid[x, y - 1]); } //TopLeft
                    if (x < LEVEL_SIZE.X - 1 && y > 0) { grid[x, y].neighbours.Add(grid[x + 1, y - 1]); } //TopRight
                    if (x < LEVEL_SIZE.X - 1 && y < LEVEL_SIZE.Y - 1) { grid[x, y].neighbours.Add(grid[x + 1, y + 1]); } //BottomRight
                    if (y < LEVEL_SIZE.Y - 1) { grid[x, y].neighbours.Add(grid[x, y + 1]); } //BottomLeft
                }
            }
        }

        //ADD LEVEL OBJECTS IN THE CAMERA CLASS.
        //ADDING THEM HERE WILL ADD THEM TO EVERY PLANE AL THE SAME
    }

    public GridNode NodeAt(Vector2 pos, bool throwClosest = true) //throwClosest=true will return the closest node if not found
    {
        if (pos.X < 0 || pos.Y < 0)
        {
            throw new Exception("Can't look for nodes at negative coordinates: " + pos.X + ", " + pos.Y);
        }

        float shortDis = float.MaxValue; //Use to find the shortest node.
        GridNode shortNode = null;
 
        for (int x = 0; x < LEVEL_SIZE.X; ++x)
        {
            for (int y = 0; y < LEVEL_SIZE.Y; ++y)
            {
                if (grid[x,y].HoversMe(pos))
                {
                    return grid[x, y];
                }
                float dis = CalculateDistance(grid[x, y].Position, pos); //Store the distance
                if (dis < shortDis)
                {
                    shortDis = dis; //Set new shortDis
                    shortNode = grid[x, y]; //Set new shortNode
                }
            }
        }

        if (throwClosest && shortNode != null)
        {
            return shortNode;
        }
        throw new Exception("No node found at: " + pos.X + ", " + pos.Y);
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
        for (int i = 0; i < (LEVEL_SIZE.X + LEVEL_SIZE.Y); ++i)
        {
            //Right to left
           DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(LEVEL_SIZE.X * NODE_SIZE.X - NODE_SIZE.X * i, 0), GlobalPosition + new Vector2(LEVEL_SIZE.X * NODE_SIZE.X, NODE_SIZE.Y * i), Color.Black, 2, 0.0f);
            //Left to right
           DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(NODE_SIZE.X * i, 0), GlobalPosition + new Vector2(0, NODE_SIZE.Y * i), Color.Black, 2, 0.0f);
        }
    }
}