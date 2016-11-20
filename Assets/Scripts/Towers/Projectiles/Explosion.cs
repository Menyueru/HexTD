using UnityEngine;
using System.Collections;

namespace HexTD.Towers.Projectiles
{
    public class Explosion : MonoBehaviour
    {
        void FinishExplosion()
        {
            Destroy(gameObject);
        }
    }
}