using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Astar pathfinding algorithm
/// </summary>
public static class AStar 
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition,new Node(tile));
        }
    }
    public static /*void*/ Stack<Node> GetPath(Point start , Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closeList = new HashSet<Node>();

        Stack<Node> finalPanth = new Stack<Node>();
       
        Node currentNode = nodes[start];

        openList.Add(currentNode);
        while(openList.Count > 0) //10
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].Walkable && neighbourPos != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if (Math.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        Node neighbour = nodes[neighbourPos];

                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G) //9.4
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closeList.Contains(neighbour)) //9.1
                        {
                            openList.Add(neighbour); //9.2
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
                        }
                        //if (!openList.Contains(neighbour))
                        //{
                        //    openList.Add(neighbour);
                        //}
                        //neighbour.CalcValues(currentNode, nodes[goal], gCost);
                    }
                }
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }
            if(currentNode == nodes[goal])
            {
                while(currentNode.GridPosition != start)
                {
                    finalPanth.Push(currentNode);
                    currentNode = currentNode.Parent;                   
                }
                break;
            }
        }
       return finalPanth;
        //// Debug İçin Sadece ***************************
       // GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList,closeList,finalPanth);
        ////****************************       
    }
    /// <summary>
    /// check if two building connected diagonally
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="neighbour"></param>
    /// <returns></returns>
    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = neighbour.GridPosition - currentNode.GridPosition;
        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y + direction.Y);

        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable)
        {
            return false;
        }
        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }
        return true;
    }
}
