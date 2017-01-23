using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class EnemyLand : Enemy
{
    public EnemyLand(Type type, Texture2D sprite) : base(type, sprite)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Shadow
        //spriteBatch.Draw(sprite, GlobalPosition + new Vector2(15, -30), sheetRec, Color.Black * 0.4f, (float)Math.PI / 20, origin, new Vector2(1f, 1.15f), SpriteEffects.None, 0);
        base.Draw(gameTime, spriteBatch);
    }
}