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
    public Vector2 gridPosition, pos, mousePosition;
    public Texture2D baseTexture;
    public Texture2D cannonTexture;
    float rotation;
    int range;
    int level = 1;
    int cost;

    public Tower()
    {
        baseTexture = Sprites.SPR_ABSTRACT_TOWER;
        cannonTexture = Sprites.SPR_ABSTRACT_CANNON;
    }
        
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        pos = gridPosition * NODE_SIZE + GlobalPosition;
        spriteBatch.Draw(baseTexture, pos);
        spriteBatch.Draw(cannonTexture, pos + new Vector2(baseTexture.Width/2, baseTexture.Height/2), null, null, new Vector2(cannonTexture.Width/2,cannonTexture.Height/2), rotation);
    }
    public override void HandleInput(InputHelper inputHelper)
    {
        double opposite = inputHelper.MousePosition.Y -cannonTexture.Width/2 - pos.Y;
        double adjacent = inputHelper.MousePosition.X -cannonTexture.Width/2 - pos.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float) Math.PI;
        mousePosition = inputHelper.MousePosition;
    }
    public Vector2 findNearestEnemy()
    {
        return mousePosition;
        //Change this into closest enemy position
    }

    public virtual void Attack()
    {

    }
    public virtual void Upgrade()
    {

    }
}
