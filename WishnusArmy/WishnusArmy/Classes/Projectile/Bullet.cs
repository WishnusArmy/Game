using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;

class Bullet : Projectile
    
{
    private float rotation;
    int speed;
    public Enemy enemy;

    public Bullet(int damage, int speed, Vector2 startPosition) : base()
    {
        this.damage = damage;
        this.speed = speed;
        Position = startPosition + GlobalPosition;
        calculateRotation();
        calculateVelocity();
        
    }

    private void calculateVelocity()
    {
        int x = (int) (Math.Cos(rotation-0.5*Math.PI) * 5 * speed);
        int y = (int)(Math.Sin(rotation-0.5 * Math.PI) * 5 * speed);
        velocity = new Vector2(x,y);
        
    }

    private void calculateRotation()
    {
        if (enemy != null)
        {
            double opposite = Target().Y - SPR_BULLET.Width / 2 - GlobalPosition.Y;
            double adjacent = Target().X - SPR_BULLET.Width / 2 - GlobalPosition.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
        }
    }

    public Vector2 Target()
    {
        return enemy.Position;
    }

    public void CheckCollision()
    {
        if (CalculateDistance(enemy.Position, position) < 30)
        {
            enemy.DealDamage = damage;
            visible = false;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        calculateRotation();
        calculateVelocity();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);
        // find target and get his position
        //GameObject target = GameWorld.FindById(targetId) as GameObject;
        //Vector2 targetPosition = target.GlobalPosition;
        // calculate the rotation of the sprite

        // draw the sprite
        Vector2 origin = new Vector2(SPR_BULLET.Width/2, SPR_BULLET.Height/2);
        spriteBatch.Draw(SPR_BULLET, 
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
        base.Update(gameTime);
        position += velocity;
        CheckCollision();
    }
}
