using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
class ProjectileTower : Tower
        
{
    List<Bullet> bulletList;
    int maxBullets;

    public ProjectileTower() : base()
    {
        damage = Constant.BULLET_DAMAGE[level];
        bulletList = new List<Bullet>();
        maxBullets = 3;
        
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        foreach (Bullet x in bulletList)
        {
            x.Draw(gameTime, spriteBatch);
        }
    }
    
    public override void Update(GameTime gameTime)
    {
        Attack();
    }
    public override void Attack()
    {
        if (GameWorld.FindByType<Bullet>().Count < maxBullets)
        {
            GameWorld.AddToGameWorld(new Bullet(damage, 6, Position));
        }
    }
}
