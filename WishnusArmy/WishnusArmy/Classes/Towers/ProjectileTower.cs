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
    public ProjectileTower()
    {
        damage = Constant.BULLET_DAMAGE[level];
        bulletList = new List<Bullet>();
        range = 150;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        foreach (Bullet x in bulletList)
        {
            x.Draw(gameTime, spriteBatch);
        }
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        foreach (Bullet x in bulletList)
        {
            x.HandleInput(inputHelper);
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //remove bullets from the list that are finished
        for (int t = bulletList.Count - 1; t > -1; t--)
        {
            if (bulletList.ElementAt(t).Visible == false)
                bulletList.Remove(bulletList.ElementAt(t));
        }

        //update the bullets
        foreach (Bullet x in bulletList)
        {
            x.Update(gameTime);
        }
    }
    public override void Attack()
    {
        base.Attack();
        Bullet x = new Bullet(damage, 2, pos + new Vector2(baseTexture.Width / 2, baseTexture.Height / 2));
        x.enemy = target;
        x.Visible = true;
        bulletList.Add(x);
    }
}
