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
        damage = Constant.PULSE_DAMAGE[level];
        this.baseTexture = SPR_PULSE_TOWER;
        Pulse p = new Pulse(0, 500);
        p.Position = new Vector2(NODE_SIZE.X/4, 0);
        Add(p);
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
    }
    public override void Upgrade()
    {
        base.Upgrade();
    }
}
    
