using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using HexTD.Towers;
using UnityEngine.SceneManagement;

namespace HexTD
{

    [Serializable]
    public class TowerInfo
    {
        public string name;
        public GameObject prefab;
    }

    public class BoardHandle : MonoBehaviour
    {

        public int currency;
        public int life;
        private HexSpace SelectedGameObject;
        public LayerMask HexMask;
        public List<TowerInfo> Towers;
        public Text currencyText;
        public Text lifeText;
        public GameObject ShopPanel;
        public GameObject SellPanel;
        private PathFinder pathfinder;

        // Use this for initialization
        void Start()
        {
            pathfinder = GetComponent<PathFinder>();
        }

        private void SetShopActive()
        {
            ShopPanel.SetActive(true);
            SellPanel.SetActive(false);
        }
        private void SetSellActive()
        {
            ShopPanel.SetActive(false);
            SellPanel.SetActive(true);
            var sellPriceText = SellPanel.transform.Find("Button/Price").GetComponent<Text>();
            sellPriceText.text = SelectedGameObject.Tower.GetComponent<BaseTower>().sellPrice.ToString();
        }
        private void DeactivatePanels()
        {
            ShopPanel.SetActive(false);
            SellPanel.SetActive(false);
        }

        public void SellTower()
        {
            var towerObject = SelectedGameObject.Tower;
            var tower = towerObject.GetComponent<BaseTower>();
            SelectedGameObject.HasTower = false;
            SelectedGameObject.Tower = null;
            currency += tower.sellPrice;
            Destroy(towerObject);
            SetShopActive();
        }

        public void CreateTower(string towerName)
        {
            var towerObject = Towers.First(x => x.name == towerName).prefab;
            var towerType = towerObject.GetComponent<BaseTower>();
            if (currency - towerType.price >= 0 && !SelectedGameObject.HasEnemy())
            {
                SelectedGameObject.HasTower = true;           
                if (!pathfinder.CalculateNewPath())
                {
                    SelectedGameObject.HasTower = false;
                    return;
                }
                SelectedGameObject.Tower = Instantiate(towerObject, SelectedGameObject.transform.position, Quaternion.identity, SelectedGameObject.transform) as GameObject;
                currency -= towerType.price;
                SetSellActive();
            }
        }

        public void Exit()
        {
            Application.Quit();
        }

        private bool IsPointerOverUIObject()
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (life <= 0)
            {
                SceneManager.LoadScene("Game");
            }
            if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
            {
                if (SelectedGameObject != null)
                {
                    SelectedGameObject.ChangeSelected();
                    SelectedGameObject = null;
                }
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, HexMask);

                if (hit.collider != null)
                {
                    var hex = hit.transform.gameObject.GetComponent<HexSpace>();
                    hex.ChangeSelected();
                    SelectedGameObject = hex;
                }
                if (SelectedGameObject != null)
                {
                    if (SelectedGameObject.Tower == null)
                    {
                        SetShopActive();
                    }
                    else
                    {
                        SetSellActive();
                    }
                }
                else
                {
                    DeactivatePanels();
                }
            }
            currencyText.text = currency.ToString();
            lifeText.text = life.ToString();
        }
    }
}