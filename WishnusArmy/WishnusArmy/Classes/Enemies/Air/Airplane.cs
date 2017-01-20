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
        this.speed = 4.5f;
        cost = 450;
        weakness = Tower.Type.RocketTower;
        strongness = Tower.Type.LaserTower;
    }
}
