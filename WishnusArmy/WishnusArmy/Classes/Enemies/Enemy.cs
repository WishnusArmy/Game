﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Constant;

public partial class Enemy : GameObject
{
    public Texture2D Sprite;
    float rotation, healthRatio;
    Vector2 target = new Vector2(200,200), startPosition = new Vector2(200,200);
    float speed = 5;
    int health = ENEMY_HEALTH[0];
    List<GridNode> path;
    int pathIndex;

    public Enemy()
    {
        //position = startPosition + GlobalPosition;
        this.Sprite = SPR_ENEMY;
        healthRatio = (float)this.Sprite.Width / (float) this.health;
        path = new List<GridNode>();
        pathIndex = 0;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        /*
        if (inputHelper.KeyPressed(Keys.P))
        {
            foreach(GridNode node in plane.grid)
            {
                node.beacon = false;
            }
            path = getPath(GlobalPosition, new Vector2(500, 500));
            foreach(GridNode node in path)
            {
                node.beacon = true;
            }
        }
        */
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (pathIndex == 0)
        {
            GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
            foreach(GridNode node in plane.grid)
            {
                node.beacon = false;
            }

            try
            {
                Vector2 p = Position;
                if (p.X < 0) { p.X = 0; }
                if (p.Y < 0) { p.Y = 0; }
                path = getPath(p, new Vector2(RANDOM.Next(1500) + 128, RANDOM.Next(600) + 100));
            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            foreach(GridNode node in path)
            {
                node.beacon = true;
            }
            pathIndex = path.Count - 1;
        }
        target = path[pathIndex].Position;
        base.Update(gameTime);
        //Enemy is in de goede richting gedraaid
        double opposite = target.Y - position.Y;
        double adjacent = target.X - position.X;
        rotation = (float)Math.Atan2(opposite, adjacent);

        
        //The position never truly equals the target position so 5 pixels lower or higher.
        if (CalculateDistance(target, position) < 5)
        {
            //target = new Vector2((int)(1000 * Constant.RANDOM.NextDouble()), Constant.RANDOM.Next(1000));
            pathIndex -= 1;
        }
        

        //sprite beweegt richting de muis met vaste snelheid (speed)
        velocity = (target - position);

        //als velocity 0,0 is krijg je deling door 0
        if (velocity != new Vector2(0, 0))
        {
            velocity *= (speed / (Math.Abs(velocity.X) + Math.Abs(velocity.Y)));
        }

        position += velocity;

        // tijdelijk toegevoegd door maurin
        // zet visible naar false als health < 0
        Kill = !IsAlive;

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(Sprite, GlobalPosition + new Vector2(NODE_SIZE.X, NODE_SIZE.Y)/2, null, null, new Vector2(Sprite.Width/2,Sprite.Height/2), rotation);

        //draw Healthbar, above the enemy. The healthRatio sets the width of the healthbar to the width of the sprite.
        DrawingHelper.DrawRectangleFilled(new Rectangle((int)GlobalPosition.X - (int)(health * healthRatio)/2,(int) GlobalPosition.Y -Sprite.Height -10,(int)((float)health * healthRatio),10), spriteBatch, Color.Black);
    }

    // deal damage to enemy
    public int DealDamage
    {
        set { health -= value; }
    }

    // hiermee kunnen alle enemies uit de lijst verwijderd worden dmv !enemy.IsAlive
    public bool IsAlive
    {
        get { return health > 0; }
    }

}
