using UnityEngine;
using System.Collections;
using System;

namespace HexTD.PathFinding.Graph
{
    [Serializable]
    public class UnityNode
    {
        public int index;
        public GameObject value;
        [HideInInspector]
        public HexSpace space { get { return this.value.GetComponent<HexSpace>(); } }
        [HideInInspector]
        public bool IsStart { get { return value.tag == "Start"; } }
        [HideInInspector]
        public bool IsEnd { get { return value.tag == "End"; } }

        public UnityNode() : this(-1)
        {
        }
        public UnityNode(int index)
        {
            this.index = index;

        }

        public bool IsPassable()
        {
            return IsStart || IsEnd || !space.HasTower;
        }
    }
}