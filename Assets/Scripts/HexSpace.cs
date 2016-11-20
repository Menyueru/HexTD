using UnityEngine;
using System.Collections;

namespace HexTD
{
    public class HexSpace : MonoBehaviour
    {
        public bool selected = false;
        public GameObject SelectedBorder;
        [HideInInspector]
        public bool HasTower;
        [HideInInspector]
        public GameObject Tower;

        // Use this for initialization
        void Start()
        {
            HasTower = false;
        }

        public bool ChangeSelected()
        {
            selected = !selected;
            SelectedBorder.SetActive(selected);
            return selected;
        }

        public bool HasEnemy()
        {
            return Physics2D.OverlapCircle(transform.position, .8f, 1 << LayerMask.NameToLayer("Mob"));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}