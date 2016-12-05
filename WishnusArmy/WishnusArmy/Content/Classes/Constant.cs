using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Constant
{
    /* THIS CLASS DEFINES ALL GLOBAL CONSTANTS FOR THE GAME.
     YOU CAN IMPLEMENT "using static Constant" to use them without a prefix.
     Please use all caps and lower bars (_) for new constants to make them easy to spot */

    //LEVEL
    internal const int NODE_SIZE = 128; //The size of a node in the grid
    internal const int LEVEL_SIZE = 25; //The size of the level grid

    //CAMERA
    internal const int SLIDE_BORDER = 100; //Defines the width of the edge that will respond to the mouse.
    internal const int SLIDE_SPEED = 10; //The speed at which the window slides.
}
