using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter;

public class Tower : GameObject
{
    protected MouseState state = new MouseState();
    public Vector2 gridPosition, pos, mousePosition, previousPosition = new Vector2(0, 0);
    public Texture2D baseTexture;
    public Texture2D cannonTexture;
    public Camera camera;
    protected Enemy target;
    float rotation;
    protected int range = 5 * Constant.NODE_SIZE.X;
    int[] levels; // {cost, damage, firerate, radius }
    int cost;
    int damage;

    public Tower()
    {
        baseTexture = Sprites.SPR_ABSTRACT_TOWER; //Texture of the (unanimated) base of the tower
        cannonTexture = Sprites.SPR_ABSTRACT_CANNON; // The moving part of a tower
        levels = new int[] {
            0,0,0,0
        };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        pos = gridPosition * NODE_SIZE.X + GlobalPosition;
        spriteBatch.Draw(baseTexture, pos);
        spriteBatch.Draw(cannonTexture, pos + new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //calculate the rotation of the cannonbarrel
            double opposite = findTarget().Y - cannonTexture.Width / 2 - pos.Y;
            double adjacent = findTarget().X - cannonTexture.Width / 2 - pos.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;

        mousePosition = inputHelper.MousePosition;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public virtual Vector2 findTarget()
    {

        //if there already is a target that is within the tower range
        if (target != null && CalculateDistance(target.Position, pos) < range)
        {
            previousPosition = target.Position;
            return target.Position;
        }
        else
            //necessary so that the tower only follows enemies withing range
            target = null;

        //set distance to find a new enemy that is under that distance
        double distance = range;


        // look through all the enemies
        foreach (Enemy x in camera.FindByType<Enemy>())
        {
            //Calculate the distance of the target
            double enemyDistance = CalculateDistance(x.Position, pos);
            //if the enemy is closer than the last;
            if (enemyDistance <= distance)
            {
                distance = enemyDistance;
                //set this enemy as the target
                target = x;    
            }
        }
        //if a target is found
        if (target != null)
            return target.Position;
        else
            return previousPosition + GlobalPosition;

    }

    public virtual void Attack()
    {

    }

    public virtual void Upgrade()
    {

    }

    //returns the length of the direct line between two points
    public double CalculateDistance(Vector2 A, Vector2 B)
    {
        double K = A.Y - B.Y;
        double L = A.X - B.X;
        double distance = Math.Sqrt(K * K + L * L);
        return distance;
    }
}
