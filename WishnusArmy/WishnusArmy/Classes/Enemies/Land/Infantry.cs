﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sheets;
using static Constant;
using Microsoft.Xna.Framework.Graphics;

public class Infantry : EnemyLand
{
    public Infantry()
        : base(Type.Soldier, SHEET_INFANTRY)
    {
        this.sprite = SHEET_INFANTRY;
        this.killReward = EnemyRewardMoney(type);
        this.speed = 2.5f;
        strongness = Tower.Type.PulseTower;
        cost = 20;
    }
}
