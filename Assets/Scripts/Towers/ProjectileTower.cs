using HexTD.Towers.Projectiles;
using System.Collections.Generic;
using UnityEngine;

namespace HexTD.Towers
{
    public class ProjectileTower : BaseTower
    {
        public int damage;
        public GameObject projectilePrefab;
        private GameObject cannon;

        protected override void Start()
        {
            base.Start();
            foreach(Transform children in transform)
            {
                if (children.gameObject.tag == "Cannon")
                {
                    cannon = children.gameObject;
                }
            }
        }

        protected override void Attack(List<Enemy> mobs)
        {
            Enemy target = null;
            var minDistance = float.MaxValue;
            foreach(var mob in mobs)
            {
                var distance = mob.DistanceToTarget();
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = mob;
                }
            }
            if (target != null)
            {
                var startPosition = gameObject.transform.position;
                var targetPosition = target.transform.position;
                var heading = (targetPosition - startPosition);
                heading.Normalize();
                float rotationAngle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;
                cannon.transform.rotation = Quaternion.AngleAxis(rotationAngle - 90, Vector3.forward);

                var projectileObject = Instantiate(projectilePrefab, startPosition, transform.rotation) as GameObject;
                var projectile = projectileObject.GetComponent<Projectile>();
                projectile.damage = damage;
                projectile.heading = heading;
            }
        }
    }
}