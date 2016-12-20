using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

class Bullet : Projectile
    
{
    private float rotation;
    private bool foundTarget;
    public Enemy enemy;

    public Bullet(int damage, int speed, Vector2 startPosition) : base()
    {
        foundTarget = false;
        this.damage = damage;
        this.speed = speed;
        Position = startPosition + GlobalPosition;
    }

    private void findTarget()
    {
        if (foundTarget)
            return;
        List<Enemy> enemies = GameWorld.FindByType<Enemy>();
        if (enemies.Count > 0)
        {
            enemy = enemies[RANDOM.Next(0,enemies.Count)];
            foundTarget = true;
        }
    }
    
    private void calculateCourse()
    {
        if (foundTarget)
        {
            double opposite = enemy.Position.Y - SPR_BULLET.Width / 2 - GlobalPosition.Y;
            double adjacent = enemy.Position.X - SPR_BULLET.Width / 2 - GlobalPosition.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
        }
        else
        {
            rotation += (float) 0.05;
        }
        int x = (int)(Math.Cos(rotation - 0.5 * Math.PI) * speed);
        int y = (int)(Math.Sin(rotation - 0.5 * Math.PI) * speed);
        velocity = new Vector2(x, y);
    }
    
    
    public void CheckCollision()
    {
        if (CalculateDistance(enemy.GlobalPosition, GlobalPosition) < 50)
        {
            enemy.health -= damage;
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
        base.Update(gameTime);
        findTarget();
        if (enemy == null)
        {
            foundTarget = false;
        }
        else
        {
            foundTarget = !enemy.Kill;
            CheckCollision();
        }
        calculateCourse();
        Position += this.velocity;
    }

    
}
