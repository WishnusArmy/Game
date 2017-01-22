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
                double type_a = Math.Sqrt(Math.Min(LIST_ENEMIES.Count, (int)(GameStats.Wave / 0.3)));
                double type_b = Math.Sqrt(Math.Min(LIST_ENEMIES.Count, (int)(GameStats.Wave / 0.3)));
                string eType = LIST_ENEMIES[RANDOM.Next((int)(type_a*type_b))];
                Type t = Type.GetType(eType); //Get the type of the object
                object temp = Activator.CreateInstance(t); //Create an instance of that object
                Enemy obj = temp as Enemy; //Cast it as an Enemy
                GridNode node = available[RANDOM.Next(available.Count)];

                obj.startNode = node;
                obj.Position = node.Position;
                plane.Add(obj);
                resources -= obj.cost;
            }

            bool AllDeath = true;
            List<Enemy> list = plane.FindByType<Enemy>();
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

                resources = (int)(500 + 2*Math.Pow(x, (Math.Sqrt(x / 100) + 1)) + 400 * (float)Math.Sqrt(x));
            }
        }
    }
}
