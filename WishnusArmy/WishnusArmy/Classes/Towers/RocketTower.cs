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
    public RocketTower() : base()
    {
        type = 0;
        sprite = ContentImporter.Sprites.SPR_ABSTRACT_TOWER;
        target = findTarget();
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
        if (children.Count < (int)TowerRate(type, stats))
        {
            Bullet b = new Bullet((int)TowerDamage(type, stats), TowerRange(type, stats), BULLET_SPEED);
            b.Target = target;
            Add(b);
        /*
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
        */
        }
        base.Attack();
    }
}
