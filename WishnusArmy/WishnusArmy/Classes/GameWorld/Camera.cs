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
    public enum Plane { Underground, Land, Air };
    public GridPlane currentPlane;
    public GridPlane Underground, Land, Air;
    List<GridPlane> planes;

    public Camera() : base()
    {
        planes = new List<GridPlane>();
        for (int i=0; i<3; ++i)
        { 
            GridPlane p = new GridPlane((Plane)i);
            p.active = false;
            Add(p);
            switch((Plane)i)
            {
                case Plane.Underground:
                    Underground = p;
                    planes.Add(Underground);
                    //Add items to the underground plane (p.Add)
                    break;

                case Plane.Land:
                    Land = p;
                    planes.Add(Land);
                    //Add items to the land plane (p.Add)
                    Base b = new Base();
                    b.Position = LEVEL_CENTER;
                    p.Add(b);
                    //(testcode) plaatst torens en voegt een enemy toe
                    for (int q = 0; q < 1; ++q)
                    {
                        Enemy e = new Enemy();
                        e.Position = new Vector2(RANDOM.Next(500)+100, RANDOM.Next(600)+100);
                        p.Add(e);
                    }
                    //einde testcode
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
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //Manually handle the updates because the planes are inactive.
        for(int i=0; i<3; ++i)
        {
            planes[i].Update(gameTime);
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
        if (mp.X > GAME_WINDOW_SIZE.X - SLIDE_BORDER)
            position.X -= SLIDE_SPEED;
        if (mp.Y < SLIDE_BORDER)
            position.Y += SLIDE_SPEED;
        if (mp.Y > GAME_WINDOW_SIZE.Y - SLIDE_BORDER)
            position.Y -= SLIDE_SPEED;

        //Make sure the camera doesn't move out of bounds
        if (position.X > -NODE_SIZE.X/2) { position.X = -NODE_SIZE.X/2; }
        if (position.Y > -NODE_SIZE.Y/2) { position.Y = -NODE_SIZE.Y/2; }

        if (position.X < -NODE_SIZE.X * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X ) { position.X = -NODE_SIZE.X * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X;  }
        if (position.Y < -NODE_SIZE.Y/2 * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y) { position.Y = -NODE_SIZE.Y/2 * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y; }

        base.HandleInput(inputHelper);
    }
}