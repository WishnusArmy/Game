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

class BaseProjectile : DrawOnTop
    {
    protected Texture2D sprite;
    protected double damage;
    protected Vector2 cameraPosition;
    protected Boolean shooting = false;
    protected float speed;
    protected int explosionRadius = 500, damageMultiplierInCenter = 4;
    protected float distance;
    protected Vector2 mousePos, targetPos, adjustment, cameraPos, scale = new Vector2(1f,1f);
    protected float rotation;

    public BaseProjectile(double damage, float speed)
    {
        sprite = SPR_CANNONBALL;
        this.damage = damage;
        this.speed = speed;
    }
    public override void Update(object gameTime)
    {
        Camera c = GameWorld.FindByType<Camera>()[0];
        cameraPosition = c.Position;
        base.Update(gameTime);
        if (OutOfScreen())
        {
            Kill = true;
        }
        if (shooting && CalculateDistance(GlobalPosition, targetPos) < 15)
        {
            Explode();
            PlaySound(SND_EXPLOSION);
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (!shooting)
        {
            mousePos = inputHelper.MousePosition / Camera.scale;
            shooting = true;
            cameraPos = cameraPosition;
            distance = CalculateDistance(GlobalPosition, mousePos);
        }
        adjustment = cameraPosition - cameraPos;
        targetPos = mousePos + adjustment;
        
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent);
        float x = (float)Math.Cos(rotation) * speed;
        float y = (float)Math.Sin(rotation) * speed;
        velocity = new Vector2(x, y);
    }
    public Boolean OutOfScreen()
    {
        if ((GlobalPosition.X < -100) || GlobalPosition.Y < -100 || GlobalPosition.X > (LEVEL_SIZE.X * NODE_SIZE.X) || GlobalPosition.Y > (LEVEL_SIZE.Y * NODE_SIZE.Y))
            return true;
        return false;
    }
    public void Explode()
    {
        foreach (Enemy e in ObjectLists.Enemies)
        {
            float radius = CalculateDistance(GlobalPosition, e.GlobalPositionCenter);
            if (radius < explosionRadius)
            {
                e.dealDamage(damage*(damageMultiplierInCenter - (radius/explosionRadius)* damageMultiplierInCenter), Tower.Type.Base);
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
            scale * (-Math.Abs(((0.5f*distance - CalculateDistance(GlobalPosition, targetPos)))/distance) + 0.75f),
            Color.White,
            SpriteEffects.None,
            0f);
    }
    protected Vector2 Origin()
    {
        return new Vector2(sprite.Width / 2, sprite.Height / 2);
    }



}