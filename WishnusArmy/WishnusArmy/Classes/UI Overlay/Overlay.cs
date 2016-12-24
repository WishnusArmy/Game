using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public class Overlay : GameObjectList
{
    string selected;
    Vector2 mousePos;

    public Overlay() : base()
    {
        selected = null;
        mousePos = Vector2.Zero;
        OverlayItem item = new OverlayItem("");
        item.Position = new Vector2(SCREEN_SIZE.X - 100, 100);
        Add(item);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        mousePos = inputHelper.MousePosition;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, 0), new Point(OVERLAY_SIZE.X, SCREEN_SIZE.Y)), spriteBatch, Color.SteelBlue, 0.6f);
        DrawingHelper.DrawRectangleFilled(new Rectangle(new Point(0, SCREEN_SIZE.Y - OVERLAY_SIZE.Y), new Point(SCREEN_SIZE.X - OVERLAY_SIZE.X, OVERLAY_SIZE.Y)), spriteBatch, Color.SteelBlue, 0.6f);
        base.Draw(gameTime, spriteBatch);
    }
}
