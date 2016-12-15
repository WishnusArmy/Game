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
            this.target = value;
        }
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (target == null || !visible)
            return;
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawLine(spriteBatch, GlobalPosition, target, new Color(255 - timer*2,0, timer*5), 16);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        Target = inputHelper.MousePosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        timer++;
        
        if (timer > LASER_TIME)
        {
            visible = false;
        }
        if (timer > LASER_TIME * 2)
        {
            Reset();
        }
    }

    private void Reset()
    {
        timer = 0;
        visible = true;
    }

}
