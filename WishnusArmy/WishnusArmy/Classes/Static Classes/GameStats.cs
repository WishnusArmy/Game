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
    static int totalEnemiesKilled;

    public static void Initialize()
    {
        wave = 0;
        totalEnemiesKilled = 0;
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
}
