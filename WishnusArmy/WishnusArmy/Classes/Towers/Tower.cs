using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Sprites;

public class Tower : GameObject
{
    protected MouseState state = new MouseState();
    public Vector2 gridPosition, pos, mousePosition, previousPosition = new Vector2(0, 0);
    public Texture2D baseTexture;
    public Texture2D cannonTexture;
    protected Enemy target;
    public Boolean hover = false;
    float rotation;
    protected int damage, cost, level = 0, range = 5 * Constant.NODE_SIZE.X;
    protected double reloadTime;
    int[] levels; // {cost, damage, firerate, radius }


    public Tower() : base()
    {
        baseTexture = SPR_ABSTRACT_TOWER; //Texture of the (unanimated) base of the tower
        cannonTexture = SPR_ABSTRACT_CANNON; // The moving part of a tower
        reloadTime = 1d / Constant.FIRE_RATE[level];
        levels = new int[] {
            0,0,0,0
        };
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        pos = gridPosition * NODE_SIZE.X + GlobalPosition;
        spriteBatch.Draw(baseTexture, pos);
        spriteBatch.Draw(cannonTexture, pos + Origin, null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);

        //draw the radius
        if (hover)
            spriteBatch.Draw(SPR_RADIUS, pos + Origin, null, null, new Vector2(SPR_RADIUS.Width / 2, SPR_RADIUS.Height / 2),0f, new Vector2(1f,1f) *((float)range/((float)SPR_RADIUS.Width/2)), new Color(0.2f,0.2f,0.2f, 0.1f));
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //calculate the rotation of the cannonbarrel
            double opposite = findTarget().Y - cannonTexture.Width / 2 - pos.Y;
            double adjacent = findTarget().X - cannonTexture.Width / 2 - pos.X;
            rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;

        mousePosition = inputHelper.MousePosition;

        //check if mouse is hovering over tower
        if (range != 0 && BoundingBox.Contains(mousePosition)) { hover = true; }
        else {hover = false; }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        reloadTime -= gameTime.ElapsedGameTime.TotalSeconds;
        if (target != null && reloadTime <= 0)
        { 
            Attack();
            reloadTime = 1d/Constant.FIRE_RATE[level];
            
        }
            
    }


    public virtual Vector2 findTarget()
    {
        if (target != null && target.Visible == false)
            target = null;
        //if there already is a target that is within the tower range
        if (target != null && Constant.DISTANCE(target.Position - target.Origin, pos + Origin ) < range)
        {
            previousPosition = target.Position;
            return target.Position;
        }
            //necessary so that the tower only follows enemies within range
        else
            target = null;
        
        //set distance to find a new enemy that is under that distance
            double distance = range;


        // look through all the enemies
        foreach (Enemy x in GameWorld.FindByType<Enemy>())
        {
            if (x.Visible == true)
            {
                //Calculate the distance of the target
                double enemyDistance = CalculateDistance(x.Position - x.Origin, pos + Origin);
                //if the enemy is closer than the last;
                if (enemyDistance <= distance)
                {
                    distance = enemyDistance;
                    //set this enemy as the target
                    target = x;
                }
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
    public override Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)pos.X, (int)pos.Y, baseTexture.Width, baseTexture.Height);
        }
    }
    public Vector2 Origin
    {
        get { return new Vector2(baseTexture.Width/2, baseTexture.Height/2); }
    }

}
