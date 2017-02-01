using System;
using static GameStats;
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
    private HandleHighscores hh;
    private int timer;
    Rectangle pos;
    SpriteFont textFont;
    string title;
    string text;
    Color boxColor;
    Color fontColor;
    string[] special;

    public TextForm(Rectangle position, SpriteFont textFont, string title, string text, Color boxColor, Color fontColor, int layer = 0, string id = "")
    {
        hh = new HandleHighscores();
        timer = -1;
        this.pos = position;
        this.textFont = textFont;
        this.text = text;
        this.title = title;
        this.boxColor = boxColor;
        this.fontColor = fontColor;
        this.special = new string[10] { ")", "!", "@", "#", "$", "%", "^", "&", "*", "(", };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        DrawRectangleFilled(pos, spriteBatch, boxColor, 0.8f);
        DrawRectangleFilled(new Rectangle(new Point(pos.Location.X + (int)(pos.Width * 0.05), pos.Location.Y + (int)(pos.Height * 0.4)), new Point((int)(pos.Width * 0.9), (int)(pos.Height * 0.5))), spriteBatch, Color.Black, 1f);
        DrawText(spriteBatch, textFont, title, pos.Location.ToVector2() + new Vector2(pos.Width / 2, 30), fontColor, true);
        DrawText(spriteBatch, textFont, text, pos.Location.ToVector2() + new Vector2(pos.Width / 2, pos.Size.Y - 50), fontColor, true);
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.CurrentPressedKeys().Length == 0)
        {
            timer = -1;
            return;
        }

        if (inputHelper.IsKeyDown(Keys.Back) && text.Length > 0)
        {
            timer++;
            if (timer == 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            if (timer > 44 && timer % 3 == 0)
            {
                text = text.Substring(0, text.Length - 1);
            }
            return;
        }
        
        // Enter the value
        if (inputHelper.KeyPressed(Keys.Enter))
        {
            hh.SubmitScore(text, Wave, TotalEnemiesKilled, totalResourcesGathered, FinalScore);
            Kill = true;
            Console.WriteLine("NEW ENTRY: " + text);
            return;
        }

        // SPACE
        if (inputHelper.KeyPressed(Keys.Space))
        {
            text = text + " ";
            return;
        }
        
        for (int i = 65; i < 97; i++)
        {
            Keys k = (Keys)i;
            if (inputHelper.KeyPressed(k))
            {
                AddChar(k, inputHelper);
                return;
            }
        }
        
        for (int i = 48; i < 58; i++)
        {
            Keys k = (Keys)i;
            if (inputHelper.KeyPressed(k))
            {
                AddNumber(i, inputHelper);
                return;
            }
        }
    }

    private void AddChar(Keys k, InputHelper inputHelper)
    {
        string input = k.ToString();
        if (!inputHelper.IsKeyDown(Keys.LeftShift))
            input = input.ToLower();
        text = text + input;
    }

    private void AddNumber(int i, InputHelper inputHelper)
    {
        int value = i - 48;
        if (!inputHelper.IsKeyDown(Keys.LeftShift))
            text = text + value;
        else
            text = text + special[value];
    }

    
}
