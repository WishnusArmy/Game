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
    public static int Wave;
    public static bool InWave;
    public static int WaveTimer;
    public static int TotalEnemiesKilled;
    public static int MaxBaseHealth;
    public static int BaseHealth;

    public static void Initialize()
    {
        MaxBaseHealth = 1000;
        BaseHealth = 1000;
        Wave = 0;
        InWave = true;
        WaveTimer = 0;
        TotalEnemiesKilled = 0;
    }
}
