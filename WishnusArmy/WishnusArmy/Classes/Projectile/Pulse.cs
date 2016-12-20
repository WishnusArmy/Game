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
    private int radiusMax;
    private int radiusCurrent;

    public Pulse(int level, Vector2 position, int radius) : base()
    {
        Position = position;
        this.radiusMax = PULSE_RADIUS[level];
        this.damage = PULSE_DAMAGE[level];
        this.speed = PULSE_SPEED[level];
        Reset();
    }

    public override void Reset()
    {
        radiusCurrent = 0;
    }

    public void CheckCollision()
    {
        foreach (Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            double distance = DISTANCE(Position, enemy.Position);
            int offset = (int)speed/2;
            if (distance < radiusCurrent + offset && distance > radiusCurrent - offset)
            {
                enemy.health -= damage;
            }
        }
    }
    

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);

        spriteBatch.Draw(
               SPR_PULSE,
               new Rectangle(
                   (int)GlobalPosition.X - radiusCurrent, 
                   (int)GlobalPosition.Y - radiusCurrent, 
                   radiusCurrent*2, 
                   radiusCurrent*2),
               new Rectangle(0, 0, SPR_PULSE.Width, SPR_PULSE.Height),
               Color.Yellow);
    }

    public override void Update(GameTime gameTime) {
        if (!visible)
            return;
        base.Update(gameTime);
        radiusCurrent += (int)speed;
        CheckCollision();
        if (radiusCurrent > radiusMax)
            Reset();
    }
}
