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
    private int rotation;
    Vector2 target;

    public Bullet(int damage, Vector2 velocity, Vector2 startPosition) : base()
    {
        this.damage = damage;
        Position = startPosition;
        this.rotation = 0;
        this.velocity = velocity;
    }
    
    public Vector2 Target
    {
        set
        {
            this.target = value;
        }
    }

    public void CheckCollision()
    {
        return;
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
        spriteBatch.Draw(SPR_BULLET, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, SPR_BULLET.Width, SPR_BULLET.Height), null, Color.White, 180, origin, SpriteEffects.None, 0f);
        
    }

    public override void Update(GameTime gameTime)
    {
        if (!visible)
            return;
        base.Update(gameTime);
        Position += this.velocity;
        CheckCollision();
    }
}
