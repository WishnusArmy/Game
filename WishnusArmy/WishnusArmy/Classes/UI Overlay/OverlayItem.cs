using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Sprites;
using static ContentImporter.Fonts;
using static DrawingHelper;
using static Constant;

public class OverlayItem : GameObject
{
    public Texture2D icon;
    public string itemType;
    public int cost;
    bool hover;


    public OverlayItem(string itemType) : base()
    {
        this.itemType = itemType;
        icon = SPR_LASER_TOWER;
        cost = Towers[itemType].Cost;
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

        if (inputHelper.MouseLeftButtonPressed() && hover)
        {
            Overlay p = parent as Overlay;
            p.selected = this;
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
        DrawText(spriteBatch, FNT_LEVEL_BUILDER, cost.ToString(), GlobalPosition, Color.Black, false);
    }
}
