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
    static int visibleTimerMax = 30;
    int visibleTimer;
    Vector2 pos;

    public Laser(double damage, double range, int rate) : base(damage, range, rate)
    {
        visibleTimer = visibleTimerMax;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        float p = (float)visibleTimer / visibleTimerMax;
        if (HasTarget && timer > 0)
        {
            if (pos == Vector2.Zero)
            {
                pos = target.GlobalPositionCenter;
            }
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, pos, Color.Red * (p * p * p), 10);
        }
        base.Draw(gameTime, spriteBatch);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (HasTarget && (visibleTimer == visibleTimerMax))
        {
            target.health -= damage;
        }
        if (visibleTimer > 0)
            visibleTimer--;
        else
            kill = true;
    }
}
