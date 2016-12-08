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
    private Vector2 target;
    private int timer;


    // Call Laser like 
    //      Laser(towerPosition)
    // Target new enemy as 
    //      laser.Target = TargetPosition

    public Laser(Vector2 startPosition) : base()
    {
        Position = startPosition;
        Reset();
    }
    
    public Vector2 Target
    {
        set
        {
            if (timer > LASER_TIME)
            {
                this.target = value;
                Reset();
            }
        }
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (target == null)
            return;
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target + GlobalPosition, Color.Red, 8);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer++;
        if (timer > LASER_TIME)
            visible = false;
    }

    private void Reset()
    {
        timer = 0;
        visible = true;
    }

}
