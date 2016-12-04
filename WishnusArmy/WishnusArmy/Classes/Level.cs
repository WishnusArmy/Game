using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Level : GameObject
{
    GridLayer[] world; //Container for the three layers

    public Level(ContentManager content)
    {
        world = new GridLayer[3];  //Iniatilize the world
        for(int i=0; i<3; ++i)
        {
            world[i] = new GridLayer(content); //fill the world with empty grids
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        for(int i=0; i<world.Length; ++i)
        {
            world[i].Draw(gameTime, spriteBatch);
        }
    }
}

