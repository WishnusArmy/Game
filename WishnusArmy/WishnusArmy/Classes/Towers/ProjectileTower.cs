using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


class ProjectileTower : Tower  
{
    int maxBullets;

    public ProjectileTower() : base()
    {
        damage = Constant.BULLET_DAMAGE[level];
        maxBullets = 3;   
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Attack();
    }
    public override void Attack()
    {
        if (GameWorld.FindByType<Bullet>().Count < maxBullets)
        {
            Add(new Bullet(damage, 6, GlobalPosition));
        }
    }
}
