using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
        position.X++;
    }
}

