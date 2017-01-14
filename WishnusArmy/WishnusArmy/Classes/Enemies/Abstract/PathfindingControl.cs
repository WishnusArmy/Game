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
    static Thread thread;
    static List<Enemy> pendingRequests;

    public PathfindingControl() : base()
    {
        pendingRequests = new List<Enemy>();
        threadCount = 0;
        thread = new Thread(new ThreadStart(delegate { }));
    }

    public static void AddRequest(Enemy e)
    {
        if (!pendingRequests.Contains(e))
        {
            if (pendingRequests.Count < 50)
            {
                pendingRequests.Add(e);
            }
            else
            {
                Console.WriteLine("Request Denied: Queue full! FLUSHING ALL REQUESTS...");
                pendingRequests.Clear();
            }
        }
        else Console.WriteLine("Request Denied: Enemy already has a pending request");
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);

        //pendingRequests = pendingRequests.OrderBy(o => o.pathIndex).ToList();
        if (!thread.IsAlive && pendingRequests.Count > 0)
        {
            Enemy e = pendingRequests[0];
            if (e.kill)
            {
                pendingRequests.Remove(e);
                Console.WriteLine("Request Denied: Enemy already killed");
            }
            else
            {
                if (e.path != null && e.pathIndex < e.path.Count)
                    e.startNode = e.path[e.pathIndex];
                thread = new Thread(new ThreadStart(e.getPath), 4194304);
                thread.Start();
                e.wait = false;
                e.waitAt = null;
                pendingRequests.Remove(e);
                Console.WriteLine("Request Granted: " + (pendingRequests.Count) + " to go");
            }
        }
    }
}
