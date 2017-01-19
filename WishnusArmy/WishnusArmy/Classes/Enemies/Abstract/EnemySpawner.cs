using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Constant;

public class EnemySpawner : GameObjectList
{
    GridPlane plane;
    List<GridNode> available = new List<GridNode>();
    public EnemySpawner(GridPlane plane)
    {
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
            if (RANDOM.Next(50) == 0)
            {
                GridNode node = available[RANDOM.Next(available.Count)];
                Add(new Tank() { startNode = node, Position = node.Position });
            }
        }
    }
}
