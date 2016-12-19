using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Constant;

public partial class Enemy : GameObject
{
    protected List<GridNode> getPath(Vector2 origin, Vector2 target)
    {
        List<GridNode> path = new List<GridNode>();
        List<GridNode> openList = new List<GridNode>();
        List<GridNode> closedList = new List<GridNode>();
        List<GridNode> world = GameWorld.FindByType<Camera>()[0].currentPlane.FindByType<GridNode>();
        GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane;
        GridNode startNode = plane.NodeAt(origin);
        GridNode targetNode = plane.NodeAt(target);
        foreach (GridNode node in world)
        {
            node.Hval = (int)(Math.Abs(node.Position.X - targetNode.Position.X) + Math.Abs(node.Position.Y - targetNode.Position.Y))/16;
            node.pathParent = node;
        }
        calcNode(startNode, targetNode, openList, closedList);
        bool done = false;
        GridNode currentNode = targetNode;
        while(!done)
        {
            path.Add(currentNode);
            if (currentNode == startNode || currentNode == currentNode.pathParent) //Path found || stuck
                done = true;
            currentNode = currentNode.pathParent;
        }
        return path;
    }

    private void calcNode(GridNode node, GridNode targetNode, List<GridNode> openList, List<GridNode> closedList)
    {
        if (onList(node, openList))
        {
            openList.RemoveAt(0); //Remove self from the openList
        }
        closedList.Add(node); //Add itself to the closedList
        List<GridNode> next = node.Neighbours; //Find all the neighbours
        for(int i=0; i<next.Count; ++i)
        {
            if (next[i] == targetNode)
            {
                targetNode.pathParent = node;
                openList.Clear();
                break;
            }
            if (onList(next[i], openList))
            {
                if (node.Gval + 10 < next[i].Gval) //If the path from here to there is faster than the previous path
                {
                    next[i].pathParent = node; //Reparent to me
                    next[i].Gval = node.Gval + 10; //Update the Fval
                }
            }
            if (!onList(next[i], openList) && !onList(next[i], closedList) && !next[i].solid)
            {
                openList.Add(next[i]); //Add Neighbour to the openList
                next[i].pathParent = node; //Make it a parent
                next[i].Gval = node.Gval + 10; //Add movement cost
                next[i].Fval = next[i].Gval + next[i].Hval; //Update the Fvalue
            }
        }
        //Sort the openList by Fvalue
        openList = openList.OrderBy(o => o.Fval).ToList();
        if (openList.Count > 0)
        {
            calcNode(openList[0], targetNode, openList, closedList);
        }
    }

    private bool onList(GridNode node, List<GridNode> list)
    {
        bool flag = false;
        for(int i=0; i<list.Count; ++i)
        {
            if (node == list[i])
                flag = true;
        }
        return flag;
    }
}
