using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;
using static ContentImporter.Sprites;
using static Constant;

internal static class GameStats
{
    static int wave;


    // temp
    static Vector2 cameraPosition;
    static Vector2 cameraScale;
    public static List<Enemy> enemies;
    public static List<Tower> towers;

    static int totalEnemiesKilled;
    static int maxBaseHealth;
    static int baseHealth;

    public static void Initialize()
    {
        maxBaseHealth = 10000;
        baseHealth = 10000;
        wave = 0;
        totalEnemiesKilled = 0;

        cameraPosition = Vector2.Zero;
        cameraScale = Vector2.Zero;
        enemies = new List<Enemy>();
        towers = new List<Tower>();
    }

    internal static int TotalEnemiesKilled
    {
        get
        {
            return totalEnemiesKilled;
        }
        set
        {
            totalEnemiesKilled = value;
        }
    }

    internal static int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
        }
    }

    internal static int MaxBaseHealth
    {
        get
        {
            return maxBaseHealth;
        }
    }

    internal static int BaseHealth
    {
        get
        {
            return baseHealth;
        }
        set
        {
            baseHealth = value;
        }
    }

    internal static Vector2 CameraPosition
    {
        get
        {
            return cameraPosition;
        }
        set
        {
            cameraPosition = value;
        }
    }
    internal static Vector2 CameraScale
    {
        get
        {
            return cameraScale;
        }
        set
        {
            cameraScale = value;
        }
    }
}
