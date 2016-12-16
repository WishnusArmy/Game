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

public class ToolbarSelector : GameObject
{
    bool _open = false, reallyOpen;
    double width, targetWidth;

    bool open
    {
        get { return _open;  }
        set
        {
            _open = value;
            if (value)
            {
                targetWidth = 400;
                reallyOpen = false;
            }
            else
            {
                targetWidth = 0;
                reallyOpen = false;
            }
        }
    }
    
    static string[] toolbars = new string[]
    {
        "Floor Textures"
    };


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (width != targetWidth)
        {
            width += Math.Abs(targetWidth - width) / (targetWidth - width) * (400/12);
            if (Math.Abs(targetWidth - width) <= (400 / 12))
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
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, 0, (int)width, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y), spriteBatch, Color.White, 0.4f);
        if (reallyOpen)
        {
            for(int i=0; i<TOOLBAR_LIST.Count; ++i)
            {
                spriteBatch.Draw(TOOLBAR_LIST[i][0], new Vector2(GlobalPosition.X + 190, GlobalPosition.Y + 100 + 100 * i), Color.White);
            }
        }
    }
}
