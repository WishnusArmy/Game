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
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (laser != null)
            laser.Draw(gameTime, spriteBatch);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (laser != null)
            laser.HandleInput(inputHelper);
    }
    public override void Update(GameTime gameTime)
    {
        if (target != null)
        {
            Attack();
        }
        if (laser != null)
        {
            laser.Update(gameTime);
            laser.Position = gridPosition * NODE_SIZE.X + new Vector2(baseTexture.Width/2, baseTexture.Height/2)+ GlobalPosition;
        }
        //if target is out of range
        if (target != null && DISTANCE(target.Position + GlobalPosition, pos) > range)
            target = null;
    }
    public override void Attack()
    {
        base.Attack();
        laser = new Laser(pos);
        laser.target = target;
        laser.damage = damage;

    }
}

