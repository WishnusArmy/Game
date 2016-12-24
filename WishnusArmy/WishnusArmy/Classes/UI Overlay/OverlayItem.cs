using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static DrawingHelper;

public class OverlayItem : GameObject
{
    Texture2D icon;
    string itemType;
    bool hover;


    public OverlayItem(string itemType) : base()
    {
        this.itemType = itemType;
        icon = SPR_LASER_TOWER;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MouseInRectangle(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, icon.Width, icon.Height)))
        {
            hover = true;
        }
        else
        {
            hover = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        if (hover)
        {
            //DrawRectangleFilled(new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, icon.Width, icon.Height), spriteBatch, Color.Black, 0.4f);
            spriteBatch.Draw(icon, GlobalPosition, null, Color.Black, 0f, new Vector2(icon.Width, icon.Height)*0.05f, 1.1f, SpriteEffects.None, 0);
        }
        spriteBatch.Draw(icon, GlobalPosition, Color.White);
    }
}
