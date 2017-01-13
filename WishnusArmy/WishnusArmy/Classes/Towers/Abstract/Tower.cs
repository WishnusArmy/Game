﻿using Microsoft.Xna.Framework;
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
    public Texture2D baseTexture;
    public Vector2 gridPosition, mousePosition, previousPosition = new Vector2(0, 0);
    protected Enemy target;
    public GridNode myNode;
    public bool hover;
    public Type type;
    public int[] stats;
    int timer;

    public enum Type { RocketTower, LaserTower, PulseTower, Base}

    public Tower(Type type) : base()
    {
        this.type = type;
        stats = new int[] {0, 0, 0}; // damage, range, rate
        timer = 0;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (hover)
            spriteBatch.Draw(SPR_CIRCLE, GlobalPosition, null, null, new Vector2(SPR_CIRCLE.Width / 2, SPR_CIRCLE.Height / 2), 0f, new Vector2(1f, 0.5f) * ((float)TowerRange(type, stats) / ((float)SPR_CIRCLE.Width / 2)), new Color(0.2f, 0.2f, 0.2f, 0.05f));
        spriteBatch.Draw(baseTexture, GlobalPosition, null, null, new Vector2(baseTexture.Width / 2, baseTexture.Height / 2));
        base.Draw(gameTime, spriteBatch);
    }

    public Vector2 DrawPosition //Correction for being on the grid.
    {
        get { return GlobalPosition + new Vector2(NODE_SIZE.X/2, NODE_SIZE.Y/2); }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && hover)
        {
            GameWorld.FindByType<Overlay>()[0].TowerInfo.tower = this;
        }
    }

    public bool canShoot
    {
        get { return timer <= 0; }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (timer <= 0)
            timer = TowerRate(type, stats);
        else if (timer > 0)
            timer--;


        if (myNode == null)
        {
            myNode = MyPlane.NodeAt(GlobalPosition);
            myNode.solid = true;
            myNode.setDval(myNode, TowerRange(type, stats), new List<GridNode>(), (int)TowerDamage(type, stats)*150);
        }
        hover = myNode.selected; //check if the  mouse is hovering the tower

        //if target is out of range
        if (target != null && DISTANCE(target.GlobalPositionCenter, GlobalPosition) > TowerRange(type, stats))
            target = null;

        if (target == null || target.Kill)
        {
            target = findTarget();
        }
    }

    public virtual Enemy findTarget()
    {
        List<Enemy> enemies = MyPlane.FindByType<Enemy>();
        for(int i=enemies.Count-1; i>=0; --i)
        {
            if (CalculateDistance(GlobalPosition, enemies[i].GlobalPositionCenter) > TowerRange(type, stats))
            {
                enemies.RemoveAt(i);
            }
        }
        if (enemies.Count > 0)
            return enemies[RANDOM.Next(enemies.Count)];

        return null;
    }

    public virtual void Attack()
    {
    }
}
