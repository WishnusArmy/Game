using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;

class PulseTower : Tower
{
    public PulseTower() : base()
    {
        type = 2;
        sprite = SPR_PULSE_TOWER;
        Pulse p = new Pulse(TowerDamage(type, stats), TowerRange(type, stats), TowerRate(type, stats));
        Add(p);
    }
   
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.Attack();
    }
}


