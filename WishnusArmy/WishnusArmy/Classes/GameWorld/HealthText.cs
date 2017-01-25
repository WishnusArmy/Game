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
    public string text;
    public int timer;
    public int maxTimer;
    public float p;
    public float impact;
    public Color startColor = Color.Red;
    public Color endColor = Color.Pink;
    protected int height;
    protected static SpriteFont font = FNT_HEALTH_INFO;

    public HealthText(string text, float impact) : base()
    {
        this.text = text;
        maxTimer = 50;
        timer = 0;
        height = 50;
    }

    public override void Update(object gameTime)
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
        //Shadow
        spriteBatch.DrawString(font, text.ToString(), GlobalPosition - new Vector2(0, p*p * height + 20) + new Vector2(3), Color.Black*(1-p*p), 0, 
            new Vector2(font.MeasureString(text.ToString()).X, font.MeasureString(text.ToString()).Y), 0.3f + 0.2f * impact + impact * (0.9f * impact)*p, SpriteEffects.None, 0);
        //Actual text
        spriteBatch.DrawString(font, text.ToString(), GlobalPosition - new Vector2(0, p * p * height + 20), new Color(((1f - p) * a.R)/255f + p * b.R/255f, (1f - p) * a.G/255f + p * b.G/255f, (1f - p) * a.B/255f + p * b.B/255f, 1f)*(1-p*p), 0,
            new Vector2(font.MeasureString(text.ToString()).X, font.MeasureString(text.ToString()).Y), 0.3f + 0.2f * impact + impact * (0.9f * impact) * p, SpriteEffects.None, 0);
    }
}
