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
                    p.Add(new Base());
                    //(testcode) plaatst torens en voegt een enemy toe
                    int y = 0;
                        for (int t = 0; t < 20; t++)
                        {
                            {

                                p.Add(new Enemy());
                                y++;
                            /*
                            p.Add(new Laser(new Vector2(200, 200)));
                            p.Add(new Laser(new Vector2(600, 600)));
                            p.Add(new Laser(new Vector2(900, 900)));
                            */

                        }
                        }
                    LaserTower x = new LaserTower();
                    x.camera = this;
                    x.gridPosition = new Vector2(6, 6);
                    p.Add(x);
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
        Add(currentPlane); //Add the currenPlane to the children list. This way, only one plane at a time will be handled
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //Manually Update all the planes (except currentPlane)
        //This way only one will be drawn, but all will update.
        for (int i = 0; i < 3; ++i)
        {
            if (planes[i].planeType != currentPlane.planeType)
            {
                planes[i].Update(gameTime);
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //Camera Movement
        Vector2 mp = inputHelper.MousePosition;
        if (mp.X < SLIDE_BORDER)
            position.X += SLIDE_SPEED;
        if (mp.X > WishnusArmy.WishnusArmy.Screen.X - SLIDE_BORDER)
            position.X -= SLIDE_SPEED;
        if (mp.Y < SLIDE_BORDER)
            position.Y += SLIDE_SPEED;
        if (mp.Y > WishnusArmy.WishnusArmy.Screen.Y - SLIDE_BORDER)
            position.Y -= SLIDE_SPEED;

        //Make sure the camera doesn't move out of bounds
        if (position.X > -NODE_SIZE.X/2) { position.X = -NODE_SIZE.X/2; }
        if (position.Y > -NODE_SIZE.Y/2) { position.Y = -NODE_SIZE.Y/2; }

        if (position.X < -NODE_SIZE.X * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X ) { position.X = -NODE_SIZE.X * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X;  }
        if (position.Y < -NODE_SIZE.Y/2 * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y) { position.Y = -NODE_SIZE.Y/2 * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y; }

        base.HandleInput(inputHelper);
    }
}