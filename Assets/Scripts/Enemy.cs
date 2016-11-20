using UnityEngine;
using System.Collections;

namespace HexTD
{
    public class Enemy : MonoBehaviour
    {

        public int rotationSpeed = 0;
        public float speed=0;
        public float health=0;
        public int reward;
        public GameObject slowedElement;
        [HideInInspector]
        public PathFinder path;
        private int currentNode = 0;
        private float lastNodeSwitch;
        private float originalSpeed;

        IEnumerator RecoverSlow(float slowFor)
        {
            yield return new WaitForSeconds(slowFor);
            speed = originalSpeed;
            slowedElement.SetActive(false);
        }

        public void SlowByPercentageFor(float slowBy,float slowFor)
        {
            originalSpeed = speed;
            speed *= slowBy;
            slowedElement.SetActive(true);
            StartCoroutine("RecoverSlow", slowFor);
        }

        public float DistanceToTarget()
        {
            var distance = 0f;
            var node = path.CurrentPath[currentNode];
            distance += Vector2.Distance(transform.position, path.NodeInPath(node).transform.position);
            while(path.CurrentPath[node] != -1)
            {
                var startPosition = path.NodeInPath(node).transform.position;
                var endPosition = path.NodeInPath(path.CurrentPath[node]).transform.position;
                distance+= Vector2.Distance(startPosition, endPosition);
                node = path.CurrentPath[node]; 
            }
            return distance;
        }

        // Use this for initialization
        void Start()
        {
            this.lastNodeSwitch = Time.time;
            path = GameObject.Find("Board").GetComponent<PathFinder>();
            transform.position = path.NodeInPath(currentNode).transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(health <= 0)
            {
                GameObject.Find("Board").GetComponent<BoardHandle>().currency += reward;
                Destroy(gameObject);
            }
            transform.Rotate(0, 0, rotationSpeed);

            var startPosition = path.NodeInPath(currentNode).transform.position;
            var endPosition = path.NodeInPath(path.CurrentPath[currentNode]).transform.position;
            var distance = Vector2.Distance(startPosition, endPosition);

            var heading = endPosition - startPosition;
            heading.Normalize();

            gameObject.transform.position += (heading * speed * Time.deltaTime);

            if (Vector2.Distance(startPosition,transform.position)>=distance)
            {
                currentNode = path.CurrentPath[currentNode];
                lastNodeSwitch = Time.time;
                if (path.CurrentPath[currentNode] == -1)
                {
                    var board = GameObject.Find("Board").GetComponent<BoardHandle>();
                    board.life--;
                    Destroy(gameObject);
                }
            }
        }
    }
}
