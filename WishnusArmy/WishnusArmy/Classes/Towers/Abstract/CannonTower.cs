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

public abstract class CannonTower : Tower
{
    protected float rotation;
    protected Vector2 targetPos;
    protected Texture2D cannonTexture;


    public CannonTower(Type type) : base(type)
    {
        cannonTexture = SPR_ABSTRACT_CANNON; // The moving part of a tower
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(cannonTexture, GlobalPosition, null, null, new Vector2(cannonTexture.Width / 2, cannonTexture.Height / 2), rotation);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        //calculate the rotation of the cannonbarrel
        if (target != null)
        {
            targetPos = target.Position;
        }
        double opposite = targetPos.Y - GlobalPosition.Y;
        double adjacent = targetPos.X - GlobalPosition.X;
        rotation = (float)Math.Atan2(opposite, adjacent) + 0.5f * (float)Math.PI;
    }
}
