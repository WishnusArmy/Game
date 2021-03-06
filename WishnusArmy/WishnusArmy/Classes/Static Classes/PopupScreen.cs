﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;
using static ContentImporter.Fonts;
using static ContentImporter.Sprites;
using static DrawingHelper;
using static Functions;

public static class PopupScreen
{
    public static bool show = false;
    public static Point position;
    public static bool center;
    public static Color backgroundColor;
    public static Color edgeColor;
    public static Color buttonColor;
    public static Color buttonHoverColor;
    public static SpriteFont titleFont;
    public static SpriteFont buttonFont;
    public static Point size;
    public static int edgeSize;
    public static float alpha;
    public static List<ButtonWithDelegate> buttons;

    public static string title;

    public static void Initialize()
    {
        buttons = new List<ButtonWithDelegate>();
        position = new Point(SCREEN_SIZE.X / 2, SCREEN_SIZE.Y / 2);
        size = new Point(600, 800);
        center = true;
        alpha = 1f;
        buttonColor = Color.LightGreen;
        buttonHoverColor = Color.DarkGreen;
        backgroundColor = Color.Gray;
        edgeColor = Color.Black;
        buttonFont = FNT_MENU;
        edgeSize = 5;
        titleFont = FNT_OVERLAY;
        title = "PopUp";
    }

    public static void ClearButtons()
    {
        buttons.Clear();
    }

    public static void AddButton(string txt, ButtonWithDelegate.Clicked del)
    {
        buttons.Add(new ButtonWithDelegate(txt, buttonColor, buttonHoverColor, buttonFont) { obj = del + delegate { show = false; } });
    }

    public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //Plain popup background
        //DrawRectangleFilled(new Rectangle(position.X - (size.X/2)*center.ToInt(), position.Y - (size.Y/2)*center.ToInt(), size.X, size.Y), spriteBatch, backgroundColor);

        DrawRectangleFilled(new Rectangle(position.X - (size.X / 2) * center.ToInt() - edgeSize, position.Y - (size.Y / 2) * center.ToInt() - edgeSize, size.X + 2 * edgeSize, size.Y + 2 * edgeSize), spriteBatch, edgeColor);
        spriteBatch.Draw(SPR_POPUP, new Vector2(position.X - (size.X / 2) * center.ToInt(), position.Y - (size.Y / 2) * center.ToInt()), Color.White);

        int buttonHeight = (int)buttonFont.MeasureString("#").Y + 2*BUTTON_MARGIN.Y + 20;
        Vector2 offset = new Vector2(20, -200);

        spriteBatch.DrawString(FNT_GAMEOVER, "GAME PAUSED", position.toVector() - new Vector2(FNT_GAMEOVER.MeasureString("GAME PAUSED").X / 2, size.Y * 0.38f), Color.Black);
        for(int i=0; i < buttons.Count; ++i)
        {
            buttons[i].Position = position.toVector() + new Vector2(0, -size.Y/2 + size.Y * 0.38f) + new Vector2(0, buttonHeight * i);
            buttons[i].Draw(gameTime, spriteBatch);
        }
    }

    public static void HandleInput(InputHelper inputHelper)
    {
        foreach(Button but in buttons)
        {
            but.HandleInput(inputHelper);
        }
    }

    public static void Update(object gameTime)
    {
       // foreach(Button but in buttons)
       for(int i=0; i<buttons.Count; ++i)
        {
            buttons[i].Update(gameTime);
        }
    }
}