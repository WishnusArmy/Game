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
    public LaserTower() : base()
    {
        type = 1;
        baseTexture = SPR_LASER_TOWER;
        target = findTarget();
        Laser laser = new Laser(TowerDamage(type, stats), TowerRange(type, stats), TowerRate(type, stats));
        laser.Target = target;
        Add(laser);
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
        target = findTarget();
        Attack();
    }
}

