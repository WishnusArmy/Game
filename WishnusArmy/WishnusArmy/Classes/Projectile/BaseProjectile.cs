using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

class BaseProjectile : Rocket
    {
    Boolean shooting = false;
    float speed;
    int homingRange = 500;
    List<Enemy> enemies;

    public BaseProjectile(double damage, float speed) : base(damage, speed)
    {
        sprite = SPR_ROCKET;
        this.damage = damage;
        this.speed = speed;
    }
    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        if (OutOfScreen())
        {
            Kill = true;
        }
    }

    public override Enemy findTarget()
    {
        if (enemies == null)
           enemies = MyPlane.FindByType<Enemy>();
        enemies = enemies.OrderBy(o => o.CalculateDistance(o.GlobalPositionCenter, GlobalPositionCenter)).ToList();
        if (enemies.Count > 0)
        {
            if (CalculateDistance(enemies[0].GlobalPositionCenter, GlobalPositionCenter) <= homingRange)
            return enemies[0];
        }
        return null;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (target == null && !shooting)
        {
            
            Vector2 mousePos = inputHelper.MousePosition / Camera.scale;
            double opposite = mousePos.Y - GlobalPositionCenter.Y;
            double adjacent = mousePos.X - GlobalPositionCenter.X;
            rotation = (float)Math.Atan2(opposite, adjacent);
            targetRotation = (float)Math.Atan2(opposite, adjacent);
            shooting = true;
        }
    }
    public Boolean OutOfScreen()
    {
        if ((GlobalPosition.X < -100) || GlobalPosition.Y < -100 || GlobalPosition.X > (LEVEL_SIZE.X * NODE_SIZE.X) || GlobalPosition.Y > (LEVEL_SIZE.Y * NODE_SIZE.Y))
            return true;
        return false;
    }



}