using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DrawingHelper
{
    protected static Texture2D pixel;
    public static GraphicsDevice Graphics;

    public static void Initialize(GraphicsDevice graphics)
    {
        pixel = new Texture2D(graphics, 1, 1);
        pixel.SetData(new[] { Color.White });
        Graphics = graphics;
    }

    public static void DrawRectangle(Rectangle r, SpriteBatch spriteBatch, Color col, int size, float alpha = 1f)
    {
        spriteBatch.Draw(pixel, new Rectangle(r.Left - size/2, r.Top, size, r.Height +size), col * alpha); // Left
        spriteBatch.Draw(pixel, new Rectangle(r.Right - size/2, r.Top, size, r.Height + size), col* alpha); // Right
        spriteBatch.Draw(pixel, new Rectangle(r.Left - size/2, r.Top - size/2, r.Width + size, size), col * alpha); // Top
        spriteBatch.Draw(pixel, new Rectangle(r.Left - size/2, r.Bottom - size/2, r.Width + size, size), col * alpha); // Bottom
    }

    public static void DrawRectangleFilled(Rectangle r, SpriteBatch spriteBatch, Color col, float alpha = 1f)
    {
        spriteBatch.Draw(pixel, r, col * alpha);
    }

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color c, int size, float alpha = 1f)
    {
        Vector2 edge = end - start;
        // calculate angle to rotate line
        float angle =
            (float)Math.Atan2(edge.Y, edge.X);


        spriteBatch.Draw(pixel,
            new Rectangle(// rectangle defines shape of line and position of start of line
                (int)start.X,
                (int)start.Y,
                (int)edge.Length(), //the SpriteBatch will strech the texture to fill this rectangle
                size), //width of line, change this to make thicker line
            null,
            c * alpha, //colour of the line multiplied with the alpha
            angle,     //angle of line (calulated above)
            new Vector2(0, 0), // point in line about which to rotate
            SpriteEffects.None,
            0);
    }

    public static void DrawText(SpriteBatch spriteBatch, SpriteFont font, string str, Vector2 pos, Color col, bool center = false, float alpha = 1f)
    {
        if (center)
            pos -= new Vector2(font.MeasureString(str).X, font.MeasureString(str).Y) / 2;

        spriteBatch.DrawString(font, str, pos, col * alpha);
    }
}
