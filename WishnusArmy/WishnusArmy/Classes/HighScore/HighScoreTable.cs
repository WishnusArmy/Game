using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class highScoreTable
{
    int[] ranks = new int[10];
    string[] names = new string[10];
    int[] kills = new int[10];
    int[] waves = new int[10];
    int[] resources = new int[10];
    int[] scores = new int[10];

    public highScoreTable(int[] ranks, string[] names, int[] waves, int[] kills, int[] resources, int[] scores)
    {
        this.ranks = ranks;
        this.names = names;
        this.waves = waves;
        this.kills = kills;
        this.resources = resources;
        this.scores = scores;
    }

    #region Highscore Properties
    public int[] Ranks
    {
        get { return ranks; }
    }

    public string[] Names
    {
        get { return names; }
    }

    public int[] Kills
    {
        get { return kills; }
    }

    public int[] Waves
    {
        get { return waves; }
    }

    public int[] Resources
    {
        get { return resources; }
    }

    public int[] Scores
    {
        get { return scores; }
    }
    #endregion
}

