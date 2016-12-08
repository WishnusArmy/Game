﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static Constant;
using WishnusArmy.Classes.Towers;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public enum Plane { Underground, Land, Air };
    public Plane currentPlane;

    public Camera() : base()
    {
        currentPlane = Plane.Land;
        for(int i=0; i<3; ++i)
        {
            Add(new GridPlane((Plane)i)); //Add the three layers of the game
            if(i==1)
            {
            }
        }
        Add(new Base());
        
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
        if (inputHelper.MouseLeftButtonPressed())
        {
            Console.WriteLine((float)Math.Floor((inputHelper.MousePosition.X / NODE_SIZE)));
            Tower t = new Tower();
            t.gridPosition = new Vector2((float)Math.Floor(((inputHelper.MousePosition.X - position.X) / NODE_SIZE)), (float)Math.Floor((inputHelper.MousePosition.Y - position.Y) / NODE_SIZE));
            Add(t);
        }

        //
        if (position.X > 0) { position.X = 0; }
        if (position.Y > 0) { position.Y = 0; }
        if (position.X < -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X ) { position.X = -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.X;  }
        if (position.Y < -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y) { position.Y = -NODE_SIZE * LEVEL_SIZE + WishnusArmy.WishnusArmy.Screen.Y; }


        base.HandleInput(inputHelper);
    }
}