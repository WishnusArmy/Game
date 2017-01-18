using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

class PulseTower : Tower
{
    public PulseTower() : base(Type.PulseTower)
    {
        this.baseTexture = SPR_PULSE_TOWER;
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        if(canShoot && target != null)
        {
            Add(new Pulse(TowerDamage(type, stats), TowerRange(type, stats), enemies));
        }
    }
}

