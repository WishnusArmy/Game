using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class highScoreTable
{
    int[] ranks = new int[10];
    public string[] names = new string[10];
    int[] kills = new int[10];
    int[] waves = new int[10];
    int[] resources = new int[10];
    int[] scores = new int[10];

    public highScoreTable(int[] ranks, string[] names, int[] kills, int[] waves, int[] resources, int[] scores)
    {
        this.ranks = ranks;
        this.names = names;
        this.kills = kills;
        this.waves = waves;
        this.resources = resources;
        this.scores = scores;
    }
}

