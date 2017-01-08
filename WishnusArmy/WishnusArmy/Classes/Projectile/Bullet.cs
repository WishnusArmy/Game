using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;

public class Bullet : Projectile
{
    private float rotation;

    public Bullet(double damage, double range, double rate) : base(damage, range, rate)
    {
        rotation = 0;
    }

    private void calculateCourse()
    {
        if (HasTarget)
        {
            double opposite = target.Position.Y - SPR_BULLET.Width / 2 - base.GlobalPosition.Y;
            double adjacent = target.Position.X - SPR_BULLET.Width / 2 - GlobalPosition.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
        }
        else
        {
            rotation += (float) 0.05;
        }
        int x = (int)(Math.Cos(rotation - 0.5 * Math.PI) * rate);
        int y = (int)(Math.Sin(rotation - 0.5 * Math.PI) * rate);
        velocity = new Vector2(x, y);
    }
    
    
    public void CheckCollision()
    {
        if (CalculateDistance(target.GlobalPosition, GlobalPosition) < 50)
        {
            target.health -= (int)damage;
            Kill = true;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);
        Vector2 origin = new Vector2(SPR_BULLET.Width/2, SPR_BULLET.Height/2);
        spriteBatch.Draw(
            SPR_BULLET, 
            new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, SPR_BULLET.Width, SPR_BULLET.Height),
            null,
            Color.White,
            rotation,
            origin,
            SpriteEffects.None,
            0f);
    }

    public override void Update(GameTime gameTime)
    {
        if (!visible)
            return;
        if (HasTarget)
            CheckCollision();
        calculateCourse();
        Position += this.velocity;
    }
    
}
