using HexTD.PathFinding.DataStructure;
using HexTD.PathFinding.Graph;
using System.Collections.Generic;
using UnityEngine;

namespace HexTD.PathFinding
{
    public class BreadthFirstSearch
    {
        public Dictionary<int,int> Search(TDGraph graph, int target)
        {
            var distances = new float[graph.Count()];
            var frontier = new MinIndexedPriorityQueue<float>(distances, graph.Count());
            distances[target] = 0;
            frontier.Insert(target);
            var came_from = new Dictionary<int, int>();
            came_from.Add(target, -1);
            while (!frontier.IsEmpty())
            {
                var current = frontier.Pop();
                foreach (var node in graph.GetEdgeListFrom(current))
                {
                    var distance = distances[current] + 1;
                    if (!came_from.ContainsKey(node))
                    {
                        distances[node] = distance;
                        came_from.Add(node, current);
                        if (!graph.GetNode(node).IsPassable()) continue;
                        frontier.Insert(node);                   
                    }else if (distance<distances[node])
                    {
                        if (!graph.GetNode(node).IsPassable()) continue;
                        distances[node] = distance;
                        frontier.ChangePriority(node);
                        came_from[node]= current;
                    }
                }
            }
            return came_from;
        }
    }
}
