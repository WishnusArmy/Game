﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ContentImporter.Textures;
using static ContentImporter.Sprites;

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
    internal static readonly Point OVERLAY_SIZE = new Point(300, 200);
    internal static readonly Point GAME_WINDOW_SIZE = SCREEN_SIZE - OVERLAY_SIZE;

    //LEVEL
    internal const int NODE_TEXTURE_SIZE = 64; //The raw, square size of a node
    internal static readonly Point NODE_SIZE  =  new Point(128, 64); //The size of a node in the grid
    internal static readonly Point LEVEL_SIZE = new Point(25, 50); //The size of the level grid
    internal static readonly Vector2 LEVEL_CENTER = new Vector2(LEVEL_SIZE.X * NODE_SIZE.X, LEVEL_SIZE.Y * NODE_SIZE.Y)/2;

    //CAMERA
    internal const int SLIDE_BORDER = 10; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES
    // damage/speed/radius per level
    internal static int[] BULLET_DAMAGE = new int[] { 60, 100, 120 };       
    internal static int[] BULLET_SPEED = new int[] { 5, 8, 10 };
    internal static int[] PULSE_DAMAGE = new int[] { 10, 30, 40 };
    internal static int[] PULSE_SPEED = new int[] { 4, 6, 8 };
    internal static int[] PULSE_RADIUS = new int[] { 200, 400, 600 };
    internal static int[] LASER_DAMAGE = new int[] { 1, 2, 4 };
    internal static int[] LASER_RADIUS = new int[] { 150, 400, 800 };
    internal const int LASER_TIME = 4;

    //TOWERS
    internal static int[] FIRE_RATE = new int[] { 1, 2, 3 };

    //ENEMIES
    internal static int[] ENEMY_HEALTH = new int[] { 100, 250, 600 };

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
        new ToolBarObjectsItem("ProjectileTower", SPR_ABSTRACT_TOWER)
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
