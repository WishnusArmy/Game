using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

class Pulse : ProjectileAtTower
{
    private int radiusCurrent;
    private List<Enemy> TargetsHit;
    private Color color;

    public Pulse(double damage, double range, int rate) : base(damage, range, rate)
    {
        sprite = SPR_PULSE;
        TargetsHit = new List<Enemy>();
        radiusCurrent = 0;
        color = new Color(0, 0, 210);
    }

    public void CheckCollision()
    {
        foreach (Enemy enemy in MyPlane.FindByType<Enemy>())
        {
            double distance = DISTANCE(GlobalPosition, enemy.GlobalPositionCenter);
            if (distance < radiusCurrent + rate*2 && distance > radiusCurrent - rate*2 && !TargetsHit.Contains(enemy))
            {
                enemy.health -= (int)damage;
                TargetsHit.Add(enemy);
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
        radiusCurrent += (int)rate;
        CheckCollision();
        Kill = radiusCurrent > range;
    }
    
}
