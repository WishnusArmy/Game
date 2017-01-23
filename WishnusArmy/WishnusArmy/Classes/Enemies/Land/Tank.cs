using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sheets;
using static Constant;
using Microsoft.Xna.Framework.Graphics;

public class Tank : EnemyLand
{
    public Tank()
        : base(Type.Tank, SHEET_TANK)
    {
        this.sprite = SHEET_TANK;
        this.speed = (2.0f + (0.1f * GameStats.Wave));
        weakness = Tower.Type.RocketTower;
        strongness = Tower.Type.LaserTower;
        killReward = 30 + GameStats.Wave;
        cost = 100;
    }
}
