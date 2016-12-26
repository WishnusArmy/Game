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
    public LaserTower() : base()
    {
        damage = Constant.LASER_DAMAGE[level];
        this.range = LASER_RADIUS[level];
        this.baseTexture = SPR_LASER_TOWER;
        laser = new Laser();
        laser.Position = new Vector2(NODE_SIZE.X / 2, -NODE_SIZE.Y / 3);
        Add(laser);
        target = null;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        target = getTarget();
        laser.target = target;
        laser.damage = damage;

        if (target != null && DISTANCE(target.GlobalPosition, GlobalPosition) > range)
            target = null;
    }
    public override void Attack()
    {
        base.Attack();
    }
}

