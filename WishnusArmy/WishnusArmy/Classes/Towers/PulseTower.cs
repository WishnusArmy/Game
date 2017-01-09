﻿using System;
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

    public override void Attack()
    {
        base.Attack();
        Add(new Pulse(TowerDamage(type, stats), TowerRange(type, stats), TowerRate(type, stats)));
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}

