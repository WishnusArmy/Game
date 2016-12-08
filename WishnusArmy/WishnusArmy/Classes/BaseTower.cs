using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static Constant;


public class Base : GameObject
{
    Vector2 GameMiddle = new Vector2(LEVEL_SIZE * NODE_SIZE / 2, LEVEL_SIZE * NODE_SIZE / 2);
    Vector2 BaseOrigin = new Vector2(SPR_BASEGUN.Width / 2, SPR_BASEGUN.Height / 2);
    Vector2 mouseposition = new Vector2();

    public override void HandleInput(InputHelper inputHelper)
    {
        mouseposition = inputHelper.MousePosition;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(SPR_BASE,(GameMiddle - BaseOrigin + GlobalPosition), Color.White);


        spriteBatch.Draw(SPR_BASEGUN, (GameMiddle - BaseOrigin + GlobalPosition), Color.White);


    }
}
