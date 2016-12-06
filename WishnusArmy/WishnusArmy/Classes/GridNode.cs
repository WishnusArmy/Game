using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using static Constant;
using static ContentImporter.Textures;

public class GridNode : GameObject
{
    int obj; //Indicator for what is placed on the square (0 for emtpy)
    int texture; //Indicator for what texture is placed on the square
    Texture2D tex;
    bool selected;

    public GridNode(ContentManager Content, Vector2 position, int texture = 0, int obj = 0) : base()
    {
        this.texture = texture;
        this.obj = obj;
        this.position = position;
        if (WishnusArmy.WishnusArmy.Random.Next(2) == 0)
            tex = TEX_GRASS;
        else
            tex = TEX_GRASS_DIRT;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        selected = false;
        if (inputHelper.MouseInRectangle(new Rectangle(new Point((int)GlobalPosition.X, (int)GlobalPosition.Y), new Point(NODE_SIZE))))
        {
            selected = true;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(tex, GlobalPosition, Color.White);
        if (selected)
        {
            DrawingHelper.DrawRectangleFilled(new Rectangle(new Point((int)GlobalPosition.X, (int)GlobalPosition.Y), new Point(NODE_SIZE)), spriteBatch, Color.Black, 0.2f);
        }
    }
}

