﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;
using static ContentImporter.Sprites;
using static GameStats;

internal static class Constant
{
    /* THIS CLASS DEFINES ALL GLOBAL CONSTANTS FOR THE GAME.
     YOU CAN IMPLEMENT "using static Constant" to use them without a prefix.
     Please use all caps and lower bars (_) for new constants to make them easy to spot */

    //APPLICATION
    internal static readonly Point WINDOW_SIZE = new Point(1920 / 2, 1080 / 2);
    internal static readonly Point SCREEN_SIZE = new Point(1920, 1080);
    internal static Random RANDOM = new Random();

    //OVERLAY
    internal static readonly Point OVERLAY_SIZE = new Point(256, 180);
    internal static readonly Point GAME_WINDOW_SIZE = new Point(SCREEN_SIZE.X, SCREEN_SIZE.Y - OVERLAY_SIZE.Y);

    //LEVEL
    internal const int NODE_TEXTURE_SIZE = 64; //The raw, square size of a node
    internal static readonly Point IMAGE_NODE_SIZE = new Point(128, 64); //size of the original texture image
    internal static readonly Point NODE_SIZE = new Point(96, 48); //The size of a node in the grid
    internal static readonly Point LEVEL_SIZE = new Point(60, 120); //The size of the level grid
    internal static readonly Vector2 LEVEL_CENTER = new Vector2(LEVEL_SIZE.X * NODE_SIZE.X, LEVEL_SIZE.Y / 2 * NODE_SIZE.Y) / 2;

    //HIGHSCORE
    internal const int MAXSIZE_HIGHSCORELIST = 10;

    //CAMERA
    internal const int SLIDE_BORDER = 10; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES (Perhaps use a function here? Some kind of e-curve or root)
    // damage/speed/radius per level
    internal const int LASER_TIME = 4;
    internal const int BULLET_SPEED = 10;

    //ENEMYLIST
    internal static List<Enemy> allEnemies = new List<Enemy>();

    //TOWERS
    internal static int towerAmount = 0;
    private static double Efunction(double max, double slope)
    {
        return max / (1 + Math.Pow(Math.E, -slope));
    }

    // 0=Projectile Tower, 1=LaserTower, 2=PulseTower, 3=base
    internal static double TowerDamage(Tower.Type type, int[] stats)
    {
        int s = stats[0];
        switch (type)
        {
            case Tower.Type.RocketTower:
                return (int)(55 + 22 * s);
            case Tower.Type.LaserTower:
                return (int)(4 + 1.5 * s);
            case Tower.Type.PulseTower:
            	return (int)(75 + 35 * s);
            case Tower.Type.Base:
                return (int)(50 * Math.Pow(1.10, GameStats.Wave));
            case Tower.Type.ResourceTower:
                return (int)(40 + 10 * s);
            case Tower.Type.BombTower:
                return (int)(150 + 75 * s); //blastRadius scaled mee met damage.
            default:
                return 0;
        }
    }
    internal static int TowerRange(Tower.Type type, int[] stats)
    {
        int s = stats[1];
        switch (type)
        {
            case Tower.Type.RocketTower:
                return (int)Efunction(1200, 0.7 * s);
            case Tower.Type.LaserTower:
                return (int)Efunction(800, 0.6 * s);
            case Tower.Type.PulseTower:
                return (int)Efunction(900, 0.8 * s);
            case Tower.Type.BombTower:
                return (int)Efunction(2000, 0.8 * s);
            default:
                return 0;
        }
    }
    internal static int TowerRate(Tower.Type type, int[] stats)
    {
        int s = stats[2];
        switch (type)
        {
            case Tower.Type.RocketTower:
                return (int)(120 - 10 * s);
            case Tower.Type.LaserTower:
                return (int)(10 - 1.5 * s);
            case Tower.Type.PulseTower:
                return (int)(200 - 30 * s);
            case Tower.Type.Base:
                return (int)(200 * Math.Pow(0.93, GameStats.Wave));
            case Tower.Type.ResourceTower:
                return (800 - s * 100);
            case Tower.Type.BombTower:
                return (int) (300 - 30*s);
            default:
                return 0;
        }
    }
    internal static int UpgradeCost(Tower.Type type)
    {
        //multiplied by 1.5 for every successive level-up
        switch (type)
        {
            case Tower.Type.RocketTower:
                return 130;
            case Tower.Type.LaserTower:
                return 60;
            case Tower.Type.PulseTower:
                return 200;
            case Tower.Type.Base:
                return 250;
            case Tower.Type.ResourceTower:
                return 250;
            case Tower.Type.BombTower:
                return 400;
            default:
                return 0;
        }
    }

    public class TowerInfo
    {
        public string name;
        public int cost;
        public int range;
        public Texture2D sprite;
        public Texture2D icon;
    }

    public static readonly Dictionary<string, TowerInfo> TOWER_INFO = new Dictionary<string, TowerInfo>()
    {
        { "LaserTower", new TowerInfo() { name = "Laser Tower", cost = 100, sprite = SPR_LASER_TOWER, icon = SPR_LASER_ICON, range = TowerRange(Tower.Type.LaserTower, new int[] {0,0,0})} },
        { "RocketTower", new TowerInfo() { name = "Rocket Tower", cost = 200, sprite = SPR_ROCKET_TOWER, icon = SPR_ROCKETLAUNCHER_ICON, range = TowerRange(Tower.Type.RocketTower, new int[] {0,0,0}) } },
        { "PulseTower", new TowerInfo() { name = "PulseTower", cost = 600, sprite = SPR_PULSE_TOWER, icon = SPR_PULSE_ICON, range = TowerRange(Tower.Type.PulseTower, new int[] {0,0,0}) } },
        { "ResourceTower", new TowerInfo() { name = "ResourceTower", cost = 1000, sprite = SPR_MERCHANT_TOWER, icon = SPR_MERCHANT_ICON, range = TowerRange(Tower.Type.ResourceTower, new int[] {0,0,0}) } },
        { "BombTower", new TowerInfo() { name = "BombTower", cost = 2000, sprite = SPR_BOMB_TOWER, icon = SPR_BOMB_ICON, range = TowerRange(Tower.Type.BombTower, new int[] {0,0,0}) } },
    };


    //ENEMIES

    internal static int EnemyHealthFunction(double mod)
    {
        return (int)(mod * (100 * Math.Sqrt(GameStats.Wave)) + mod * (0.2 * GameStats.Wave * GameStats.Wave * GameStats.Wave));
    }

    /// <summary>
    /// 0=Tank, 1=soldier, 2=airballoon, 3=airplane
    /// </summary>
    internal static int EnemyHealth(Enemy.Type type)
    {
        //0=Tank, 1=soldier, 2=airballoon, 3=airplane
        switch (type)
        {
            case Enemy.Type.Tank:
                return EnemyHealthFunction(3.0); 
            case Enemy.Type.Soldier:
                return EnemyHealthFunction(0.6);
            case Enemy.Type.Helicopter:
                return EnemyHealthFunction(1.1);
            case Enemy.Type.Airplane:
                return EnemyHealthFunction(2.3);
            default:
                return EnemyHealthFunction(1);
        }

    }
    //reward for killing an enemy
    internal static int EnemyDamage(Enemy.Type type)
    {
        //0=Tank, 1=soldier, 2=airballoon, 3=airplane
        switch (type)
        {
            case Enemy.Type.Tank:
                return 30;
            case Enemy.Type.Soldier:
                return 10;
            case Enemy.Type.Helicopter:
                return 25;
            case Enemy.Type.Airplane:
                return 50;
            default:
                return 10;
        }
    }

    //LEVEL BUILDER
    internal static readonly Point TOOLBAR_SIZE = new Point(SCREEN_SIZE.X, 150);
    internal static readonly Point TOOLBAR_SELECTOR_SIZE = new Point(400, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y);
    internal static readonly Point TOOLBAR_ITEM_SIZE = new Point(TOOLBAR_SELECTOR_SIZE.X - 20, 100);
    internal const int TOOLBAR_ITEM_SPACING = 20;
    //LISTS

    //Textures that should show up in the LevelBuilder Toolbar Land
    internal static readonly List<Texture2D> LIST_TEXTURES = new List<Texture2D>
    {
        TEX_GRASS, //0
        TEX_GRASS_DIRT, //1
        TEX_MOUNTAIN_1, //2
        TEX_DIRT, //3
        TEX_WATER, //4
        TEX_FOREST, //5 
        TEX_AIR, //6
        TEX_MOUNTAIN_2, //7
        TEX_MOUNTAIN_3, //8
        TEX_FOREST_2 //9
    };

    internal static readonly List<ToolBarObjectsItem> LIST_OBJECTS = new List<ToolBarObjectsItem>
    {
        //The name string must be a valid type!
        new ToolBarObjectsItem("Enemy", SPR_ENEMY),
        new ToolBarObjectsItem("Base", SPR_BASE),
        new ToolBarObjectsItem("LaserTower", SPR_LASER_TOWER),
        new ToolBarObjectsItem("PulseTower", SPR_PULSE_TOWER),
        new ToolBarObjectsItem("ProjectileTower", SPR_ROCKET_TOWER),
        new ToolBarObjectsItem("BombTower", SPR_BOMB_TOWER)
    };

    internal static readonly List<string> LIST_ENEMIES = new List<string>
    {
         "Infantry",
         "Tank",
         "Helicopter",
         "Airplane"
    };

    //BUTTON MARGIN
    internal static readonly Point BUTTON_MARGIN = new Point(20, 10);

    //MATH
    internal static double DISTANCE(Vector2 v1, Vector2 v2)
    {
        Vector2 v3 = v1 - v2;
        return Math.Sqrt(v3.X * v3.X + v3.Y * v3.Y);
    }
}