using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

class Laser : Projectile
{
    public Laser() : base()
    {
        
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (HasTarget)
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.GlobalPositionCenter, Color.Red, 10);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (HasTarget)
        {
            target.health -= (int)damage;
        }
    }
}
