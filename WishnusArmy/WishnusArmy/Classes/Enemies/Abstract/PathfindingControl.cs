using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;

//This object handles pathfinding like an operating system handles processes.
public class PathfindingControl : GameObject
{
    public static int threadCount;
    public static Thread thread;
    List<Enemy> pendingRequests;

    public PathfindingControl() : base()
    {
        pendingRequests = new List<Enemy>();
        threadCount = 0;
        thread = new Thread(new ThreadStart(delegate { }));
    }

    public void AddRequest(Enemy e)
    {
        if (!pendingRequests.Contains(e))
        {
            pendingRequests.Add(e);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (pendingRequests.Count > 1000)
            throw new Exception("Pathfinding Request Overflow!");
        if (!thread.IsAlive && pendingRequests.Count > 0)
        {
            Enemy e = pendingRequests[0];
            if (e.path != null && e.pathIndex < e.path.Count)
                e.startNode = e.path[e.pathIndex];
            thread = new Thread(new ThreadStart(e.getPath));
            thread.Start();
            e.wait = false;
            e.waitAt = null;
            pendingRequests.RemoveAt(0);
            Console.WriteLine("Request Granted: " + (pendingRequests.Count) + " to go");
        }
        base.Update(gameTime);
    }
}
