using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

class Laser : ProjectileAtTower
{
    int visibleTimer;
    int timer;

    public Laser(double damage, double range, int rate) : base(damage, range, rate)
    {
        visibleTimer = 12;
        timer = 0;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (HasTarget && timer > 0)
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.GlobalPositionCenter, Color.Red * ((float)timer/visibleTimer), 10);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (timer > 0)
            timer--;
        if (canShoot)
        {
            timer = visibleTimer;
        }
        if (HasTarget && canShoot)
        {
            target.health -= damage;
        }
    }
}
