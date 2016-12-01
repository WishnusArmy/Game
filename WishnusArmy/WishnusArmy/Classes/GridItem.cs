using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GridItem
{
    int obj; //Indicator for what is placed on the square (0 for emtpy)
    int texture; //Indicator for what texture is placed on the square
    Texture2D tex;

    public GridItem(ContentManager Content, int texture = 0, int obj = 0)
    {
        this.texture = texture;
        this.obj = obj;
        tex = Content.Load<Texture2D>("Content/Textures/tex_grass");
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 pos)
    {
        spriteBatch.Draw(tex, pos, Color.White);
    }
}

