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

public abstract class Tower : GameObjectList
{
    protected MouseState state = new MouseState();
    public Vector2 gridPosition, mousePosition, previousPosition = new Vector2(0, 0);
    public Texture2D baseTexture;
    protected Enemy target;
    protected Vector2 targetPos;
    public GridNode myNode;
    public bool hover;
    protected int damage, cost, level = 0, range = 5 * Constant.NODE_SIZE.X;
    protected double reloadTime;
    int[] levels; // {cost, damage, firerate, radius }


    public Tower() : base()
    {
        baseTexture = SPR_ABSTRACT_TOWER; //Texture of the (unanimated) base of the tower
        reloadTime = 1d / Constant.FIRE_RATE[level];
        levels = new int[] {
            0,0,0,0
        };
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (hover)
            spriteBatch.Draw(SPR_CIRCLE, GlobalPosition, null, null, new Vector2(SPR_CIRCLE.Width / 2, SPR_CIRCLE.Height / 2), 0f, new Vector2(1f, 1f) * ((float)range / ((float)SPR_CIRCLE.Width / 2)), new Color(0.2f, 0.2f, 0.2f, 0.05f));
    spriteBatch.Draw(baseTexture, GlobalPosition - new Vector2(baseTexture.Width, baseTexture.Height) / 2, Color.White);

    }

    public Vector2 DrawPosition //Correction for being on the grid.
    {
        get { return GlobalPosition + new Vector2(NODE_SIZE.X/2, NODE_SIZE.Y/2); }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        mousePosition = inputHelper.MousePosition;

        //check if mouse is hovering over tower
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (myNode == null)
        {
            myNode = MyPlane.NodeAt(GlobalPosition);
            myNode.solid = true;
        }
        hover = myNode.selected;
        reloadTime -= gameTime.ElapsedGameTime.TotalSeconds;
        if (target == null)
        {
            target = getTarget();
        }
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
