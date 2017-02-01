using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static DrawingHelper;
using static GameStats;
using static ContentImporter.Fonts;

public abstract partial class Enemy : IsometricMovingGameObject
{
    public GridNode startNode;
    GridNode targetNode;
    protected float speed;
    protected int killReward;
    double _health;
    public int damage = 10;
    int maxHealth;
    public HealthText healthText;
    public enum Type {Tank, Soldier, AirBaloon, Helicopter, Airplane }
    public Type type;
    public bool wait;
    public GridNode waitAt;
    GridNode centerNode;
    public List<GridNode> path;
    public int pathIndex;
    public Tower.Type weakness, strongness;
    public int cost;


    public Enemy(Type type, Texture2D sprite, int SheetIndex = 0) 
        : base(sprite, SheetIndex)

    {
        damage = EnemyDamage(type);
        this.type = type;
        pathIndex = 0;
        maxHealth = EnemyHealth(type);
        _health = (int)maxHealth;
        //allEnemies.Add(this);
    }

    public void dealDamage(double damage, Tower.Type type)
    {
        health -= damage * (1 + 0.5 * (type == weakness).ToInt()) * (1 / (1 + 0.5 * (type == strongness).ToInt()));
    }

    double health
    {
        get { return _health;  }
        set
        {
            if (healthText == null || healthText.p > 0.7f)
            {
                float deltaHealth = (float)(value - _health);
                if (deltaHealth > _health)
                    deltaHealth = (float)_health;
                healthText = new HealthText(((int)(deltaHealth)).ToString(), deltaHealth/maxHealth) { Position = GlobalPosition + new Vector2(0, -15) };
                MyPlane.Add(healthText);
            }
            else
            {
                try 
                {
                    healthText.text = Int32.Parse(healthText.text) + (int)(value - _health) + "";
                    if (Int32.Parse(healthText.text.Substring(0, 2)) > maxHealth)
                        healthText.text = maxHealth.ToString();
                }
                catch (Exception e)
                {
                }
                healthText.timer /= 2;
            }
            _health = value;
            if (_health <= 0 && !kill)
            {
                kill = true;
                TotalEnemiesKilled++;
                EcResources += killReward;
                totalResourcesGathered += killReward;
                if (!GameWorld.FindByType<Base>()[0].InUltimate)
                    GameWorld.FindByType<UltimateTimer>()[0].Timer += 150;
                //PlaySound(SND_WILHELM_SCREAM);
            }
        }
    }

    public override void Update(object gameTime)
    {
        if (centerNode == null)
            centerNode = MyPlane.CenterNode;
        base.Update(gameTime);

        if (path == null && startNode != null)
        {
            path = new List<GridNode>();
            requestPath();
        }

        if (path != null && path.Count > 0 && !Kill)
        {
            velocity = Vector2.Zero;
            moveAlongPath();
        }

        if (healthText != null)
        {
            healthText.Position = GlobalPositionCenter - GlobalPosition + Position - new Vector2(0, 40);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        
        //draw Healthbar, above the enemy
        DrawRectangle(new Rectangle(GlobalPosition.ToPoint() - new Point(50 - sheetRec.Width / 2, 60), new Point(100, 14)), spriteBatch, Color.Black, 2, 1f);
        Color healthColor = new Color((int)(255 * (1 - (health / maxHealth))), (int)(255 * (health / maxHealth)), 0, 255);
        DrawRectangleFilled(new Rectangle(GlobalPosition.ToPoint() - new Point(50 - sheetRec.Width/2, 60), new Point((int)((health/maxHealth)*100), 14)), spriteBatch, healthColor, 0.8f);
        DrawText(spriteBatch, FNT_LEVEL_BUILDER, ((int)(health)).ToString(), GlobalPosition + new Vector2(sheetRec.Width / 2, -53), Color.Black, true);
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
        if (CalculateDistance(targetNode.Position, position) < speed)
        {
            if (pathIndex > 0)
            {
                if (path[pathIndex - 1].solid && !wait && !(this is EnemyAir)) //Path has changed on the way.
                {
                    GridNode solidNode = path[pathIndex - 1];
                    startNode = path[pathIndex]; //set the startNode for the request
                    requestPath(); //Request a new path around the obstacle
                    List<Enemy> enemies = ObjectLists.Enemies;
                    enemies = enemies.OrderBy(o => o.CalculateDistance(o.position, position)).ToList();
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.path != null)
                        {
                            if (enemy.path.Contains(solidNode))
                            {
                                enemy.startNode = enemy.path[enemy.pathIndex];
                                enemy.wait = true;
                                enemy.waitAt = solidNode;
                                enemy.requestPath();
                            }
                        }
                    }
                    if (path.Count == 0)
                    {
                        Kill = true;
                    }
                }
                else
                {
                    if (path[pathIndex - 1] != waitAt)
                    {
                        path.RemoveAt(pathIndex);
                        pathIndex -= 1;
                    }
                }
            }
            else
            {
                GameStats.BaseHealth -= damage;
                Kill = true;
            }
        }

        //sprite beweegt richting de muis met vaste snelheid (speed)
        velocity = (targetNode.Position - position);

        //als velocity 0,0 is krijg je deling door 0
        if (velocity != new Vector2(0, 0))
        {
            velocity *= (speed / ((Math.Abs(velocity.X) + Math.Abs(velocity.Y))));
        }
    }
    
    public Vector2 GlobalPositionCenter
    {
        get
        {
            return GlobalPosition + NODE_SIZE.toVector()/2;
        }
    }

    // hiermee kunnen alle enemies uit de lijst verwijderd worden dmv !enemy.IsAlive
    bool IsAlive
    {
        get { return health > 0; }
    }

}
