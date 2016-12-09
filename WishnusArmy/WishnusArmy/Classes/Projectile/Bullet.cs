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
    public Bullet(int damage, Vector2 velocity, Vector2 startPosition) : base()
    {
        this.damage = damage;
        this.velocity = velocity;
        Position = startPosition;

    }
    public bool CheckCollision()
    {
        return true;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        Position += this.velocity;
        base.Update(gameTime);
    }
}
