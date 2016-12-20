using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Constant;
using static ContentImporter.Fonts;

public class ToolBarSelector : GameObject
{
    bool _open = false, reallyOpen;
    double width, targetWidth;
    List<ToolBarSelectorItem> items;
    int selected;

    //pointers to the toolbars
    ToolBarTextures tbTextures;
    ToolBarObjects tbObjects;

    public ToolBarSelector(ToolBarTextures tbTextures, ToolBarObjects tbObjects) : base()
    {
        this.tbTextures = tbTextures;
        this.tbObjects = tbObjects;
        selected = -1;
        items = new List<ToolBarSelectorItem>
        {
            new ToolBarSelectorItem("Land Textures", LIST_LAND_TEXTURES),
            new ToolBarSelectorItem("Game Objects", LIST_OBJECTS)
        };
    }

    bool open
    {
        get { return _open;  }
        set
        {
            _open = value;
            if (value)
            {
                targetWidth = TOOLBAR_SELECTOR_SIZE.X;
                reallyOpen = false;
            }
            else
            {
                targetWidth = 0;
                reallyOpen = false;
            }
        }
    }
    

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (width != targetWidth)
        {
            width += Math.Abs(targetWidth - width) / (targetWidth - width) * (TOOLBAR_SELECTOR_SIZE.X/12);
            if (Math.Abs(targetWidth - width) <= (TOOLBAR_SELECTOR_SIZE.X / 12))
            {
                width = targetWidth;
                if (open)
                    reallyOpen = true;
            }
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.KeyPressed(Keys.T))
        {
            open = !open;
        }

        if (reallyOpen)
        {
            Vector2 mousePos = inputHelper.MousePosition;
            selected = -1;
            for (int i = 0; i < items.Count; ++i)
            {
                if (mousePos.X >= 10 && mousePos.X < 10 + TOOLBAR_ITEM_SIZE.X)
                {
                    if (mousePos.Y >= 100 + (TOOLBAR_ITEM_SIZE.Y + TOOLBAR_ITEM_SPACING) * i && mousePos.Y < 100 + (TOOLBAR_ITEM_SIZE.Y + TOOLBAR_ITEM_SPACING) * i + TOOLBAR_ITEM_SIZE.Y)
                    {
                        selected = i;
                    }
                }
            }

            if (inputHelper.MouseLeftButtonPressed())
            {
                if (selected != -1)
                {
                    switch(items[selected].type)
                    {
                        case ToolBarSelectorItem.ItemType.Textures:
                            tbTextures.active = true;
                            tbObjects.active = false;
                            tbTextures.toolList = items[selected].tex_list;
                            break;

                        case ToolBarSelectorItem.ItemType.Object:
                            tbTextures.active = false;
                            tbObjects.active = true;
                            tbObjects.toolList = items[selected].obj_list;
                            break;
                    }
                }
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0,0,(int)width, TOOLBAR_SELECTOR_SIZE.Y), spriteBatch, Color.White, 0.4f);
        if (reallyOpen)
        {
            for (int i = 0; i < items.Count; ++i)
            {
                Color col = Color.White;
                if (i == selected)
                {
                    col = Color.YellowGreen;
                }
                int mult = TOOLBAR_ITEM_SIZE.Y + TOOLBAR_ITEM_SPACING;
                DrawingHelper.DrawRectangleFilled(new Rectangle(10, 100 + mult * i, TOOLBAR_SELECTOR_SIZE.X - 20, TOOLBAR_ITEM_SIZE.Y), spriteBatch, col);
                DrawingHelper.DrawRectangle(new Rectangle(10, 100 + mult * i, TOOLBAR_SELECTOR_SIZE.X-20, TOOLBAR_ITEM_SIZE.Y), spriteBatch, Color.Black, 3);
                DrawingHelper.DrawText(spriteBatch, FNT_LEVEL_BUILDER, items[i].name, new Vector2(45, 140 + mult * i), Color.Black);
            }
        }
    }
}
