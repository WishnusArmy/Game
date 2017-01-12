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
    static int speed = 20;
    private int radiusCurrent;
    private List<Enemy> TargetsHit;
    private Color color;
    double range;

    public Pulse(double damage, double range) : base(damage)
    {
        sprite = SPR_PULSE;
        TargetsHit = new List<Enemy>();
        radiusCurrent = 0;
        color = new Color(0, 0, 210);
        this.range = range;
    }

    public void CheckCollision()
    {
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            double distance = DISTANCE(GlobalPosition, enemy.GlobalPositionCenter);
            if (distance < radiusCurrent + speed*2 && distance > radiusCurrent - speed*2 && !TargetsHit.Contains(enemy))
            {
                TargetsHit.Add(enemy);
                enemy.health -= (int)damage;
            }
        }
    }

    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(
               sprite,
               new Rectangle(
                   (int) GlobalPosition.X - radiusCurrent, 
                   (int) GlobalPosition.Y - radiusCurrent, 
                   radiusCurrent*2, 
                   radiusCurrent*2),
               new Rectangle(0, 0, sprite.Width, sprite.Height),
               color);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!visible)
            return;
        base.Update(gameTime);
        radiusCurrent += speed;
        CheckCollision();
        Kill = radiusCurrent > range;
    }
    
}
