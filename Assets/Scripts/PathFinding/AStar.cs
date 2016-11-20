using HexTD.PathFinding.DataStructure;
using HexTD.PathFinding.Graph;
using HexTD.PathFinding.Heuristics;
using System.Collections.Generic;
using System.Linq;

namespace HexTD.PathFinding
{
    public class AStar
    {
        private readonly IHeuristics Heuristic;

        public AStar(IHeuristics heuristic)
        {
            this.Heuristic = heuristic;
        }

        public List<int> Search(TDGraph graph, int source, int target)
        {
            var gCosts = new double[graph.Count()];
            var fCosts = new double[graph.Count()];
            var shortestPathTree = Enumerable.Repeat(-1, graph.Count()).ToArray();
            var searchEdges = Enumerable.Repeat(-1, graph.Count()).ToArray();

            var queue = new MinIndexedPriorityQueue<double>(fCosts, graph.Count());
            queue.Insert(source);
            while (!queue.IsEmpty())
            {
                int nextClosestNode = queue.Pop();

                shortestPathTree[nextClosestNode] = searchEdges[nextClosestNode];

                if (nextClosestNode == target) return GetPathToTarget(shortestPathTree, source, target);

                foreach (var to in graph.GetEdgeListFrom(nextClosestNode))
                {
                    if (!graph.GetNode(to).IsPassable()) continue;

                    double HCost = Heuristic.Calculate(graph, target, to);

                    double GCost = gCosts[nextClosestNode] + 1;

                    if (searchEdges[to] == -1)
                    {
                        fCosts[to] = GCost + HCost;
                        gCosts[to] = GCost;

                        queue.Insert(to);

                        searchEdges[to] = nextClosestNode;
                    }
                    else if ((GCost < gCosts[to]) && (shortestPathTree[to] == -1))
                    {
                        fCosts[to] = GCost + HCost;
                        gCosts[to] = GCost;

                        queue.ChangePriority(to);

                        searchEdges[to] = to;
                    }
                }
            }
            return null;
        }

        private List<int> GetPathToTarget(int[] shortestPathTree, int source, int target)
        {
            var path = new List<int>();
            if (target < 0) return path;
            int nextIndex = target;
            path.Add(nextIndex);

            while ((nextIndex != source) && (shortestPathTree[nextIndex] != -1))
            {
                nextIndex = shortestPathTree[nextIndex];

                path.Add(nextIndex);
            }
            path.Reverse();
            return path;
        }
    }
}