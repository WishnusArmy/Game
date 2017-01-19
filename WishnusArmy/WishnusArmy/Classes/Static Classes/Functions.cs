using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Constant;

public static class Functions
{
    static GraphicsDevice graphicsDevice;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        Functions.graphicsDevice = graphicsDevice;
    }

    public static int ToInt(this bool b)
    {
        if (!b)
            return 0;
        else
            return 1;
    }

    public static Vector2 toVector(this Point p)
    {
        return new Vector2(p.X, p.Y);
    }

    public static Point toPoint(this Vector2 v)
    {
        return new Point((int)v.X, (int)v.Y);
    }

    public static Texture2D CreateCircle(int radius)
    {
        int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
        Texture2D texture = new Texture2D(graphicsDevice, outerRadius, outerRadius);

        Color[] data = new Color[outerRadius * outerRadius];

        // Colour the entire texture transparent first.
        for (int i = 0; i < data.Length; i++)
            data[i] = new Color(0, 0, 0, 0);

        // Work out the minimum step necessary using trigonometry + sine approximation.
        double angleStep = 1f / radius;

        for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
        {
            int x = (int)Math.Round(radius + radius * Math.Cos(angle));
            int y = (int)Math.Round(radius + radius * Math.Sin(angle));

            data[y * outerRadius + x + 1] = Color.White;
        }

        texture.SetData(data);
        return texture;
    }

    public static Vector2 getOrigin(this Texture2D tex)
    {
        return new Vector2(tex.Width, tex.Height) / 2;
    }

    public static int choose(List<int> list)
    {
        return list[RANDOM.Next(list.Count)];
    }

    public static bool onList(this List<GridNode> list, GridNode node)
    {
        bool flag = false;
        for (int i = 0; i < list.Count; ++i)
        {
            if (node == list[i])
                flag = true;
        }
        return flag;
    }
}
