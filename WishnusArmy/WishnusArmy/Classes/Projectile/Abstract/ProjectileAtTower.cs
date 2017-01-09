using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public abstract class ProjectileAtTower : Projectile
{
    public double range;
    public int rate;
    public int timer;
    public bool canShoot
    {
        get
        {
            return timer <= 0;
        }
    }

    public ProjectileAtTower(double damage, double range, int rate) : base(damage)
    {
        this.rate = rate;
        this.range = range;
        this.timer = rate;
    }

    public override void Update(GameTime gameTime)
    {
        if (timer == 0)
            timer = rate;
        else if (timer > 0)
            timer--;

        base.Update(gameTime);
        if (HasTarget && CalculateDistance(target.GlobalPosition, GlobalPosition) > range)
        {
            target = null;
        }
    }
}
