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
        this.speed = 2;
    }
}
