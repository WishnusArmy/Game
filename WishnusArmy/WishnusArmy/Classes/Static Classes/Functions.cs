using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


public static class Functions
{
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
}
