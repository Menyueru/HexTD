using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace HexTD.Towers
{
    public class SnowTower : BaseTower
    {
        public float slowByPercentage = 0f;
        public float slowFor = 0f;
        public GameObject SlowAttackAnimation;

        private float slowBy;

        protected override void Start()
        {
            base.Start();
            slowBy = slowByPercentage / 100;
        }

        protected override void Attack(List<Enemy> mobs)
        {
            Instantiate(SlowAttackAnimation,transform.position,Quaternion.identity);
            foreach (var mob in mobs)
            {
                mob.SlowByPercentageFor(slowBy, slowFor);
            }
        }
    }
}