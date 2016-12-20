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
    public Enemy target;

    // Call Laser like 
    //      Laser(towerPosition)
    // Target new enemy as 
    //      laser.Target = TargetPosition

    public Laser() : base()
    {
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        if (target != null)
            DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target.GlobalPositionCenter, Color.Red, 16);
    }

    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (target != null)
          target.health -= damage;
    }
}
