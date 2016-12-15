using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

class Pulse : Projectile
{
    private int radiusMax;
    private int radiusCurrent;

    public Pulse(int damage, Vector2 velocity, Vector2 position, int radius) : base()
    {
        Position = position;
        this.radiusMax = radius;
        this.damage = damage;
        this.velocity = velocity;
        Reset();
    }

    public void Reset()
    {
        radiusCurrent = 0;
    }

    public void CheckCollision()
    {
        return;
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
               Color.Blue);
    }

    public override void Update(GameTime gameTime) {
        if (!visible)
            return;
        base.Update(gameTime);
        radiusCurrent += 2;
        if (radiusCurrent > radiusMax)
            Reset();
        CheckCollision();
    }
}
