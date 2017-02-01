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
    public override void Update(object gameTime)
    {
        base.Update(gameTime);
    }
    public override void Attack()
    {
        //foreach (Enemy enemy in enemies)
        //{
        if ( FindByType<Rocket>().Count < maxRockets)
            {
            Add(new Rocket((int)TowerDamage(type, stats), BULLET_SPEED));
                return;
            
            }
       // }
        
    }
}
