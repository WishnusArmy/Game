using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static Constant;
using Microsoft.Xna.Framework.Graphics;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public enum Plane { Land, Air };
    public GridPlane currentPlane;
    public GridPlane Land, Air;
    List<GridPlane> planes;

    public Camera() : base()
    {
        planes = new List<GridPlane>();
        for (int i=0; i<2; ++i)
        { 
            GridPlane p = new GridPlane((Plane)i);
            p.active = false;
            Add(p);
            switch((Plane)i)
            {
                case Plane.Land:
                    Land = p;
                    planes.Add(Land);
                    //Add items to the land plane (p.Add)
                    p.Add(new Base { Position = LEVEL_CENTER });
                    //(testcode) plaatst torens en voegt een enemy toe
                    for (int q = 0; q < 1; ++q)
                    {
                        GridNode node = Land.grid[0, LEVEL_SIZE.Y / 2];
                        p.Add(new Tank { startNode = node, Position = node.Position });
                        
                    }
                    break;

                case Plane.Air:
                    Air = p;
                    planes.Add(Air);
                    //Add items to the air plane (p.Add)
                    break;
            }
        }
        currentPlane = planes[(int)Plane.Land]; //Reference the current plane to one of the three
        Console.WriteLine("Current Plane: " + currentPlane.planeType.ToString());
        LevelGenerator levelGenerator = new LevelGenerator();
        List<int[,]> list = new List<int[,]>();
        list = levelGenerator.GenerateNewLevel();
        for(int x=0; x<LEVEL_SIZE.X; ++x)
        {
            for(int y=0; y<LEVEL_SIZE.Y; ++y)
            {
                Land.grid[x, y].texture = list[0][x,y];
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //Manually handle the updates because the planes are inactive.
        for(int i=0; i<planes.Count; ++i)
        {
            planes[i].Update(gameTime);
        }

        if (RANDOM.Next(80) == 0)
        {
            GridNode node = Land.grid[0, RANDOM.Next(LEVEL_SIZE.Y)];
            Land.Add(new Tank { startNode = node, Position = node.Position });
            Land.Add(new Airplane { startNode = node, Position = node.Position });
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        //Only draw the active plane;
        currentPlane.Draw(gameTime, spriteBatch);
}


    public override void HandleInput(InputHelper inputHelper)
    {
        //Manually handle the input of the active plane
        currentPlane.HandleInput(inputHelper);

        //Camera Movement
        Vector2 mp = inputHelper.MousePosition;
        if (mp.X < SLIDE_BORDER)
            position.X += SLIDE_SPEED;
        if (mp.X > SCREEN_SIZE.X - SLIDE_BORDER)
            position.X -= SLIDE_SPEED;
        if (mp.Y < SLIDE_BORDER)
            position.Y += SLIDE_SPEED;
        if (mp.Y > SCREEN_SIZE.Y - SLIDE_BORDER)
            position.Y -= SLIDE_SPEED;

        //Make sure the camera doesn't move out of bounds
        if (position.X > -NODE_SIZE.X/2 - GridNode.origin.X/2) { position.X = -NODE_SIZE.X/2 - GridNode.origin.X/2; }
        if (position.Y > -NODE_SIZE.Y/2 - GridNode.origin.Y) { position.Y = -NODE_SIZE.Y/2 - GridNode.origin.Y; }

        if (position.X < -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X) { position.X = -NODE_SIZE.X * LEVEL_SIZE.X + GAME_WINDOW_SIZE.X;  }
        if (position.Y < -NODE_SIZE.Y/2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y) { position.Y = -NODE_SIZE.Y/2 * LEVEL_SIZE.Y + GAME_WINDOW_SIZE.Y; }

        base.HandleInput(inputHelper);
    }
}