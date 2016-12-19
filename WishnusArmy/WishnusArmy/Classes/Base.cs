using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

public class Base : Tower
{
    Vector2 GameMiddle = new Vector2(LEVEL_SIZE * NODE_SIZE.X / 2, LEVEL_SIZE * NODE_SIZE.Y / 4);
    Vector2 BaseOrigin = new Vector2(SPR_BASEGUN.Width / 2, SPR_BASEGUN.Height / 2);

    public Base() : base()
    {
        //this.gridPosition = new Vector2(LEVEL_SIZE / 2, LEVEL_SIZE / 4) - BaseOrigin/NODE_SIZE.X;
        //this.gridPosition = new Vector2(5,5);
        this.cannonTexture = SPR_BASEGUN;
        this.baseTexture = SPR_BASE;
        this.range = 0; //(aimed manually)
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
    public override Vector2 findTarget()
    {
        return mousePosition;
    }
}
