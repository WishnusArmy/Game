using System;
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
        : base(Type.Tank, SHEET_INFANTRY)
    {
        this.sprite = SHEET_INFANTRY;
        this.killReward = EnemyRewardMoney(0);
        this.speed = 2f;
        strongness = Tower.Type.PulseTower;
    }
}
