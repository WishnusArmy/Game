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
        TargetsHit.Clear();
        color = new Color(0, 0, 210);
    }

    public void CheckCollision()
    {
        foreach (Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            double distance = DISTANCE(GlobalPositionCenter, enemy.GlobalPositionCenter);
            int offset = (int)rate/2;
            if (distance < radiusCurrent + offset && distance > radiusCurrent - offset)
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
