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

public class OverlayTowerItem : GameObject
{
    public Texture2D icon;
    public string itemType;
    public int cost, range;
    public string name;
    bool hover;

    public bool possible
    {
        get
        {
            return GameStats.EcResources >= cost;
        }
    }

    public OverlayTowerItem(string itemType, Vector2 pos = new Vector2()) : base()
    {
        this.itemType = itemType;
        icon = TOWER_INFO[itemType].icon;
        cost = TOWER_INFO[itemType].cost;
        name = TOWER_INFO[itemType].name;
        range = TOWER_INFO[itemType].range;

        Position = pos;
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
            Color[] tcolor = new Color[edge.Width * edge.Height]; //Make a Color[] container
            icon.GetData<Color>(tcolor);  //Get the pixel data from the icon texture
            for (int i = 0; i < tcolor.Length; ++i) //for a pixels
            {
                if (tcolor[i].A != 0) //if not opaque...
                {
                    tcolor[i] = Color.White; //Make the pixel white
                }
            }
            edge.SetData<Color>(tcolor); //Set the pixel data in the "edge" container
            //Draw the edge 10 percent larger than the icon
            spriteBatch.Draw(edge, GlobalPosition, null, Color.White * 0.6f, 0f, new Vector2(icon.Width, icon.Height) * 0.05f, 1.1f, SpriteEffects.None, 0);
        }
        spriteBatch.Draw(icon, GlobalPosition, Color.White);
    }

    public void DrawInfo(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Vector2 size = new Vector2(250, 400); //The size of the overlay rectangle
        Vector2 offset = new Vector2(SCREEN_SIZE.X - OVERLAY_SIZE.X - size.X, 0); //The position of the overlay rectangle
        DrawRectangleFilled(new Rectangle((int)offset.X, (int)offset.Y, (int)size.X, (int)size.Y), spriteBatch, Color.Black, 0.3f); //Draw the rectangle
        SpriteFont font = FNT_OVERLAY_INFO; //Reference a uniform font
        int lineHeight = (int)font.MeasureString("#").Y + 4; //Measure it to define line height
        DrawText(spriteBatch, font, name, offset + new Vector2(20, 20), Color.White); //Draw the name of the object
        DrawText(spriteBatch, font, "Cost: " + cost, offset + new Vector2(20, 20 + lineHeight), Color.White); //Draw the cost of the object
    }
}