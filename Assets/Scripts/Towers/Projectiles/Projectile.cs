using UnityEngine;
using System.Collections;

namespace HexTD.Towers.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public int damage;
        public float speed = 1;
        public float maxDistance = 0;

        public Vector3 heading;
        private Vector2 startPosition;

        public abstract void HitEnemy(Enemy enemy);

        // Use this for initialization
        void Start()
        {
            startPosition = transform.position;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Mob"))
            {
                HitEnemy(other.gameObject.GetComponent<Enemy>());
            }
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += (heading * Time.deltaTime * speed);
            var distance = Vector2.Distance(transform.position, startPosition);
            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
