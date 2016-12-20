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

public class Tower : GameObjectList
{
    protected MouseState state = new MouseState();
    public Vector2 gridPosition, mousePosition, previousPosition = new Vector2(0, 0);
    public Texture2D baseTexture;
    public Texture2D cannonTexture;
    protected Enemy target;
    protected Vector2 targetPos;
    public GridNode myNode;
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
        spriteBatch.Draw(baseTexture, GlobalPosition);
        spriteBatch.Draw(cannonTexture, GlobalPosition + new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);

        if (hover)
            spriteBatch.Draw(SPR_RADIUS, GlobalPosition + new Vector2(baseTexture.Width/2, baseTexture.Height/2), null, null, new Vector2(SPR_RADIUS.Width / 2, SPR_RADIUS.Height / 2),0f, new Vector2(1f,1f) *((float)range/((float)SPR_RADIUS.Width/2)), new Color(0.2f,0.2f,0.2f, 0.1f));
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //calculate the rotation of the cannonbarrel
        if (target != null)
        {
            targetPos = target.Position;
        }
        double opposite = targetPos.Y - cannonTexture.Width / 2 - GlobalPosition.Y;
        double adjacent = targetPos.X - cannonTexture.Width / 2 - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;

        mousePosition = inputHelper.MousePosition;

        //check if mouse is hovering over tower
        if (range != 0 && BoundingBox.Contains(mousePosition)) { hover = true; }
        else { hover = false; }
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

    public virtual Enemy getTarget()
    {
        List<Enemy> enemies = GameWorld.FindByType<Camera>()[0].currentPlane.FindByType<Enemy>();
        foreach(Enemy x in enemies)
        {
            if (x.CalculateDistance(GlobalPosition, x.GlobalPosition) <= range)
            {
                return x;
            }
        }
        return null;
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
            return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, baseTexture.Width, baseTexture.Height);
        }
    }

}
