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
    private bool colorUP;
    private Color color;

    public Pulse(double damage, double range, int rate) : base(damage, range, rate)
    {
        TargetsHit = new List<Enemy>();
        radiusCurrent = 0;
        TargetsHit.Clear();
        colorUP = true;
        color = new Color(0, 0, 210);
    }

    public void CheckCollision()
    {
        foreach (Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            double distance = DISTANCE(GlobalPosition, enemy.GlobalPositionCenter);
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
               SPR_PULSE,
               new Rectangle(
                   (int)GlobalPosition.X - radiusCurrent + SPR_PULSE_TOWER.Width/2, 
                   (int)GlobalPosition.Y - radiusCurrent + SPR_PULSE_TOWER.Height/2, 
                   radiusCurrent*2, 
                   radiusCurrent*2),
               new Rectangle(0, 0, SPR_PULSE.Width, SPR_PULSE.Height),
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
        if (radiusCurrent > range)
            Reset();
    }
    
}
