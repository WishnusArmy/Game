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
    public Enemy target;
    private int timer;
    private int radius;

    // Call Laser like 
    //      Laser(towerPosition)
    // Target new enemy as 
    //      laser.Target = TargetPosition

    public Laser(Vector2 startPosition) : base()
    {
        Position = startPosition;
        timer = 0;
    }
    
    

    public void Target()
    {
        if (target != null)
        {
            target.DealDamage = damage;
        }
        
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (target == null || !visible)
            return;
        base.Draw(gameTime, spriteBatch);
        if (target.Visible)
        DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.GlobalPosition, new Color(255 - timer*2,0, timer*5), 16);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer++;
            Reset();
        position += position - GlobalPosition;
    }

    public override void Reset()
    {
        timer = 0;
        visible = true;
        Target();
    }

}
