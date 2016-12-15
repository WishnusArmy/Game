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


    // Call Laser like 
    //      Laser(towerPosition)
    // Target new enemy as 
    //      laser.Target = TargetPosition

    public Laser(Vector2 startPosition) : base()
    {
        Position = startPosition;
        timer = 0;
        damage = 4;
    }
    
    

    public void Target()
    {
        Enemy targetEnemy = null;
        foreach(Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            if (enemy.Visible)
            {
                targetEnemy = enemy;
            }
                
        }
        if (targetEnemy != null)
        {
            this.target = targetEnemy;
            targetEnemy.DealDamage = damage;
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
