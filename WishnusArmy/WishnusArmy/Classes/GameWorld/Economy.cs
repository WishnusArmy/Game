using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;

public static class Economy
{
    //Let all member variables have the EcPrefix so
    //the origin of the variable is clear
    public static int EcResources;
    
    public static void Initialize()
    {
        EcResources = 5000;
    }
}