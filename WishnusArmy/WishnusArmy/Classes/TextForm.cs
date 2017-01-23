using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static ContentImporter.Fonts;
using static ContentImporter.Sprites;
using static Constant;
using Microsoft.Xna.Framework.Input;

public class TextForm : GameObject
{
    private int timer;
    Rectangle pos;
    SpriteFont textFont;
    string title;
    string text;
    Color boxColor;
    Color fontColor;

    List<Keys> keys;

    public TextForm(Rectangle position, SpriteFont textFont, string title, string text, Color boxColor, Color fontColor, int layer = 0, string id = "")
    {
        timer = 100;
        this.pos = position;
        this.textFont = textFont;
        this.text = text;
        this.title = title;
        this.boxColor = boxColor;
        this.fontColor = fontColor;
        keys = new List<Keys>();
    }
    
    
    public override void HandleInput(InputHelper inputHelper)
    {
        timer++;
        if (timer < 8 || inputHelper.CurrentPressedKeys().Length == 0)
            return;
        timer = 0;
        foreach (Keys k in inputHelper.CurrentPressedKeys())
            keys.Add(k);
        
        if ((int)keys[0] > 64 && (int)keys[0] < 91 && text.Length < 17)
        {
            text = text + keys[0].ToString();
        }
        if (keys.Contains(Keys.Back) && text.Length != 0)
            text = text.Substring(0, text.Length - 1);

        if (keys.Contains(Keys.Enter))
        {
            GameStats.highScore.AddScore(text, GameStats.FinalScore);
            Kill = true;
            return;
        }
        keys.Clear();
    }
    
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawRectangleFilled(pos, spriteBatch, boxColor, 0.8f);
        DrawRectangleFilled(new Rectangle(new Point(pos.Location.X + (int)(pos.Width * 0.05), pos.Location.Y + (int)(pos.Height * 0.4)), new Point((int)(pos.Width*0.9), (int)(pos.Height * 0.5))), spriteBatch, Color.Black, 1f);
        DrawText(spriteBatch, textFont, title, pos.Location.ToVector2() + new Vector2(pos.Width/2, 30), fontColor, true);
        DrawText(spriteBatch, textFont, text, pos.Location.ToVector2() + new Vector2(pos.Width / 2, pos.Size.Y - 50), fontColor, true);
    }
}
