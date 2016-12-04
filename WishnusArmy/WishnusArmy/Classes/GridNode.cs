using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class GridNode : GameObjectList
{
    int obj; //Indicator for what is placed on the square (0 for emtpy)
    int texture; //Indicator for what texture is placed on the square
    Texture2D tex;

    public GridNode(ContentManager Content, Vector2 position, int texture = 0, int obj = 0) : base()
    {
        this.texture = texture;
        this.obj = obj;
        this.position = position;
        tex = Content.Load<Texture2D>("Content/Textures/tex_grass");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(tex, GlobalPosition, Color.White);
    }
}

