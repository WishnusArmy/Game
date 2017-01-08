using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

class Laser : Projectile
{
    double timer;

    public Laser(double damage, double range, double rate) : base(damage, range, rate)
    {
        timer = 0;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (HasTarget)
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.GlobalPositionCenter, Color.Red, 10);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer++;
        if (HasTarget && timer > rate)
        {
            target.health -= (int)damage;
            timer = 0;
        }
    }
}
