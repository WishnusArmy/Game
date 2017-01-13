using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;

//This object handles pathfinding like an operating system handles processes.
public class PathfindingControl : GameObject
{
    List<Enemy> pendingRequests;
    int timer;

    public PathfindingControl() : base()
    {
        pendingRequests = new List<Enemy>();
        timer = -1;
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
        for(int i = Math.Min(pendingRequests.Count-1, timer); i>=0; --i)
        {
            pendingRequests[i].requestGranted = true;
            pendingRequests.RemoveAt(i);
            timer = -5;
        }
        base.Update(gameTime);
    }
}
