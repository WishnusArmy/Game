using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter;

public class Tower : GameObject
{
    protected MouseState state = new MouseState();
    public Vector2 gridPosition, pos, mousePosition, previousPosition = new Vector2(0, 0);
    public Texture2D baseTexture;
    public Texture2D cannonTexture;
    float rotation;
    protected int range = 5 * Constant.NODE_SIZE;
    int level = 1;
    int cost;

    public Tower()
    {
        baseTexture = Sprites.SPR_ABSTRACT_TOWER; //Texture of the (unanimated) base of the tower
        cannonTexture = Sprites.SPR_ABSTRACT_CANNON; // The moving part of a tower
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        pos = gridPosition * NODE_SIZE + GlobalPosition;
        spriteBatch.Draw(baseTexture, pos);
        spriteBatch.Draw(cannonTexture, pos + new Vector2(baseTexture.Width / 2, baseTexture.Height / 2), null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        //calculate the rotation of the cannonbarrel
        double opposite = findTarget().Y - cannonTexture.Width / 2 - pos.Y;
        double adjacent = findTarget().X - cannonTexture.Width / 2 - pos.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;

        mousePosition = inputHelper.MousePosition;
    }

    public virtual Vector2 findTarget()
    {
        //Calculate the distance of the target
        double A = mousePosition.Y - pos.Y;
        double B = mousePosition.X - pos.X;
        double distance = Math.Sqrt(A * A + B * B);

        // if target is withing the tower range
        if (distance <= range)
        {
            previousPosition = mousePosition;
            return mousePosition;
            //Change this into closest enemy position
        }
        else
            return previousPosition;

    }

    public virtual void Attack()
    {

    }

    public virtual void Upgrade()
    {

    }
}
