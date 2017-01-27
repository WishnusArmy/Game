using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

class BombTower : RocketTower
{
    public BombTower() 
    {
        baseTexture = ContentImporter.Sprites.SPR_ROCKET_TOWER;
        this.type = Tower.Type.BombTower;
    }
    public override void Attack()
    {
            MyPlane.Add(new Bomb(TowerDamage(Tower.Type.BombTower, stats), 7) { Position = position });
            return;
    }
}
