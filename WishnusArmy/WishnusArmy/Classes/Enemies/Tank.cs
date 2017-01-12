using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sheets;
using Microsoft.Xna.Framework.Graphics;

public class Tank : Enemy
{
    public Tank() 
        : base(SHEET_TANK)
    {
        this.sprite = SHEET_TANK;
        this.speed = 5;
    }
}
