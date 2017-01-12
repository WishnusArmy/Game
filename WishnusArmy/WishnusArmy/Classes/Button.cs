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

public class Button : GameObject
{
    protected bool pressed;
    protected bool hover;
    protected string buttonText;
    public Point padding;

    protected Color buttonColor;
    protected Color hoverColor;
    protected Rectangle buttonBox;
    protected SpriteFont buttonFont;

    public Button(string buttonText, Color buttonColor, Color hoverColor, SpriteFont buttonFont, int layer = 0, string id = "")
    {
        pressed = false;
        this.buttonText = buttonText;
        this.buttonColor = buttonColor;
        this.hoverColor = hoverColor;
        this.buttonFont = buttonFont;
        padding = BUTTON_MARGIN;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        pressed = inputHelper.MouseLeftButtonPressed() &&
            BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);

        //geluid voor alle buttons
        if (pressed)
        { PlaySound(ContentImporter.Sounds.SND_BUTTON_BASIC); }

        hover = BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        buttonBox = new Rectangle((int)GlobalPosition.X - padding.X - (int)buttonFont.MeasureString(buttonText).X/2, 
                                    (int)GlobalPosition.Y - padding.Y - (int)buttonFont.MeasureString(buttonText).Y/2, 
                                    (int)buttonFont.MeasureString(buttonText).X + 2 * padding.X, 
                                    (int)buttonFont.MeasureString(buttonText).Y + 2 * padding.Y);

        base.Draw(gameTime, spriteBatch);

        if (hover)
        {
            spriteBatch.Draw(SPR_WHITEPIXEL, buttonBox, hoverColor);
            spriteBatch.DrawString(buttonFont, buttonText, GlobalPosition - buttonFont.MeasureString(buttonText)/2, Color.Black);
        }
        else
        {
            spriteBatch.Draw(SPR_WHITEPIXEL, buttonBox, buttonColor);
            spriteBatch.DrawString(buttonFont, buttonText, GlobalPosition - buttonFont.MeasureString(buttonText)/2, Color.Black);
        }
    }

    public override void Reset()
    {
        base.Reset();
        pressed = false;
    }

    public bool Pressed
    {
        get { return pressed; }
    }

    public Vector2 Dimensions
    {
        get { return new Vector2(buttonFont.MeasureString(buttonText).X, buttonFont.MeasureString(buttonText).Y); }
    }

    public override Rectangle BoundingBox
    {
        get
        {
            return buttonBox;
        }
    }
}
