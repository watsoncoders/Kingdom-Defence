using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class SoulTower : Tower
    {
        public List<GameObject> towerInRange, tempTowerInRange;
        private float damageBonus, speedBonus;
        // Use this for initialization

        protected void Start()
        {
            towerInRange = new List<GameObject>();
        }

        protected void OnEnable()
        {
            base.OnEnable();
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS;
            speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS;
            towerType = TowerTypes.Soul;
            cost = Constants.SOUL_TOWER_COST;
            InvokeRepeating("DetectTowerInRange", 0.5f, 2f);
            InvokeRepeating("HandleEnemyInRange", 0.5f, 1f);
        }

        protected void OnDisable()
        {
            CancelInvoke();
            if (towerInRange.Count > 0)
            {
                foreach (GameObject go in towerInRange)
                {
                    go.transform.FindChild("BuffEffect").gameObject.SetActive(false);
                    go.GetComponent<Tower>().DamageMultiplier = 1f;
                    go.GetComponent<Tower>().AttackSpeedMultiplier = 1f;
                }
            }
            towerInRange.Clear();
            transform.GetChild(0).FindChild("Frozen").gameObject.SetActive(false);
            base.OnDisable();
        }

        private void HandleEnemyInRange()
        {
            if (level < 3) return;
            foreach (GameObject go in enemiesInRange)
            {
                switch (level)
                {
                    case 20:
                        go.GetComponent<Enemy>().Armor = 0.75f;
                        break;
                    case 21:
                        go.GetComponent<Enemy>().SpeedMultiply = 0.7f;
                        break;
                    case 200:
                        go.GetComponent<Enemy>().Armor = 0.75f;
                        break;
                    case 210:
                        go.GetComponent<Enemy>().SpeedMultiply = 0.4f;
                        break;
                }
            }
        }

        private void DetectTowerInRange()
        {
            float radius = GetComponent<CircleCollider2D>().radius;
            tempTowerInRange.Clear();
            Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);
            if (col.Length == 0)
            {
                return;
            }

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.transform.parent.tag == Strings.TOWER && col[i].gameObject.transform.parent != transform)
                {
                    tempTowerInRange.Add(col[i].transform.parent.gameObject);
                }
            }

            if (tempTowerInRange.Count() > 0)
            {
                foreach (GameObject go in tempTowerInRange)
                {
                    if (!towerInRange.Exists(x => x == go))
                    {
                        towerInRange.Add(go);
                        go.transform.FindChild("BuffEffect").gameObject.SetActive(true);
                        go.GetComponent<Tower>().DamageMultiplier = damageBonus;
                        go.GetComponent<Tower>().AttackSpeedMultiplier = speedBonus;
                    }
                }

                if (towerInRange.Count() > 0)
                {
                    int tmp = towerInRange.Count();
                    for (int i = 0; i < tmp; i++)
                    {
                        if (!tempTowerInRange.Exists(x => x == towerInRange[i]))
                        {
                            towerInRange[i].transform.FindChild("BuffEffect").gameObject.SetActive(false);
                            towerInRange[i].GetComponent<Tower>().DamageMultiplier = 1f;
                            towerInRange[i].GetComponent<Tower>().AttackSpeedMultiplier = 1f;
                            towerInRange.RemoveAt(i);
                        }
                    }
                }
            }
            else
            {
                if (towerInRange.Count() > 0)
                    foreach (GameObject go in towerInRange)
                    {
                        go.transform.FindChild("BuffEffect").gameObject.SetActive(false);
                        go.GetComponent<Tower>().DamageMultiplier = 1f;
                        go.GetComponent<Tower>().AttackSpeedMultiplier = 1f;
                    }
                towerInRange.Clear();
            }
        }

        public override int UpgradeCost(int nextLevel)
        {
            int cost = 0;
            switch (nextLevel)
            {
                case 2:
                    cost = Constants.SOUL_TOWER_COST_LEVEL2;
                    break;
                case 20:
                    cost = Constants.SOUL_TOWER_COST_LEVEL20;
                    break;
                case 21:
                    cost = Constants.SOUL_TOWER_COST_LEVEL21;
                    break;
                case 200:
                    cost = Constants.SOUL_TOWER_COST_LEVEL200;
                    break;
                case 210:
                    cost = Constants.SOUL_TOWER_COST_LEVEL210;
                    break;
            }
            return cost;
        }

        public override void Upgrade(int nextLevel)
        {
            base.Upgrade(nextLevel);
            //check money available for upgrade.
            if (GameController.instance.waveManager.Money < UpgradeCost(nextLevel))
            {
                GameController.instance.buttonManager.AlertDisplay("Not Enough Money");
                return;
            }
            else
            {
                cost += UpgradeCost(nextLevel);
                GameController.instance.waveManager.Money -= UpgradeCost(nextLevel);
            }
            GameObject go = ObjectPoolerManager.Instance.soulUpgradePooler.GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            switch (nextLevel)
            {
                case 2:
                    level = 2;
                    damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL2;
                    speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL2;
                    GetComponent<CircleCollider2D>().radius = Constants.UPGRADE_BASIC_RANGE_MULTIPLY * Constants.NORMAL_TOWER_RANGE;
                    //transform.GetChild(0).GetChild(0).localScale = Constants.SOUL_TOWER_RANGE_MULTIPLY * 8 * (Vector2.one);
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel2");
                    break;
                case 20:
                    level = 20;
                    damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL20;
                    speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL20;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel20");
                    break;
                case 21:
                    level = 21;
                    damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL21;
                    speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL21;
                    transform.GetChild(0).FindChild("Frozen").gameObject.SetActive(true);
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel21");
                    break;
                case 200:
                    level = 200;
                    damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL200;
                    speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL200;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel200");
                    break;
                case 210:
                    level = 210;
                    damageBonus = Constants.SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL210;
                    speedBonus = Constants.SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL210;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel210");
                    break;
            }
        }
    }
}
