﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ObjectLists
{
    private static List<Enemy> enemies;
    private static List<Tower> towers;
    private static Camera camera;

    public static void Initialize()
    {
        enemies = new List<Enemy>();
        towers = new List<Tower>();
    }
    
    public static List<Enemy> Enemies
    {
        get { return enemies; }
    }

    public static List<Enemy> EnemiesCopy
    {
        get
        {
            List<Enemy> list = new List<Enemy>();
            foreach(Enemy e in Enemies)
            {
                list.Add(e);
            }
            return list;
        }
    }

    public static List<Tower> Towers
    {
        get  { return towers; }
    }

    public static Camera Camera
    {
        set
        {
            camera = value;
        }
    }

    public static Vector2 CameraPosition
    {
        get
        {
            return camera.Position;
        }
        set
        {
            camera.Position = value;
        }
    }

    public static Vector2 CameraScale
    {
        get
        {
            return camera.Scale;
        }
    }

    public static GridPlane CameraGridPlane
    {
        get
        {
            return camera.currentPlane;
        }
    }
}
