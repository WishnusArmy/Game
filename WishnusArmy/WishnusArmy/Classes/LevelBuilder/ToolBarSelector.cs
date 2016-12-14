using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public class ToolbarSelector : GameObject
{
    bool _open = false;
    int width, targetWidth;

    bool open
    {
        get { return _open;  }
        set
        {
            _open = value;
            if (value)
                targetWidth = 400;
            else
                targetWidth = 50;
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
            width += Math.Abs(targetWidth - width) / (targetWidth - width) * 10;
        }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (inputHelper.MousePosition.X < 100)
        {
            open = true;
        }
        else
        {
            open = false;
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        DrawingHelper.DrawRectangleFilled(new Rectangle(0, 0, width, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y), spriteBatch, Color.White, 0.4f);
    }
}
