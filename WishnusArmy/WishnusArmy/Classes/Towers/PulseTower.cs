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
        this.baseTexture = SPR_PULSE_TOWER;
    }

    public override void Attack()
    {
        base.Attack();
        Add(new Pulse(stats[0], stats[1], stats[2]));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}

