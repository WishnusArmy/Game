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

public class Base : CannonTower
{
    Vector2 GameMiddle = new Vector2(LEVEL_SIZE.X * NODE_SIZE.X / 2, LEVEL_SIZE.Y * NODE_SIZE.Y / 4);
    Vector2 BaseOrigin = new Vector2(SPR_BASEGUN.Width / 2, SPR_BASEGUN.Height / 2);

    public Base() : base(Type.Base)
    {
        //this.gridPosition = new Vector2(LEVEL_SIZE / 2, LEVEL_SIZE / 4) - BaseOrigin/NODE_SIZE.X;
        //this.gridPosition = new Vector2(5,5);
        this.cannonTexture = SPR_BASEGUN;
        this.baseTexture = SPR_BASE;
        //this.range = 0; //(aimed manually)
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        targetPos = inputHelper.MousePosition;
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
    }

    public override void Attack()
    {
        //throw new NotImplementedException();
    }
}
