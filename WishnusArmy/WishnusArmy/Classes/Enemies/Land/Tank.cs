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
        this.speed = 2.5f;
        weakness = Tower.Type.RocketTower;
        strongness = Tower.Type.LaserTower;
        cost = 100;
    }
}
