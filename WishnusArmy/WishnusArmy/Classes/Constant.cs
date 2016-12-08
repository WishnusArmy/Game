using System;
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
    internal const int NODE_SIZE = 64; //The size of a node in the grid
    internal const int LEVEL_SIZE = 50; //The size of the level grid

    //CAMERA
    internal const int SLIDE_BORDER = 100; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES
    internal const int LASER_TIME = 20; // Animation lenght of laser per target
}
