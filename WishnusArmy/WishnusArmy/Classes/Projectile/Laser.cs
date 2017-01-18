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
            pos = target.GlobalPositionCenter;
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, pos, Color.Red * (p * p * p), 10);
        }
        base.Draw(gameTime, spriteBatch);
    }

    

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        if (target != null && (visibleTimer == visibleTimerMax))
        {
            target.health -= damage;
            PlaySound(ContentImporter.Sounds.SND_LASER);
        }

        if (visibleTimer > 0)
            visibleTimer--;
        else
            kill = true;
    }
}
