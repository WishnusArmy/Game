using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using Microsoft.Xna.Framework;
using static Constant;

public class Enemy : GameObject
{
    Texture2D Sprite;
    float rotation, healthRatio;
    Vector2 position, mousePosition, target = new Vector2(200,200), startPosition = new Vector2(200,200);
    float speed = 5;
    int health = ENEMY_HEALTH[1];
    public Enemy()
    {
        this.position = startPosition + GlobalPosition;
        this.Sprite = SPR_ENEMY;
        healthRatio = (float)this.Sprite.Width / (float) this.health;
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePosition = inputHelper.MousePosition;
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        //leuk draaieffect
        double opposite = targetPosition().Y - position.Y;
        double adjacent = targetPosition().X - position.X;
        rotation = (float)Math.Atan2(opposite, adjacent);

        //sprite beweegt richting de muis met vaste snelheid (speed)
        velocity = (targetPosition() - position);

        //als velocity 0,0 is krijg je deling door 0
        if (velocity != new Vector2(0, 0))
        {
            velocity *= (speed / (Math.Abs(velocity.X) + Math.Abs(velocity.Y)));
        }

        //ziet er vaag uit maar moet denk ik zo vanwege globalpositions etc.
        startPosition += velocity;
        position = startPosition + GlobalPosition;

        // tijdelijk toegevoegd door maurin
        // zet visible naar false als health < 0
        visible = IsAlive;

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
            return;
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(Sprite, position,null, null, new Vector2(Sprite.Width/2,Sprite.Height/2), rotation);

        //draw Healthbar, above the enemy. The healthRatio sets the width of the healthbar to the width of the sprite.
        DrawingHelper.DrawRectangleFilled(new Rectangle((int)position.X - (int)(health * healthRatio)/2,(int) position.Y -Sprite.Height -10,(int)((float)health * healthRatio),10), spriteBatch, Color.Black);
    }
    public virtual Vector2 targetPosition()
    {
        //change this into the actual target position.

        //The position never truly equals the target position so 5 pixels lower or higher.

        if (target.X + GlobalPosition.X > (position).X - 5 && target.X + GlobalPosition.X < (position).X + 5 && target.Y + GlobalPosition.Y > (position).Y - 5 && target.Y + +GlobalPosition.Y < (position).Y + 5)
            //get a random target within 1000,1000
            target = new Vector2((int)(1000*Constant.RANDOM.NextDouble()), Constant.RANDOM.Next(1000));
        return target + GlobalPosition;
        

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


    //om de position te krijgen
    public Vector2 Position
    {
        get { return position; }
    }

}
