using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;

public class Bullet : Projectile
{
    public Bullet(double damage, double range, int speed) : base(damage)
    {

    }    
}
