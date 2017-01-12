using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Constant;


class RocketTower : Tower
{
    static int maxRockets = 3;

    public RocketTower() : base(Type.RocketTower)
    {
        baseTexture = ContentImporter.Sprites.SPR_ROCKET_TOWER;
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
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (FindByType<Rocket>().Count < maxRockets && 
                canShoot && 
                CalculateDistance(enemy.Position, position) < TowerRange(type, stats))
            {
                Add(new Rocket((int)TowerDamage(type, stats), BULLET_SPEED));
                return;
            }
        }
    }
}
