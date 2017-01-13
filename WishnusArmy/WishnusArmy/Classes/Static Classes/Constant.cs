using System;
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
    internal static readonly Point OVERLAY_SIZE = new Point(256, 240);
    internal static readonly Point GAME_WINDOW_SIZE = SCREEN_SIZE - OVERLAY_SIZE;

    //LEVEL
    internal const int NODE_TEXTURE_SIZE = 64; //The raw, square size of a node
    internal static readonly Point IMAGE_NODE_SIZE = new Point(128, 64); //size of the original texture image
    internal static readonly Point NODE_SIZE  =  new Point(96, 48); //The size of a node in the grid
    internal static readonly Point LEVEL_SIZE = new Point(40, 100); //The size of the level grid
    internal static readonly Vector2 LEVEL_CENTER = new Vector2(LEVEL_SIZE.X * NODE_SIZE.X, LEVEL_SIZE.Y/2 * NODE_SIZE.Y)/2;

    //CAMERA
    internal const int SLIDE_BORDER = 10; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES (Perhaps use a function here? Some kind of e-curve or root)
    // damage/speed/radius per level
    internal const int LASER_TIME = 4;
    internal const int BULLET_SPEED = 10;

    //TOWERS
    private static double Efunction(double max, double slope)
    {
        return max / (1 + Math.Pow(Math.E, -slope));
    }
    
    // 0=Projectile Tower, 1=LaserTower, 2=PulseTower
    internal static double TowerDamage(Tower.Type type, int[] stats)
    {
        int s = stats[0];
        switch (type)
        {
            case Tower.Type.RocketTower:
                return Efunction(110, 0.6 * s);
            case Tower.Type.LaserTower:
                return Efunction(8, 0.7 * s);
            case Tower.Type.PulseTower:
                return Efunction(50, 0.5 * s);
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
                return (int)Efunction(700, 0.8 * s);
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
                return 40;// (s * s / -30) + (17 * s / 30) + 1;
            case Tower.Type.LaserTower:
                return (int)(10-1.5*s);
            case Tower.Type.PulseTower:
                return (int)Efunction(120, -0.6 * s);
            default:
                return 0;
        }
    }

    public class TowerInfo
    {
        public string name;
        public int cost;
        public Texture2D icon;
    }

    public static readonly Dictionary<string, TowerInfo> TOWER_INFO = new Dictionary<string, TowerInfo>()
    {
        { "LaserTower", new TowerInfo() { name = "Laser Tower", cost = 100, icon = SPR_LASER_TOWER } },
        { "RocketTower", new TowerInfo() { name = "Rocket Tower", cost = 250, icon = SPR_ROCKET_TOWER } },
        { "PulseTower", new TowerInfo() { name = "PulseTower", cost = 300, icon = SPR_PULSE_TOWER } }
    };
    

    //ENEMIES
    internal static int EnemyHealthFunction(double mod)
    {
        return (int)mod * (18 * Wave + 100);
    }
    internal static int EnemyHealth(int type)
    {
        //0=Tank, 1=soldier, 2=airballoon, 3=airplane
        switch (type)
        {
            case 0:
                return EnemyHealthFunction(1.2);
            case 1:
                return EnemyHealthFunction(0.6);
            case 2:
                return EnemyHealthFunction(1.5);
            case 3:
                return EnemyHealthFunction(1.8);
            default:
                return EnemyHealthFunction(1);
        }
        
    }

    //LEVEL BUILDER
    internal static readonly Point TOOLBAR_SIZE = new Point(SCREEN_SIZE.X, 150);
    internal static readonly Point TOOLBAR_SELECTOR_SIZE = new Point(400, SCREEN_SIZE.Y - TOOLBAR_SIZE.Y);
    internal static readonly Point TOOLBAR_ITEM_SIZE = new Point(TOOLBAR_SELECTOR_SIZE.X - 20, 100);
    internal const int TOOLBAR_ITEM_SPACING = 20;
    //LISTS

    //Textures that should show up in the LevelBuilder Toolbar Land
    internal static readonly List<Texture2D> LIST_LAND_TEXTURES = new List<Texture2D>
    {
        TEX_GRASS,
        TEX_GRASS_DIRT,
        TEX_STONE_ROAD,
        TEX_DIRT,
        TEX_WATER,
        TEX_FOREST
    };

    internal static readonly List<ToolBarObjectsItem> LIST_OBJECTS = new List<ToolBarObjectsItem>
    {
        //The name string must be a valid type!
        new ToolBarObjectsItem("Enemy", SPR_ENEMY),
        new ToolBarObjectsItem("Base", SPR_BASE),
        new ToolBarObjectsItem("LaserTower", SPR_LASER_TOWER),
        new ToolBarObjectsItem("PulseTower", SPR_PULSE_TOWER),
        new ToolBarObjectsItem("ProjectileTower", SPR_ROCKET_TOWER)
    };

    //BUTTON MARGIN
    internal static readonly Point BUTTON_MARGIN = new Point(20, 10);

    //MATH
    internal static double DISTANCE(Vector2 v1, Vector2 v2)
    {
        Vector2 v3 = v1 - v2;
        return Math.Sqrt(v3.X*v3.X + v3.Y*v3.Y);
    }
}
