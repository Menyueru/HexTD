using UnityEngine;
using System.Collections;
using HexTD.PathFinding.Graph;
using HexTD.PathFinding;
using System.Collections.Generic;
using HexTD.PathFinding.Heuristics;

namespace HexTD
{
    public class PathFinder : MonoBehaviour
    {
        public TDGraph graph;
        private BreadthFirstSearch PathFind;
        [HideInInspector]
        public Dictionary<int,int> CurrentPath;
        public int source,target;
        // Use this for initialization
        void Start()
        {
            PathFind = new BreadthFirstSearch();
            CalculateNewPath();
        }

        public GameObject NodeInPath(int nodeIndex)
        {
            return graph.GetNode(nodeIndex).value;
        }

        public bool CalculateNewPath() {
            var path = PathFind.Search(graph, target);
            if (!path.ContainsKey(source)) return false;
            CurrentPath = path;
            return true;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}