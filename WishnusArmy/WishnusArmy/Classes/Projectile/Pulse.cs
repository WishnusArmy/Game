using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

class Pulse : Projectile
{
    static int speed = 15;
    private int radiusCurrent;
    private List<Enemy> targetsHit;
    private Color color;
    double range;
    float p;
    Tower myTower;

    public Pulse(double damage, double range, List<Enemy> enemies) : base(damage)
    {
        sprite = SPR_PULSE;
        radiusCurrent = 0;
        targetsHit = new List<Enemy>();
        color = new Color(0, 0, 210);
        this.range = range;
        p = 0;
        myTower = Parent as Tower;
    }

    public void CheckCollision()
    {
        myTower = parent as Tower;
        foreach (Enemy enemy in  myTower.enemies)
        {
            if (!targetsHit.Contains(enemy) && !(enemy is EnemyAir))
            {
                double distance = DISTANCE(GlobalPosition, enemy.GlobalPositionCenter);
                if (distance < radiusCurrent + speed * 2 && distance > radiusCurrent - speed * 2)
                {
                    enemy.dealDamage(damage * (1 - 0.5 * p), Tower.Type.PulseTower);
                    targetsHit.Add(enemy);
                }
            }
        }
    }

    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(
               sprite,
               null,
               new Rectangle(
                   (int) GlobalPosition.X - radiusCurrent, 
                   (int) GlobalPosition.Y - radiusCurrent/2, 
                   radiusCurrent*2, 
                   radiusCurrent),
               new Rectangle(0, 0, sprite.Width, sprite.Height),
               Vector2.Zero,
               0f,
               new Vector2(1f, 1f),
               color * (1-p * p ),
               SpriteEffects.None,
               0);
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        p = (float)(radiusCurrent / range);
        radiusCurrent += speed;
        CheckCollision();
        Kill = radiusCurrent > range;
    }
    
}
