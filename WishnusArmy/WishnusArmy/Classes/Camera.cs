﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static Constant;
using WishnusArmy.Classes.Towers;
using Microsoft.Xna.Framework.Graphics;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public enum Plane { Underground, Land, Air };
    public Plane currentPlane;
    public GridPlane Underground, Land, Air;
    List<GridPlane> planes;

    public Camera() : base()
    {
        currentPlane = Plane.Land;
        for(int i=0; i<3; ++i)
        { 
            GridPlane p = new GridPlane((Plane)i);
            planes = new List<GridPlane>();
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
                    break;

                case Plane.Air:
                    Air = p;
                    planes.Add(Air);
                    //Add items to the air plane (p.Add)
                    break;
            }
            Add(new GridPlane((Plane)i)); //Add the three layers of the game
        } 
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
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
        if (position.X > 0) { position.X = 0; }
        if (position.Y > 0) { position.Y = 0; }
        if (position.X < -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X ) { position.X = -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X;  }
        if (position.Y < -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y) { position.Y = -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y; }


        base.HandleInput(inputHelper);
    }
}