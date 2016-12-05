using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static Constant;

public class Camera : GameObjectList
{
    //Every object in this class will move with the camera. 
    //HUD items should therefore be put in the playingState children list.
    public Camera(ContentManager content) : base()
    {
        for(int i=0; i<3; ++i)
        {
            Add(new GridLevel(content)); //Add the three layers of the game
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        Console.WriteLine(inputHelper.MousePosition.X + " ][ " + inputHelper.MousePosition.Y);

        Vector2 mp = inputHelper.MousePosition;
        if (mp.X < SLIDE_BORDER)
            position.X += SLIDE_SPEED;
        if (mp.X > WishnusArmy.WishnusArmy.Screen.X - SLIDE_BORDER)
            position.X -= SLIDE_SPEED;
        if (mp.Y < SLIDE_BORDER)
            position.Y += SLIDE_SPEED;
        if (mp.Y > WishnusArmy.WishnusArmy.Screen.Y - SLIDE_BORDER)
            position.Y -= SLIDE_SPEED;

        if (position.X > 0) { position.X = 0; }
        if (position.Y > 0) { position.Y = 0; }


        base.HandleInput(inputHelper);
    }
}

