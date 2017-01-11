using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static ContentImporter.Sprites;
using static Constant;

public class Tank : Enemy
{
    public Tank() : base(Type.Tank)
    {
        this.sprite = SPR_ENEMY;
        this.speed = 3;
    }
}
