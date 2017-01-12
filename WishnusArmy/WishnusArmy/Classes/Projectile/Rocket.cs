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

    public Rocket(double damage, float speed,  Vector2 startPosition) : base(damage)
    {
        sprite = SPR_ROCKET;
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
            double opposite = (enemy.GlobalPositionCenter.Y) - (GlobalPosition.Y + sprite.Height / 2);
            double adjacent = (enemy.GlobalPositionCenter.X) - (GlobalPosition.X + sprite.Width / 2);
            rotation = (float)Math.Atan2(opposite, adjacent);
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
        if (CalculateDistance(enemy.GlobalPositionCenter, GlobalPosition + sprite.getOrigin()) < 50)
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
        spriteBatch.Draw(
            sprite,
            GlobalPosition - sprite.getOrigin(),
            null,
            null,
            sprite.getOrigin(),
            rotation + 0.5f * (float)Math.PI,
            null,
            Color.White,
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
