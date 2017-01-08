using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

class PulseTower : Tower
{
    public PulseTower() : base()
    {
        damage = Constant.PULSE_DAMAGE[level];
        this.baseTexture = SPR_PULSE_TOWER;
    }

    public override void Attack()
    {
        base.Attack();
        Add(new Pulse(level, Vector2.Zero, range));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}


