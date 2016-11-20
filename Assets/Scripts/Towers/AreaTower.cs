using HexTD.Towers.Projectiles;
using System.Collections.Generic;
using UnityEngine;

namespace HexTD.Towers
{
    public class AreaTower : BaseTower
    {
        public int damage = 0;
        public GameObject projectilePrefab;

        void SendProjectile(Vector2 heading)
        {
            var projectileObject = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            var projectile = projectileObject.GetComponent<Projectile>();
            projectile.damage = damage;
            projectile.heading = heading;
        }

        protected override void Attack(List<Enemy> mobs)
        {
            SendProjectile(new Vector2(-0.75f, 0.5f));
            SendProjectile(new Vector2(0f, 1f));

            SendProjectile(new Vector2(0.75f, 0.5f));
            SendProjectile(new Vector2(0.75f, -0.5f));

            SendProjectile(new Vector2(0f, -1f));
            SendProjectile(new Vector2(-0.75f, -0.5f));
        }

    }
}