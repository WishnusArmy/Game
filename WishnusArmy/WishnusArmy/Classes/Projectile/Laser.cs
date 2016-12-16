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
    private Enemy target;
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
        damage = LASER_DAMAGE[0];
        radius = LASER_RADIUS[1];
    }
    
    

    public void Target()
    {
        target = null;
        foreach(Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            if (enemy.Visible && DISTANCE(enemy.Position, Position) < radius)
            {
                this.target = enemy;
            }
                
        }
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
        if (target.Visible || timer < LASER_TIME/4)
        DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.Position, new Color(255 - timer*2,0, timer*5), 16);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer++;
        if (timer > LASER_TIME)
        {
            Reset();
        }
    }

    public override void Reset()
    {
        timer = 0;
        visible = true;
        Target();
    }

}
