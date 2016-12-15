﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

internal static class Constant
{
    /* THIS CLASS DEFINES ALL GLOBAL CONSTANTS FOR THE GAME.
     YOU CAN IMPLEMENT "using static Constant" to use them without a prefix.
     Please use all caps and lower bars (_) for new constants to make them easy to spot */

    //APPLLICATION
    internal static readonly Point WINDOW_SIZE = new Point(1920 / 2, 1080 / 2);
    internal static readonly Point SCREEN_SIZE = new Point(1920, 1080);
    internal static Random RANDOM = new Random();

    //LEVEL
    internal const int NODE_TEXTURE_SIZE = 64; //The raw, square size of a node
    internal static readonly Point NODE_SIZE  =  new Point(128, 64); //The size of a node in the grid
    internal const int LEVEL_SIZE = 50; //The size of the level grid

    //CAMERA
    internal const int SLIDE_BORDER = 100; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES
    internal const int LASER_TIME = 40; // Animation lenght of laser per target

    //LEVEL BUILDER
    internal static readonly Point TOOLBAR_SIZE = new Point(SCREEN_SIZE.X, 150);

    //BUTTON MARGIN
    internal static readonly Point BUTTON_MARGIN = new Point(20, 10);

    //STATS (edit it however you like ;)
    internal static int getTowerDamage(int level)
    {
        switch (level)
        {
            case 3: return 60;      // level 3
            case 2: return 45;      // level 2
            default: return 20;     // level 1
        }
    }
    internal static int getTowerFireRate(int level)
    {
        switch (level)
        {
            case 3: return 4;      // level 3
            case 2: return 3;      // level 2
            default: return 1;     // level 1
        }
    }

    internal static int getEnemyHealth(int level)
    {
        switch (level)
        {
            case 3: return 100;      // level 3
            case 2: return 60;      // level 2
            default: return 40;     // level 1
        }
    }


}
