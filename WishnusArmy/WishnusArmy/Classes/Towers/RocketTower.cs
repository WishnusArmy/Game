using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;


class RocketTower : Tower  
{
    static int maxRockets = 3;

    public RocketTower() : base()
    {
        damage = Constant.BULLET_DAMAGE[level];  
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
        GridPlane plane = parent as GridPlane;
        List<Enemy> enemies = plane.FindByType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (FindByType<Rocket>().Count < maxRockets && CalculateDistance(enemy.Position, position) < range)
            {
                Add(new Rocket(damage, 8, GlobalPosition));
                return;
            }
        }
    }
}
