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
using WishnusArmy.GameManagement;

public partial class Enemy : GameObject
{

    public Texture2D sprite;
    float rotation, healthRatio;
    Vector2 target = new Vector2(200,200), startPosition = new Vector2(200,200);
    float speed = 5;
    int _health = ENEMY_HEALTH[0];
    public int health
    {
        get { return _health;  }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                kill = true;
                SoundManager soundManager = new SoundManager();
                soundManager.PlaySound(SND_ENEMY_DYING);
            }
        }
    }
    List<GridNode> path;
    int pathIndex;

    public Enemy()
    {
        //position = startPosition + GlobalPosition;
        this.sprite = SPR_ENEMY;
        healthRatio = (float)this.sprite.Width / (float) this.health;
        path = new List<GridNode>();
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
        if (pathIndex == 0)
        {
            GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
            try
            {
                GridNode node = plane.NodeAt(position);
                path = getPath(node, plane.NodeAt(new Vector2(RANDOM.Next(2000) + 128, RANDOM.Next(600) + 100)));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            pathIndex = path.Count - 1;
        }
        if (pathIndex >= 0)
            target = path[pathIndex].Position;
        else
            target = position;
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
        spriteBatch.Draw(sprite, GlobalPosition + new Vector2(NODE_SIZE.X, NODE_SIZE.Y)/2, null, null, new Vector2(sprite.Width/2,sprite.Height/2), rotation);

        //draw Healthbar, above the enemy. The healthRatio sets the width of the healthbar to the width of the sprite.
        DrawingHelper.DrawRectangleFilled(new Rectangle((int)GlobalPosition.X - (int)(health * healthRatio)/2,(int) GlobalPosition.Y -sprite.Height -10,(int)((float)health * healthRatio),10), spriteBatch, Color.Black);
    }

    // hiermee kunnen alle enemies uit de lijst verwijderd worden dmv !enemy.IsAlive
    public bool IsAlive
    {
        get { return health > 0; }
    }

}
