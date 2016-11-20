using UnityEngine;
using System.Collections;
using System;

namespace HexTD.Towers.Projectiles
{
    public class Bullet : Projectile
    {
        public override void HitEnemy(Enemy mob)
        {
            mob.health -= damage;
        }
    }
}