using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

class BaseProjectile : Rocket
    {
    Vector2 cameraPosition;
    Camera c = new Camera();
    Boolean shooting = false;
    float speed;
    int explosionRadius = 500;
    float distance;
    Vector2 mousePos, targetPos, adjustment, cameraPos;

    public BaseProjectile(double damage, float speed) : base(damage, speed)
    {
        sprite = SPR_CANNONBALL;
        this.damage = damage;
        this.speed = speed;
    }
    public override void Update(object gameTime)
    {
        c = GameWorld.FindByType<Camera>()[0];
        cameraPosition = c.Position;
        base.Update(gameTime);
        if (OutOfScreen())
        {
            Kill = true;
        }
        if (shooting && CalculateDistance(GlobalPositionCenter, targetPos) < 10)
        {
            Explode();
        }
    }

    public override Enemy findTarget()
    {
        return null;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (target == null && !shooting)
        {
            mousePos = inputHelper.MousePosition / Camera.scale + sprite.getOrigin();
            shooting = true;
            cameraPos = cameraPosition;
            distance = CalculateDistance(GlobalPositionCenter, targetPos);
        }
        adjustment = cameraPosition - cameraPos;
        targetPos = mousePos + adjustment;
        
        double opposite = targetPos.Y - GlobalPositionCenter.Y;
        double adjacent = targetPos.X - GlobalPositionCenter.X;
        targetRotation = (float)Math.Atan2(opposite, adjacent);
    }
    public Boolean OutOfScreen()
    {
        if ((GlobalPosition.X < -100) || GlobalPosition.Y < -100 || GlobalPosition.X > (LEVEL_SIZE.X * NODE_SIZE.X) || GlobalPosition.Y > (LEVEL_SIZE.Y * NODE_SIZE.Y))
            return true;
        return false;
    }
    public void Explode()
    {
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        foreach (Enemy x in enemies)
        {
            if (CalculateDistance(GlobalPositionCenter, x.GlobalPositionCenter) < explosionRadius)
            {
                x.dealDamage(damage, Tower.Type.Base);
            }
        }
        MyParticleControl.AddExplosion(position + parent.Position);
        Kill = true;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        spriteBatch.Draw(
            sprite,
            GlobalPosition,
            null,
            null,
            sprite.getOrigin(),
            rotation + 0.5f * (float)Math.PI,
            new Vector2(1f, 1f) * (-Math.Abs(((0.5f*distance - CalculateDistance(GlobalPositionCenter, targetPos)))/distance) + 0.75f),
            Color.White,
            SpriteEffects.None,
            0f);
    }



}