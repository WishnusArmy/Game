using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

public class Base : GameObject
{
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(SPR_BASE, (new Vector2((LEVEL_SIZE * NODE_SIZE / 2) - 128, (LEVEL_SIZE * NODE_SIZE / 2) - 128) + GlobalPosition), Color.White);


    }
}
