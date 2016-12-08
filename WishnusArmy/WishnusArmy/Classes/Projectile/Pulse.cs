using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;

class Pulse : Projectile
{
    protected int radiusMax;
    private int radiusCurrent;

    public Pulse(int damage, Vector2 velocity, int radius) : base()
    {
        Position = velocity;
        this.radiusMax = radius;
        radiusCurrent = 1;
        this.damage = damage;
        this.velocity = velocity;
        layer = 0;
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);

        spriteBatch.Draw(SPR_PARTICLE,
               new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, radiusCurrent, radiusCurrent),
               new Rectangle(0, 0, SPR_PARTICLE.Width, SPR_PARTICLE.Height),
               Color.White);
    
    }

    public override void Update(GameTime gameTime)
    {
        if (radiusCurrent > radiusMax)
        {
            radiusCurrent = 0; ;
            return;
        }
        radiusCurrent++;
    }
}
