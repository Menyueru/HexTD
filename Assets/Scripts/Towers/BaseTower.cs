using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexTD.Towers
{
    public abstract class BaseTower : MonoBehaviour
    {

        public float radius;
        public int price;
        public int sellPrice;
        public float attackRate;

        private float NextAttack;

        public List<Collider2D> EnemiesInRange()
        {
            return Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("Mob")).ToList();
        }

        protected abstract void Attack(List<Enemy> mobs);

        // Use this for initialization
        protected virtual void Start()
        {
            NextAttack = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            var enemiesInRange = EnemiesInRange();
            if (enemiesInRange.Any() && Time.time >= NextAttack)
            {
                Attack(enemiesInRange.Select(x => x.gameObject.GetComponent<Enemy>()).ToList());
                NextAttack = Time.time + attackRate;
            }
        }
    }
}