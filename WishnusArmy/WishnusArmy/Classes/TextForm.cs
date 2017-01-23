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
using Microsoft.Xna.Framework.Input;

public class TextForm : GameObject
{
    Rectangle pos;
    SpriteFont textFont;
    string text;
    Color boxColor;
    Color fontColor;

    List<Keys> keys;

    public TextForm(Rectangle position, SpriteFont textFont, string text, Color boxColor, Color fontColor, int layer = 0, string id = "")
    {
        this.pos = position;
        this.textFont = textFont;
        this.text = text;
        this.boxColor = boxColor;
        this.fontColor = fontColor;
        keys = new List<Keys>();
        Console.WriteLine("construct");
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (Keys k in inputHelper.CurrentPressedKeys())
        {
            keys.Add(k);
        }
    }

    public override void Update(GameTime gameTime)
    {
        Console.WriteLine("update form");
        /*
        foreach (Keys k in keys)
        {
            if (k == Keys.Enter)
            {
                GameStats.highScore.AddScore(text, GameStats.FinalScore);
                Kill = true;
                return;
            }
            
            text = text + k.ToString();
        }

        keys.Clear();
        Console.WriteLine(text);
        */
    }

    public override void Draw(GameTime gameTime)
    {

    }
}
