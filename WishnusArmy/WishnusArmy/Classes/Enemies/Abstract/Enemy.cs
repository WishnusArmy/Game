using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Constant;

public abstract partial class Enemy : GameObject
{

    public Texture2D sprite;
    float rotation;
    public GridNode startNode;
    GridNode targetNode;
    float speed = 5;
    double _health = ENEMY_HEALTH[0];
    public double health
    {
        get { return _health;  }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                kill = true;
                PlaySound(SND_ENEMY_DYING);
            }
        }
    }
    List<GridNode> path;
    int pathIndex;

    public Enemy() : base()
    {
        pathIndex = 0;
    }

    public Vector2 GlobalPositionCenter
    {
        get
        {
            return GlobalPosition + new Vector2(sprite.Width, sprite.Height) / 2;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (path == null && startNode != null)
        {
            GridPlane plane = Parent as GridPlane;
            path = getPath(startNode);
            pathIndex = path.Count - 1;
        }

        if (path != null)
        {
            moveAlongPath();
        }
             
       
    }

    public void moveAlongPath()
    {
        if (pathIndex >= 0)
        {
            targetNode = path[pathIndex];
        }

        //Enemy is in de goede richting gedraaid
        double opposite = targetNode.Position.Y - position.Y;
        double adjacent = targetNode.Position.X - position.X;
        rotation = (float)Math.Atan2(opposite, adjacent);


        //The position never truly equals the target position so 5 pixels lower or higher.
        if (CalculateDistance(targetNode.Position, position) < 5)
        {
            if (pathIndex > 0)
            {
                if (path[pathIndex-1].solid) //Path has changed on the way.
                {
                    path = getPath(path[pathIndex]);
                    if (path.Count == 0)
                        kill = true;
                    pathIndex = path.Count - 1;
                }
                else
                {
                    if (path[pathIndex-1].congestion <= 1)
                        pathIndex -= 1;
                }
            }
            else
            {
                kill = true;
                position = targetNode.Position;
            }
        }


        //sprite beweegt richting de muis met vaste snelheid (speed)
        velocity = (targetNode.Position - position);

        //als velocity 0,0 is krijg je deling door 0
        if (velocity != new Vector2(0, 0))
        {
            velocity *= (speed / (Math.Abs(velocity.X) + Math.Abs(velocity.Y)));
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(sprite, GlobalPosition + new Vector2(NODE_SIZE.X, NODE_SIZE.Y)/2, null, null, new Vector2(sprite.Width/2,sprite.Height/2), rotation);

        //draw Healthbar, above the enemy
        DrawingHelper.DrawRectangleFilled(new Rectangle(GlobalPosition.ToPoint() + new Point(0, -60), new Point((int)health, 15)), spriteBatch, Color.Black);
    }

    // hiermee kunnen alle enemies uit de lijst verwijderd worden dmv !enemy.IsAlive
    public bool IsAlive
    {
        get { return health > 0; }
    }

}
