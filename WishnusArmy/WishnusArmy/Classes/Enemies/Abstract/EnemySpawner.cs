using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;

public class EnemySpawner : GameObjectList
{
    GridPlane plane;
    List<GridNode> available = new List<GridNode>();
    int resources;

    public EnemySpawner(GridPlane plane)
    {
        resources = 0;
        this.plane = plane;
        for (int x = 0; x < LEVEL_SIZE.X; ++x)
        {
            if (!plane.grid[x, 0].solid)
                available.Add(plane.grid[x, 0]);
            if (!plane.grid[x, LEVEL_SIZE.Y - 1].solid)
                available.Add(plane.grid[x, LEVEL_SIZE.Y - 1]);
        }

        for(int y=0; y<LEVEL_SIZE.Y; ++y)
        {
            if (!plane.grid[0, y].solid)
                available.Add(plane.grid[0, y]);
            if (!plane.grid[LEVEL_SIZE.X - 1, y].solid)
                available.Add(plane.grid[LEVEL_SIZE.X - 1, y]);
        }
    }

    public override GridPlane MyPlane
    {
        get
        {
            return plane;
        }
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        if (GameStats.InWave)
        {
            if (RANDOM.Next(50) == 0 && resources > 0)
            {
                resources -= 100;
                GridNode node = available[RANDOM.Next(available.Count)];
                Add(new Tank() { startNode = node, Position = node.Position });
            }

            bool AllDeath = true;
            List<Enemy> list = FindByType<Enemy>();
            foreach(Enemy e in list)
            {
                if (!e.kill)
                {
                    AllDeath = false;
                    break;
                }
                    
            }
            if (AllDeath && GameStats.InWave && resources <= 0)
            {
                GameStats.InWave = false;
                GameStats.WaveTimer = 5 * 60;
            }
        }

        if (!GameStats.InWave && GameStats.WaveTimer > 0)
        {
            GameStats.WaveTimer--;
            if (GameStats.WaveTimer <= 0)
            {
                GameStats.InWave = true;
                int x = GameStats.Wave++;

                resources = (int)(500 + Math.Pow(x, (Math.Sqrt(x / 100) + 1)) + 400 * (float)Math.Sqrt(x));
            }
        }
    }
}
