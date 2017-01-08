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
    protected Texture2D sprite;
    public Vector2 gridPosition, mousePosition, previousPosition = new Vector2(0, 0);
    protected Enemy target;
    public GridNode myNode;
    public bool hover;
    protected float rotation;
    protected int type;
    protected int[] stats;

    public Tower() : base()
    {
        stats = new int[] {0, 0, 0}; // damage, range, rate
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (hover)
            spriteBatch.Draw(SPR_CIRCLE, GlobalPosition, null, null, new Vector2(SPR_CIRCLE.Width / 2, SPR_CIRCLE.Height / 2), 0f, new Vector2(1f, 1f) * ((float)TowerRange(type, stats) / ((float)SPR_CIRCLE.Width / 2)), new Color(0.2f, 0.2f, 0.2f, 0.05f));
        spriteBatch.Draw(sprite, GlobalPosition, null, null, new Vector2(sprite.Width / 2, sprite.Height / 2));
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
            GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
            myNode = plane.NodeAt(GlobalPosition);
            myNode.solid = true;
            myNode.beacon = true;
            hover = myNode.selected;
        } else { hover = myNode.selected; }

       

        if (target == null || target.Kill)
        {
            target = findTarget();
        }
    }

    public virtual Enemy findTarget()
    {
        List<Enemy> enemies = GameWorld.FindByType<Enemy>();
        if (enemies.Count == 0)
            return null;
        List<Enemy> inrange = new List<Enemy>();
        foreach(Enemy x in enemies)
        {
            if (CalculateDistance(GlobalPosition, x.GlobalPosition) <= TowerRange(type, stats))
            {
                inrange.Add(x);
            }
        }
        if (inrange.Count > 0)
            return inrange[RANDOM.Next(0, inrange.Count)];

        return null;
    }

    public virtual void Attack()
    {
        foreach (Projectile p in children)
        {
            if (!p.HasTarget)
            {
                p.Target = findTarget();
            }
        }
    }
    
    protected void UpgradeStat(int stat)
    {
        if (stats[stat] >= 5)
            return;
        stats [stat] = stats[stat] + 1;
        
        foreach (Projectile p in children)
        {
            p.Damage = TowerDamage(type, stats);
            p.Range = TowerRange(type, stats);
            p.Rate = TowerRate(type, stats);
        }
    }

    protected void DowngradeStat(int stat)
    {
        if (stats[stat] <= 0)
            return;
        stats[stat] = stats[stat] - 1;
    }

    public void UpgradeDamage()
    {
        UpgradeStat(0);
    }
    public void UpgradeRange()
    {
        UpgradeStat(1);
    }
    public void UpgradeRate()
    {
        UpgradeStat(2);
    }
    


    
}
