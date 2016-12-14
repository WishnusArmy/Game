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
using WishnusArmy.Classes.Towers;

public class GridPlane: GameObjectList
{
    public GridNode[,] grid;
    public Camera.Plane plane;
        
    public GridPlane(Camera.Plane plane) : base()
    {
        this.plane = plane;
        //Initialize the grid with the size of the level
        grid = new GridNode[LEVEL_SIZE, LEVEL_SIZE]; 
        //Fill the grid with GridItems
        for(int x=0; x<LEVEL_SIZE; ++x)
        {
            for(int y=0; y<LEVEL_SIZE; ++y)
            {
                grid[x, y] = new GridNode(new Vector2(NODE_SIZE*x, NODE_SIZE*y), TEX_GRASS);
                Add(grid[x, y]);
            }
        }
        //ADD LEVEL OBJECTS HERE (AFTER THE GRID)
        
        Add(new Pulse(10, new Vector2(1, 2), new Vector2(300, 300), 300));
        Add(new Bullet(10, new Vector2(5, 5), Vector2.Zero));
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
        for (int i = 0; i < LEVEL_SIZE + 1; ++i)
        {
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(NODE_SIZE * i, 0), GlobalPosition + new Vector2(NODE_SIZE * i, LEVEL_SIZE * NODE_SIZE), Color.Black, 2, 0.05f);
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition + new Vector2(0, NODE_SIZE * i), GlobalPosition + new Vector2(LEVEL_SIZE * NODE_SIZE, NODE_SIZE * i), Color.Black, 2, 0.05f);
        }
    }
}