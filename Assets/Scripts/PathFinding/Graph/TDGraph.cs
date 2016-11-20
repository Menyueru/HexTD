using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexTD.PathFinding.Graph
{
    [Serializable]
    public class UnityEdges
    {
        public int nodeIndex;
        public List<int> edges;
    }

    [Serializable]
    public class TDGraph
    {
        public List<UnityNode> nodes;
        public List<UnityEdges> edges;

        public UnityNode GetNode(int index)
        {
            return nodes.FirstOrDefault(x => x.index == index);
        }

        public List<int> GetEdgeListFrom(int index)
        {
            return edges[index].edges;
        }

        public int Count()
        {
            return nodes.Count();
        }

        public bool IsEmpty()
        {
            return nodes.Any();
        }

        public bool EdgeExists(int from,int to)
        {
            var edgeList = edges[from];
            return edgeList.edges.Contains(to);
        }
    }
}
