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
    public LaserTower() : base(Type.LaserTower)
    {
        this.baseTexture = SPR_LASER_TOWER;
        Add(laser = new Laser(0,0,0));
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
        if (target == null)
            target = findTarget();
        laser.target = target;
        laser.damage = TowerDamage(type, stats);
        laser.range = TowerRange(type, stats);
        laser.rate = TowerRate(type, stats);


        // laser.Position = gridPosition * NODE_SIZE.X + new Vector2(baseTexture.Width/2, baseTexture.Height/2)+ GlobalPosition;
        //if target is out of range
        if (target != null && DISTANCE(target.GlobalPosition, GlobalPosition) > TowerRange(type, stats))
            target = null;
    }
}

