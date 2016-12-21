using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Textures;
using static ContentImporter.Fonts;


public class Hud : GameObject
{
    public Hud()
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawingHelper.DrawRectangleFilled(new Rectangle(SCREEN_SIZE.X - NODE_SIZE * 4, 0, NODE_SIZE * 4, SCREEN_SIZE.Y), spriteBatch, Color.White, 1f);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, SCREEN_SIZE.Y - NODE_SIZE * 2, SCREEN_SIZE.X, NODE_SIZE * 2), spriteBatch, Color.White, 1f);

    }
}

