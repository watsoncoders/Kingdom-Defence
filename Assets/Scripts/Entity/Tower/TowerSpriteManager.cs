using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace TowerDefend
{
    public class TowerSpriteManager : MonoBehaviour
    {

        private bool aoeSwitch = false;
        private float firstClickTime;
        void OnEnable()
        {
            aoeSwitch = false;
        }

        public void AOEManager(bool b)
        {
            TowerTypes towerType = transform.parent.GetComponent<Tower>().TowerType;
            if (towerType == TowerTypes.Block)
            {
                //GetComponent<SpriteRenderer>().color = Color.red;
                return;
            }
            GameObject aoeSprite = gameObject.transform.FindChild("AOESprite").gameObject;
            if (towerType != TowerTypes.Soul || towerType != TowerTypes.Block)
            {
                if (towerType == TowerTypes.Normal || towerType == TowerTypes.Magic)
                {
                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_RANGE_INDEX] && towerType == TowerTypes.Normal)
                    {
                        transform.parent.GetComponent<CircleCollider2D>().radius = Constants.UPGRADE_BASIC_RANGE_MULTIPLY * Constants.NORMAL_TOWER_RANGE;
                    }
                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_MAGIC_RANGE_INDEX] && towerType == TowerTypes.Magic)
                    {
                        transform.parent.GetComponent<CircleCollider2D>().radius = Constants.UPGRADE_MAGIC_RANGE_MULTIPLY * Constants.MAGIC_TOWER_RANGE;
                    }
                }
                aoeSprite.transform.localScale = (transform.parent.GetComponent<CircleCollider2D>().radius * (new Vector3(1, 1, 0)) + Vector3.one) / transform.localScale.x;
                if (towerType == TowerTypes.Soul)
                    aoeSprite.SetActive(true);
                else
                    aoeSprite.SetActive(b);
            }
        }

        private void SpriteManager(bool b)
        {

            GameObject levelUpgrade = gameObject.transform.FindChild("Upgrade").gameObject;
            //set reward when sell a tower

            GameObject sellBtn = levelUpgrade.transform.FindChild("SellBtn").gameObject;
            sellBtn.transform.GetChild(0).GetComponent<TextMesh>().text = ((int)transform.parent.GetComponent<Tower>().Cost / 2).ToString();
            int level = transform.parent.GetComponent<Tower>().Level;
            TowerTypes towerType = transform.parent.GetComponent<Tower>().TowerType;
            GameObject aoeSprite = null;
            if (towerType == TowerTypes.Block)
            {
                levelUpgrade.SetActive(b);
                return;
            }
            else
                aoeSprite = gameObject.transform.FindChild("AOESprite").gameObject;
            if (towerType != TowerTypes.Soul || towerType != TowerTypes.Block)
            {
                if (towerType == TowerTypes.Normal || towerType == TowerTypes.Magic)
                {
                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_RANGE_INDEX] && towerType == TowerTypes.Normal)
                    {
                        transform.parent.GetComponent<CircleCollider2D>().radius = Constants.UPGRADE_BASIC_RANGE_MULTIPLY * Constants.NORMAL_TOWER_RANGE;
                    }
                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_MAGIC_RANGE_INDEX] && towerType == TowerTypes.Magic)
                    {
                        transform.parent.GetComponent<CircleCollider2D>().radius = Constants.UPGRADE_MAGIC_RANGE_MULTIPLY * Constants.MAGIC_TOWER_RANGE;
                    }
                }
                aoeSprite.transform.localScale = (transform.parent.GetComponent<CircleCollider2D>().radius * (new Vector3(1, 1, 0)) + Vector3.one) / transform.localScale.x;
                aoeSprite.SetActive(b);
            }
            if (towerType != TowerTypes.Block)
            {
                levelUpgrade.transform.GetChild(1).gameObject.SetActive(false);
                levelUpgrade.transform.GetChild(2).gameObject.SetActive(false);
                levelUpgrade.transform.GetChild(3).gameObject.SetActive(false);

                if (towerType != TowerTypes.Normal)
                    levelUpgrade.transform.GetChild(4).gameObject.SetActive(false);
            }
            levelUpgrade.SetActive(b);
            //levelUpgrade.transform.GetChild(level).gameObject.SetActive(true);
            switch (towerType)
            {
                case TowerTypes.Normal:
                    if (level == 1)
                    {
                        if (GameController.instance.waveManager.NormalTowerCondition[1] && GameController.instance.waveManager.NormalTowerCondition[3])
                        {
                            levelUpgrade.transform.GetChild(1).gameObject.SetActive(b);
                            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_COST_INDEX])
                            {
                                TextMesh[] texts = levelUpgrade.transform.GetChild(1).GetComponentsInChildren<TextMesh>();
                                foreach (TextMesh text in texts)
                                {
                                    text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(Constants.NORMAL_TOWER_FROZEN_LEVEL1)).ToString();
                                }
                            }
                        }
                    }
                    else
                        if (level == Constants.NORMAL_TOWER_FROZEN_LEVEL1)
                        {
                            if (GameController.instance.waveManager.NormalTowerCondition[2])
                            {
                                levelUpgrade.transform.GetChild(2).gameObject.SetActive(b);
                                if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_COST_INDEX])
                                {
                                    TextMesh[] texts = levelUpgrade.transform.GetChild(2).GetComponentsInChildren<TextMesh>();
                                    foreach (TextMesh text in texts)
                                    {
                                        text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(Constants.NORMAL_TOWER_FROZEN_LEVEL2)).ToString();
                                    }
                                }
                            }
                        }
                        else
                            if (level == Constants.NORMAL_TOWER_FIRE_LEVEL1)
                            {
                                if (GameController.instance.waveManager.NormalTowerCondition[4])
                                {
                                    levelUpgrade.transform.GetChild(3).gameObject.SetActive(b);
                                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_COST_INDEX])
                                    {
                                        TextMesh[] texts = levelUpgrade.transform.GetChild(3).GetComponentsInChildren<TextMesh>();
                                        foreach (TextMesh text in texts)
                                        {
                                            text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(Constants.NORMAL_TOWER_FROZEN_LEVEL2)).ToString();
                                        }
                                    }
                                }
                            }
                    break;
                default:
                    if (level == 1)
                    {
                        if ((towerType == TowerTypes.LongRange && GameController.instance.waveManager.LongRangeTowerCondition[1]) ||
                            (towerType == TowerTypes.Canon && GameController.instance.waveManager.CanonTowerCondition[1]) ||
                            (towerType == TowerTypes.Magic && GameController.instance.waveManager.MagicTowerCondition[1]) ||
                            (towerType == TowerTypes.Soul && GameController.instance.waveManager.SoulTowerCondition[1])
                            )
                        {
                            levelUpgrade.transform.GetChild(1).gameObject.SetActive(b);
                            if (towerType == TowerTypes.LongRange)
                                if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                                {
                                    TextMesh[] texts = levelUpgrade.transform.GetChild(1).GetComponentsInChildren<TextMesh>();
                                    foreach (TextMesh text in texts)
                                    {
                                        text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(2)).ToString();
                                    }
                                }
                        }
                    }
                    else
                        if (level == 2)
                        {
                            if ((towerType == TowerTypes.LongRange && GameController.instance.waveManager.LongRangeTowerCondition[2] && GameController.instance.waveManager.LongRangeTowerCondition[4]) ||
                            (towerType == TowerTypes.Canon && GameController.instance.waveManager.CanonTowerCondition[2] && GameController.instance.waveManager.CanonTowerCondition[4]) ||
                            (towerType == TowerTypes.Magic && GameController.instance.waveManager.MagicTowerCondition[2] && GameController.instance.waveManager.MagicTowerCondition[4]) ||
                            (towerType == TowerTypes.Soul && GameController.instance.waveManager.SoulTowerCondition[2] && GameController.instance.waveManager.SoulTowerCondition[4])
                            )
                            {
                                levelUpgrade.transform.GetChild(2).gameObject.SetActive(b);
                                if (towerType == TowerTypes.LongRange)
                                    if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                                    {
                                        TextMesh[] texts = levelUpgrade.transform.GetChild(1).GetComponentsInChildren<TextMesh>();
                                        foreach (TextMesh text in texts)
                                        {
                                            text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(20)).ToString();
                                        }
                                    }
                            }
                        }
                        else
                            if (level == 20)
                            {
                                if ((towerType == TowerTypes.LongRange && GameController.instance.waveManager.LongRangeTowerCondition[3]) ||
                            (towerType == TowerTypes.Canon && GameController.instance.waveManager.CanonTowerCondition[3]) ||
                            (towerType == TowerTypes.Magic && GameController.instance.waveManager.MagicTowerCondition[3]) ||
                            (towerType == TowerTypes.Soul && GameController.instance.waveManager.SoulTowerCondition[3])
                            )
                                {
                                    levelUpgrade.transform.GetChild(3).gameObject.SetActive(b);
                                    if (towerType == TowerTypes.LongRange)
                                        if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                                        {
                                            TextMesh[] texts = levelUpgrade.transform.GetChild(1).GetComponentsInChildren<TextMesh>();
                                            foreach (TextMesh text in texts)
                                            {
                                                text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(200)).ToString();
                                            }
                                        }
                                }
                            }
                            else
                                if (level == 21)
                                {
                                    if ((towerType == TowerTypes.LongRange && GameController.instance.waveManager.LongRangeTowerCondition[5]) ||
                            (towerType == TowerTypes.Canon && GameController.instance.waveManager.CanonTowerCondition[5]) ||
                            (towerType == TowerTypes.Magic && GameController.instance.waveManager.MagicTowerCondition[5]) ||
                            (towerType == TowerTypes.Soul && GameController.instance.waveManager.SoulTowerCondition[5])
                            )
                                    {
                                        levelUpgrade.transform.GetChild(4).gameObject.SetActive(b);
                                        if (towerType == TowerTypes.LongRange)
                                            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                                            {
                                                TextMesh[] texts = levelUpgrade.transform.GetChild(1).GetComponentsInChildren<TextMesh>();
                                                foreach (TextMesh text in texts)
                                                {
                                                    text.text = ((int)transform.parent.GetComponent<Tower>().UpgradeCost(Constants.LONG_RANGE_TOWER_COST_LEVEL200)).ToString();
                                                }
                                            }
                                    }
                                }
                    break;
            }
            GetComponent<Animator>().Play("Idle");
            GetComponent<Animator>().Play("Upgrade");
        }

        void Update()
        {
            if (aoeSwitch && Input.GetMouseButtonDown(0) && Time.time - firstClickTime > 0.1f)
            {
                Vector2 direction = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                if (direction.magnitude < 2.5f)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Upgrade"));
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject == gameObject.transform.FindChild("Upgrade").GetChild(0).gameObject)
                            transform.parent.GetComponent<Tower>().Sell();
                        else
                            UpgradeTower(hit.collider.gameObject);
                    }
                }
            }
        }

        private void UpgradeTower(GameObject go)
        {
            int level = transform.parent.GetComponent<Tower>().Level;
            GameObject levelUpgrade;
            TowerTypes towerType = transform.parent.GetComponent<Tower>().TowerType;
            switch (towerType)
            {
                case TowerTypes.Normal:
                    if (level == 1)
                    {
                        levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(1).gameObject;
                        if (go == levelUpgrade.transform.GetChild(0).gameObject)
                            transform.parent.GetComponent<NormalTower>().Upgrade(Constants.NORMAL_TOWER_FROZEN_LEVEL1);
                        else if (go == levelUpgrade.transform.GetChild(1).gameObject)
                            transform.parent.GetComponent<NormalTower>().Upgrade(Constants.NORMAL_TOWER_FIRE_LEVEL1);
                    }
                    else
                        if (level == Constants.NORMAL_TOWER_FROZEN_LEVEL1)
                        {
                            levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(2).gameObject;
                            if (go == levelUpgrade.transform.GetChild(0).gameObject)
                                transform.parent.GetComponent<NormalTower>().Upgrade(Constants.NORMAL_TOWER_FROZEN_LEVEL2);
                        }
                        else
                            if (level == Constants.NORMAL_TOWER_FIRE_LEVEL1)
                            {
                                levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(3).gameObject;
                                if (go == levelUpgrade.transform.GetChild(0).gameObject)
                                    transform.parent.GetComponent<NormalTower>().Upgrade(Constants.NORMAL_TOWER_FIRE_LEVEL2);
                            }
                    break;
                default:
                    if (level == 1)
                    {
                        levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(1).gameObject;
                        if (go == levelUpgrade.transform.GetChild(0).gameObject)
                            transform.parent.GetComponent<Tower>().Upgrade(2);
                    }
                    else
                        if (level == 2)
                        {
                            levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(2).gameObject;
                            if (go == levelUpgrade.transform.GetChild(0).gameObject)
                                transform.parent.GetComponent<Tower>().Upgrade(20);
                            if (go == levelUpgrade.transform.GetChild(1).gameObject)
                                transform.parent.GetComponent<Tower>().Upgrade(21);
                        }
                        else
                            if (level == 20)
                            {
                                levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(3).gameObject;
                                if (go == levelUpgrade.transform.GetChild(0).gameObject)
                                    transform.parent.GetComponent<Tower>().Upgrade(200);
                            }
                            else
                                if (level == 21)
                                {
                                    levelUpgrade = gameObject.transform.FindChild("Upgrade").GetChild(4).gameObject;
                                    if (go == levelUpgrade.transform.GetChild(0).gameObject)
                                        transform.parent.GetComponent<Tower>().Upgrade(210);
                                }
                    break;
            }

            OnMouseDown();
        }

        public void AOEColor(bool b)
        {
            GameObject aoeSprite = gameObject.transform.FindChild("AOESprite").gameObject;
            if (b)
            {
                aoeSprite.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                aoeSprite.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        public void OnMouseDown()
        {
            if (!aoeSwitch)
            {
                firstClickTime = Time.time;
                aoeSwitch = true;
                SpriteManager(true);
                Invoke("OnMouseDown", 2);
            }
            else
            {
                CancelInvoke();
                aoeSwitch = false;
                if (transform.parent.GetComponent<Tower>().TowerType != TowerTypes.Soul && transform.parent.GetComponent<Tower>().TowerType != TowerTypes.Block)
                    gameObject.transform.FindChild("AOESprite").gameObject.SetActive(false);
                SpriteManager(false);
            }

        }
    }
}