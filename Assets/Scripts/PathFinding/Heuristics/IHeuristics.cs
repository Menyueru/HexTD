using HexTD.PathFinding.Graph;
using UnityEngine;

namespace HexTD.PathFinding.Heuristics
{
    public interface IHeuristics
    {
        double Calculate(TDGraph graph, int from, int to);
    }

    public class EuclideanHeuristic : IHeuristics
    {
        public double Calculate(TDGraph graph, int from, int to)
        {
            var fromNode = graph.GetNode(from).value;
            var toNode = graph.GetNode(to).value;
            return Vector2.Distance(fromNode.transform.position, toNode.transform.position);
        }
    }

    public class EuclideanSquaredHeuristic : IHeuristics
    {
        public double Calculate(TDGraph graph, int from, int to)
        {
            var fromNode = graph.GetNode(from).value;
            var toNode = graph.GetNode(to).value;
            return Vector2.SqrMagnitude(fromNode.transform.position) + Vector2.SqrMagnitude(toNode.transform.position);
        }
    }

    public class ManhattanHeuristic : IHeuristics
    {
        public double Calculate(TDGraph graph, int from, int to)
        {
            var fromNode = graph.GetNode(from).value;
            var toNode = graph.GetNode(to).value;
            return Mathf.Abs(fromNode.transform.position.x - toNode.transform.position.x) + Mathf.Abs(fromNode.transform.position.y - toNode.transform.position.y);
        }
    }
}