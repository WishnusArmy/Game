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
    private int speed;

    public Pulse(int damage, int speed, Vector2 position, int radius) : base()
    {
        Position = position;
        this.radiusMax = radius;
        this.damage = damage;
        this.speed = speed;
        Reset();
    }

    public void Reset()
    {
        
    }

    public void CheckCollision()
    {
        foreach (Enemy enemy in GameWorld.FindByType<Enemy>())
        {
            //if (CollidesWith(enemy))
            //{
                
            //}
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
        radiusCurrent += speed;
        CheckCollision();
        if (radiusCurrent > radiusMax)
            Reset();
    }
}
