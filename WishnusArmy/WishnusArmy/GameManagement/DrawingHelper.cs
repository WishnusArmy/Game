using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class DrawingHelper
{
    protected static Texture2D pixel;

    public static void Initialize(GraphicsDevice graphics)
    {
        pixel = new Texture2D(graphics, 1, 1);
        pixel.SetData(new[] { Color.White });
    }

    public static void DrawRectangle(Rectangle r, SpriteBatch spriteBatch, Color col, int size = 1)
    {
        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, size, r.Height), col); // Left
        spriteBatch.Draw(pixel, new Rectangle(r.Right, r.Top, size, r.Height + size), col); // Right
        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, r.Width, size), col); // Top
        spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Bottom, r.Width + size, size), col); // Bottom
    }

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color c, int size = 1)
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
            c, //colour of the line
            angle,     //angle of line (calulated above)
            new Vector2(0, 0), // point in line about which to rotate
            SpriteEffects.None,
            0);
    }
}
