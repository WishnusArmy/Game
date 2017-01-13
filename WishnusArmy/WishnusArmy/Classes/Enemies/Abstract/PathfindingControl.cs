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
    List<Enemy> pendingRequests;
    int timer;

    public PathfindingControl() : base()
    {
        pendingRequests = new List<Enemy>();
        timer = -1;
        threadCount = 0;
    }

    public void AddRequest(Enemy e)
    {
        if (!pendingRequests.Contains(e))
        {
            e.requestGranted = false;
            pendingRequests.Add(e);
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (timer < 0)
            timer++;
        if (pendingRequests.Count > 1000)
            throw new Exception("Pathfinding Request Overflow!");
        for(int i = Math.Min(pendingRequests.Count-1, 0); i>=0; --i)
        {
            if (timer == 0)
            {
                pendingRequests[i].requestGranted = true;
                pendingRequests.RemoveAt(i);
                timer = -5 - (threadCount)*5;
                //Console.WriteLine("Request Granted ("+threadCount+" active): " + (pendingRequests.Count) + " to go, delay set to: " + timer);
            }
        }
        base.Update(gameTime);
    }
}
