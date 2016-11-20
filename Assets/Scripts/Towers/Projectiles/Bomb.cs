using System;
using UnityEngine;
using System.Linq;

namespace HexTD.Towers.Projectiles
{
    public class Bomb : Projectile
    {
        public float radius;
        public GameObject explosion;

        public override void HitEnemy(Enemy enemy)
        {
            Instantiate(explosion, enemy.transform.position, Quaternion.identity);
            var mobs = Physics2D.OverlapCircleAll(enemy.transform.position, radius, 1 << LayerMask.NameToLayer("Mob")).Select(x => x.gameObject.GetComponent<Enemy>()).ToList();
            foreach(var mob in mobs)
            {
                mob.health -= damage;
            }
        }
    }
}
