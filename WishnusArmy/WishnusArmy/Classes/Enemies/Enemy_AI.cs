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
        List<GridNode> path = new List<GridNode>(); //Make a container for the return value
        List<GridNode> openList = new List<GridNode>(); //Nodes to be checked
        List<GridNode> closedList = new List<GridNode>(); //Nodes that have been checked
        GridPlane plane = GameWorld.FindByType<Camera>()[0].currentPlane; //get the currentplane
        GridNode startNode, targetNode;
        try
        {
            startNode = plane.NodeAt(origin); //Get the node from where to start
            targetNode = plane.NodeAt(target); //Get the node at the target position
        }
        catch(Exception e)
        {
            throw e;
        }

        foreach (GridNode node in plane.grid)
        {
            node.Hval = (int)(Math.Abs(node.Position.X - targetNode.Position.X) + Math.Abs(node.Position.Y - targetNode.Position.Y))/64;  //Calculate the Heuristic
            node.pathParent = node; //Reset the parent
        }

        calcNode(startNode, targetNode, openList, closedList); //Start the recursive pathfinding.

        bool done = false; //Used in the while loop
        GridNode currentNode = targetNode; //Start at the target node
        while(!done) //While not reached the origin node
        {
            path.Add(currentNode); //Add the node to the path
            if (currentNode == startNode || currentNode == currentNode.pathParent) //Path found || stuck
                done = true;
            currentNode = currentNode.pathParent; //Move to the next node in the path
        }
        return path; //Return the path
    }

    private void calcNode(GridNode node, GridNode targetNode, List<GridNode> openList, List<GridNode> closedList)
    {
        if (onList(node, openList))
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
            if (onList(next[i], openList)) //If neighbour is on the openList
            {
                if (node.Gval + 10 < next[i].Gval) //If the path from here to there is faster than the previous path
                {
                    next[i].pathParent = node; //Reparent to me
                    next[i].Gval = node.Gval + 10; //Update the Fval
                }
            }
            if (!onList(next[i], openList) && !onList(next[i], closedList) && !next[i].solid) //If neither on the openList or closedList
            {
                openList.Add(next[i]); //Add Neighbour to the openList
                next[i].pathParent = node; //Make it a parent
                next[i].Gval = node.Gval + 10; //Add movement cost
                next[i].Fval = next[i].Gval + next[i].Hval; //Update the Fvalue
            }
        }
        //Sort the openList by Fvalue
        openList = openList.OrderBy(o => o.Fval).ToList();
        if (openList.Count > 0) //Are their items in the openList left?
        {
            calcNode(openList[0], targetNode, openList, closedList); //Recursive call
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
