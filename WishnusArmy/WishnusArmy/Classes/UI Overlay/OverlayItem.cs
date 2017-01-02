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
using static Economy;

public class OverlayItem : GameObject
{
    public Texture2D icon;
    public string itemType;
    public int cost;
    bool hover;

    public bool possible
    {
        get
        {
            return EcResources >= cost;
        }
    }


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

        if (inputHelper.MouseLeftButtonPressed() && hover && possible)
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
            DrawInfo(spriteBatch, gameTime); //Draw the info of the tower
            Texture2D edge = new Texture2D(Graphics, icon.Width, icon.Height);  //Make a new Texture2D container
            Color[] tcolor = new Color[edge.Width*edge.Height]; //Make a Color[] container
            icon.GetData<Color>(tcolor);  //Get the pixel data from the icon texture
            for(int i=0; i<tcolor.Length; ++i) //for a pixels
            {
                if (tcolor[i].A != 0) //if not opaque...
                {
                    tcolor[i] = Color.White; //Make the pixel white
                }
            }
            edge.SetData<Color>(tcolor); //Set the pixel data in the "edge" container
            //Draw the edge 10 percent larget than the icon
            spriteBatch.Draw(edge, GlobalPosition, null, Color.White * 0.6f, 0f, new Vector2(icon.Width, icon.Height)*0.05f, 1.1f, SpriteEffects.None, 0);
        }
        spriteBatch.Draw(icon, GlobalPosition, Color.White);
    }

    public void DrawInfo(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Vector2 offset = new Vector2(SCREEN_SIZE.X - OVERLAY_SIZE.X, SCREEN_SIZE.Y - 400);
        SpriteFont font = FNT_OVERLAY_INFO;
        int lineHeight = (int)font.MeasureString("#").Y + 4;
        //DrawRectangleFilled(new Rectangle(offset, new Point(SCREEN_SIZE.X - offset.X, SCREEN_SIZE.Y - offset.Y)), spriteBatch, Color.White, 0.4f);
        DrawText(spriteBatch, font, itemType, offset + new Vector2(20, 20), Color.White);
        DrawText(spriteBatch, font, "cost: " + cost, offset + new Vector2(20, 20 + lineHeight), Color.White);
    }
}
