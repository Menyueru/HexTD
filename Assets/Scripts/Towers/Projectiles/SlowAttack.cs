using UnityEngine;
using System.Collections;

namespace HexTD.Towers.Projectiles
{
    public class SlowAttack : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, .4f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, 45);
        }
    }
}