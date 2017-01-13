using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;
using static Functions;

public abstract partial class Enemy
{
    protected void requestPath()
    {
        GameWorld.FindByType<PathfindingControl>()[0].AddRequest(this);
    }
    protected List<GridNode> getPath(GridNode startNode)
    {
        List<GridNode> path = new List<GridNode>(); //Make a container for the return value
        List<GridNode> openList = new List<GridNode>(); //Nodes to be checked
        List<GridNode> closedList = new List<GridNode>(); //Nodes that have been checked
        GridNode targetNode = MyPlane.CenterNode;

        calcNode(startNode, targetNode, openList, closedList); //Start the recursive pathfinding.

        bool done = false; //Used in the while loop
        GridNode currentNode = targetNode; //Start at the target node
        List<GridNode> inList = new List<GridNode>(); //Track the nodes that have already been in the route
        while (!done) //While not reached the origin node
        {
            path.Add(currentNode); //Add the node to the path
            if (currentNode == startNode || currentNode == currentNode.pathParent || inList.Contains(currentNode)) //Path found || stuck
                done = true;
            inList.Add(currentNode);
            currentNode = currentNode.pathParent; //Move to the next node in the path
        }
        Console.WriteLine("Fval: " + path[path.Count-1].Fval + "  ("+MyPlane.FindByType<Enemy>().Count+")");
        return path; //Return the path
    }

    private void calcNode(GridNode node, GridNode targetNode, List<GridNode> openList, List<GridNode> closedList)
    {
        if (openList.Contains(node))
        {
            openList.RemoveAt(0); //Remove self from the openList
        }
        closedList.Add(node); //Add itself to the closedList
        List<GridNode> next = node.Neighbours; //Find all the neighbours
        for(int i=0; i<next.Count; ++i) //Loop through the neighbours
        {
            if (next[i] == targetNode)  //If my neighbour is the target
            {
                targetNode.pathParent = node; //parent self to neighbour
                openList.Clear(); //Clear the openList (ending the recursive call)
                break;
            }
            if (openList.Contains(next[i])) //If neighbour is on the openList
            {
                if (node.Gval + 10 < next[i].Gval) //If the path from here to there is faster than the previous path
                {
                    next[i].pathParent = node; //Reparent to me
                    next[i].Gval = node.Gval + 10; //Update the Gval
                }
            }
            if (!openList.Contains(next[i]) && !closedList.Contains(next[i]) && !next[i].solid) //If neither on the openList or closedList and not solid
            {
                openList.Add(next[i]); //Add Neighbour to the openList
                next[i].pathParent = node; //Make it a parent
                next[i].Gval = node.Gval + 10 + node.Dval; //Add movement and danger cost
                next[i].Fval = next[i].Gval + next[i].Hval; //Update the Fvalue
            }
        }
        //Sort the openList by Fvalue
        openList = openList.OrderBy(o => o.Fval).ToList();
        if (openList.Count > 0) //Are there items in the openList left?
        {
            openList[0].beacon = true;
            calcNode(openList[0], targetNode, openList, closedList); //Recursive call
        }
    }
}
