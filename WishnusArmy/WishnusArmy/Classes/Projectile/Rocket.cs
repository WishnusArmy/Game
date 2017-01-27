using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;
using static Constant;

public class Rocket : Projectile
{
    public float rotation;
    float speed;
    public float targetRotation;

    public Rocket(double damage, float speed) : base(damage)
    {
        sprite = SPR_ROCKET;
        this.damage = damage;
        this.speed = speed;
        rotation = 0;
        targetRotation = 0;
    }

    public virtual Enemy findTarget()
    {
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        enemies = enemies.OrderBy(o => o.CalculateDistance(o.GlobalPositionCenter, GlobalPositionCenter)).ToList();
        if (enemies.Count > 0)
            return enemies[0];
        return null;
    }

    private void calculateCourse()
    {
        if (target != null)
        {
            double opposite = (target.GlobalPositionCenter.Y) - (GlobalPosition.Y);
            double adjacent = (target.GlobalPositionCenter.X) - (GlobalPosition.X);
            rotation = (float)Math.Atan2(opposite, adjacent);
        }
        else
        {
           rotation = rotation + 0.05f;
        }
        float x = (float)Math.Cos(rotation) * speed;
        float y = (float)Math.Sin(rotation) * speed;
        velocity = new Vector2(x, y);
    }


    public void CheckCollision()
    {
        if (CalculateDistance(target.GlobalPositionCenter, GlobalPosition) < speed)
        {
            target.dealDamage(damage, Tower.Type.RocketTower);
            Kill = true;
            Vector2 pos = position + parent.Position;
            MyParticleControl.AddExplosion(pos);
            PlaySound(SND_ROCKET_IMPACT);
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
            new Vector2(1f, 1f - 0.5f*(float)Math.Abs(Math.Sin(rotation))),
            Color.White,
            SpriteEffects.None,
            0f);
    }

    public override void Update(object gameTime)
    {
        rotation = targetRotation;
        base.Update(gameTime);
        //findTarget();
        if (!HasTarget)
        {
            target = findTarget();
        }
        else
        { 
            CheckCollision();
        }
        calculateCourse();
    }


}
