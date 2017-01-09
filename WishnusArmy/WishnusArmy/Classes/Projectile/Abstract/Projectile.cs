using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public abstract class Projectile : GameObject
{
    public double range, damage, rate;
    public Enemy target;

    public Projectile() : base()
    {
        target = null;
        range = 1000;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (HasTarget && CalculateDistance(target.GlobalPosition, GlobalPosition) > range)
        {
            target = null;
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public bool HasTarget
    {
        get
        {
            return (target != null && !target.Kill);
        }
    }

    public Enemy Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }
    public double Damage
    {
        set
        {
            damage = value;
        }
    }
    public double Range
    {
        set
        {
            range = value;
        }
    }
    public double Rate
    {
        set
        {
            rate = value;
        }
    }
}
