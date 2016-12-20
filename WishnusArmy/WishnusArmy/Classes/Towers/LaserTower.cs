using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static ContentImporter.Sprites;

class LaserTower : Tower
{
    Laser laser;
    public LaserTower()
    {
        damage = Constant.LASER_DAMAGE[level];
        this.range = LASER_RADIUS[level];
        this.baseTexture = SPR_LASER_TOWER;
        laser = new Laser(pos);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (target != null)
            laser.Draw(gameTime, spriteBatch);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (target != null)
            laser.HandleInput(inputHelper);
    }
    public override void Update(GameTime gameTime)
    {

        if (target == null)
            return;
        //if target is out of range
        //if (DISTANCE(pos, target.Position + target.Center) > range)
        //target = null;

        Attack();
        laser.Update(gameTime);
        laser.Position = gridPosition * NODE_SIZE.X + new Vector2(baseTexture.Width/2, baseTexture.Height/2)+ GlobalPosition;
    }
    public override void Attack()
    {
        base.Attack();
        laser.target = target;
        laser.damage = damage;
    }
}

