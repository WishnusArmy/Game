using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sheets;
using static Constant;

public class Helicopter : EnemyAir
{
    public Helicopter() : base(Type.Helicopter, SHEET_HELICOPTER)
    {
        this.sprite = SHEET_HELICOPTER;
        this.speed = (2.5f + (0.1f * GameStats.Wave));
        cost = 80;
        killReward = 40 + GameStats.Wave;
        weakness = Tower.Type.LaserTower;
    }
}
