using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

public class SortingThread : GameObject
{
    static List<GameObjectList> queue = new List<GameObjectList>();

    public static void AddRequest(GameObjectList obj)
    {
        if (!queue.Contains(obj))
        {
            queue.Add(obj);
            //Console.WriteLine("Added a queue item: "+obj);
        }
    }

    public override void Update(object gameTime)
    {
        base.Update(gameTime);
        while(queue.Count > 0)
        {
            Thread thread = new Thread(new ThreadStart(queue[0].SortChildren));
            thread.Start();
            queue.RemoveAt(0);
            Console.WriteLine("Granted Request, " + queue.Count() + " to go.");
        }
    }
}
