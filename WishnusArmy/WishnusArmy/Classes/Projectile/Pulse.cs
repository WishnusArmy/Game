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
    Texture2D sprite;

    public Pulse(int damage, Vector2 velocity, int radius) : base()
    {
        Position = new Vector2(1600, 200);
        this.radiusMax = radius;
        radiusCurrent = 1;
        this.damage = damage;
        this.velocity = velocity;
        this.sprite = PARTICLE;
        layer = 0;
    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);

        int pixels = radiusCurrent;

        Vector2[] vertex = new Vector2[pixels];

        double increment = Math.PI * 2.0 / pixels;
        double theta = 0.0;

        for (int i = 0; i < pixels; i++)
        {
            vertex[i] = radiusCurrent * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            theta += increment;
        }
        for (int i = 0; i < pixels; i++)
        {
            spriteBatch.Draw(sprite,
                GlobalPosition + vertex[i], 
                Color.White);
        }
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
