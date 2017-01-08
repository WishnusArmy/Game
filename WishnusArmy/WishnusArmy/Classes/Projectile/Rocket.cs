using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

public class Rocket : Projectile
{
    float rotation;
    bool foundTarget;
    public Enemy enemy;
    float speed;

    public Rocket(int damage, int speed,  Vector2 startPosition) : base(damage, 0, 0)
    {
        Console.WriteLine("Rocket Existing boii");
        foundTarget = false;
        this.damage = damage;
        this.speed = speed;
        Position = Vector2.Zero;
    }

    private void findTarget()
    {
        if (foundTarget)
            return;
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        if (enemies.Count > 0)
        {
            enemy = enemies[RANDOM.Next(enemies.Count)];
            foundTarget = true;
        }
    }

    private void calculateCourse()
    {
        if (foundTarget)
        {
            double opposite = (enemy.GlobalPosition.Y + enemy.sprite.Height / 2) - (GlobalPosition.Y + SPR_BULLET.Height / 2);
            double adjacent = (enemy.GlobalPosition.X + enemy.sprite.Width / 2) - (GlobalPosition.X + SPR_BULLET.Width / 2);
            rotation = (float)Math.Atan2(opposite, adjacent);// + 0.5f * (float)Math.PI;
        }
        else
        {
            rotation += (float)0.05;
        }
        float x = (float)Math.Cos(rotation) * speed;
        float y = (float)Math.Sin(rotation) * speed;
        velocity = new Vector2(x, y);
    }


    public void CheckCollision()
    {
        if (CalculateDistance(enemy.GlobalPosition + new Vector2(enemy.sprite.Width, enemy.sprite.Height) / 2, GlobalPosition + new Vector2(SPR_BULLET.Width, SPR_BULLET.Height) / 2) < 50)
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
        Vector2 origin = new Vector2(SPR_BULLET.Width / 2, SPR_BULLET.Height / 2);
        spriteBatch.Draw(
            SPR_BULLET,
            new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, SPR_BULLET.Width, SPR_BULLET.Height),
            null,
            Color.White,
            rotation + 0.5f * (float)Math.PI,
            origin,
            SpriteEffects.None,
            0f);
    }

    public override void Update(GameTime gameTime)
    {
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
    }


}
