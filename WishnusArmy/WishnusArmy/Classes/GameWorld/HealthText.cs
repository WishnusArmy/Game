using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static DrawingHelper;
using static ContentImporter.Fonts;


public class HealthText : DrawOnTop
{
    public int text;
    public int timer;
    public int maxTimer;
    public float p;
    public float impact;
    Color startColor;
    Color endColor;
    int height;
    SpriteFont font;

    public HealthText(int text, float impact) : base()
    {
        this.text = text;
        maxTimer = 50;
        timer = 0;
        startColor = Color.Black;
        endColor = Color.Black;
        height = 50;
        font = FNT_HEALTH_INFO;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (timer < maxTimer)
            timer++;
        else
            Kill = true;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Color a = startColor;
        Color b = endColor;
        p = (float)timer / maxTimer;
        base.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(font, text.ToString(), GlobalPosition - new Vector2(0, p*p * height), new Color(((1 - p) * a.R) - p * b.R, (1 - p) * a.G - p * b.G, (1 - p) * a.B - p * b.B, 255)*(1 - p * p), 0, 
            new Vector2(font.MeasureString(text.ToString()).X, font.MeasureString(text.ToString()).Y), 0.3f + 0.2f * impact + impact * (0.9f * impact)*p, SpriteEffects.None, 0);
    }
}
