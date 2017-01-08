using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Fonts;
using static ContentImporter.Sprites;
using static Constant;

public class ButtonWithDelegate : Button
{
    public delegate void Clicked();
    public Clicked obj;

    static void EmptyMethod() { }

    public ButtonWithDelegate(string buttonText, Color bottonColor, Color hoverColor, SpriteFont buttonFont) : base(buttonText, bottonColor, hoverColor, buttonFont)
    {
        obj = new Clicked(EmptyMethod);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (pressed)
        {
            obj.Invoke();
        }
    }
}
