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
    internal const int NODE_TEXTURE_SIZE = 64; //The raw, square size of a node
    internal const int NODE_SIZE = 90; //The size of a node in the grid
    internal const int LEVEL_SIZE = 50; //The size of the level grid

    //CAMERA
    internal const int SLIDE_BORDER = 100; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.

    //PROJECTILES
    internal const int LASER_TIME = 3; // Animation lenght of laser per target
            // damage/speed/radius per level
    internal static int[] BULLET_DAMAGE = new int[] { 60, 100, 120 };       
    internal static int[] BULLET_SPEED = new int[] { 5, 8, 10 };
    internal static int[] PULSE_DAMAGE = new int[] { 10, 30, 40 };
    internal static int[] PULSE_SPEED = new int[] { 4, 6, 8 };
    internal static int[] PULSE_RADIUS = new int[] { 200, 400, 600 };

    //TOWERS
    internal static int[] FIRE_RATE = new int[] { 1, 2, 3 };

    //ENEMIES
    internal static int[] ENEMY_HEALTH = new int[] { 100, 250, 600 };

    //LEVEL BUILDER
    internal static readonly Point TOOLBAR_SIZE = new Point(SCREEN_SIZE.X, 150);

    //BUTTON MARGIN
    internal static readonly Point BUTTON_MARGIN = new Point(20, 10);
    
    
    
    

}
