using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sheets;
using static Constant;

public class Airplane : EnemyAir
{
    public Airplane() : base(Type.Airplane, SHEET_AIRPLANE)
    {
        this.sprite = SHEET_AIRPLANE;
        this.speed = (4.0f + (0.1f * GameStats.Wave));
        cost = 450;
        killReward = 55 + GameStats.Wave;
        weakness = Tower.Type.RocketTower;
        strongness = Tower.Type.LaserTower;
    }
}
