using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace TowerDefend
{
    public class LongRangeTower : Tower
    {
        void OnEnable()
        {
            base.OnEnable();
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                cost = (int)(Constants.LONG_RANGE_TOWER_COST * Constants.UPGRADE_ARCHER_COST_MULTIPLY);
            else
                cost = Constants.LONG_RANGE_TOWER_COST;
            towerType = TowerTypes.LongRange;
            //upgrade speed shooting
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_ATTACK_SPEED_INDEX])
                AttackSpeedMultiplier = Constants.UPGRADE_ARCHER_ATTACK_SPEED_MULTIPLY;

            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_ATTACK_DAMAGE_INDEX])
                DamageMultiplier = Constants.UPGRADE_ARCHER_ATTACK_DAMAGE_MULTIPLY;


        }
        void Update()
        {
            if (!isPause && enemiesInRange.Count != 0)
            {
                if ((Time.time - lastShotTime) * attackSpeedMultiplier > Constants.LONG_RANGE_TOWER_WAIT_TIME)
                {
                    Shoot(enemiesInRange[0]);
                    //multiply shooting
                    if (enemiesInRange.Count > 1)
                    {
                        if (level == 21) Shoot(enemiesInRange[1]);
                        if (level == 210)
                        {
                            Shoot(enemiesInRange[1]);
                            if (enemiesInRange.Count > 2)
                                Shoot(enemiesInRange[2]);
                        }
                    }
                    lastShotTime = Time.time;
                }
            }
        }
        protected override void Shoot(GameObject target)
        {

            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
            GameObject newBullet = ObjectPoolerManager.Instance.arrowPooler.GetPooledObject();
            newBullet.GetComponent<Bullet>().Target = target;
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_DOUBLE_ARROW_INDEX]&&(UnityEngine.Random.Range(0,100)<20))
            {
                Debug.Log("Long range x2");
                newBullet.GetComponent<Bullet>().DamageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER * damageMultiplier * 2;
            }
            else
                newBullet.GetComponent<Bullet>().DamageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER * damageMultiplier;

            newBullet.GetComponent<Bullet>().SpeedMultiplier = Constants.LONG_RANGE_TOWER_SPEED_MULTIPLIER;
            newBullet.transform.position = transform.position + Vector3.up;
            newBullet.SetActive(true);
        }

        public override int UpgradeCost(int nextLevel)
        {
            int cost = 0;
            switch (nextLevel)
            {
                case 2:
                    cost = Constants.LONG_RANGE_TOWER_COST_LEVEL2;
                    break;
                case 20:
                    cost = Constants.LONG_RANGE_TOWER_COST_LEVEL20;
                    break;
                case 21:
                    cost = Constants.LONG_RANGE_TOWER_COST_LEVEL21;
                    break;
                case 200:
                    cost = Constants.LONG_RANGE_TOWER_COST_LEVEL200;
                    break;
                case 210:
                    cost = Constants.LONG_RANGE_TOWER_COST_LEVEL210;
                    break;
            }
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_ARCHER_COST_INDEX])
                return (int)(cost * Constants.UPGRADE_ARCHER_COST_MULTIPLY);
            else
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
            GameObject go = ObjectPoolerManager.Instance.longRangeUpgradePooler.GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            switch (nextLevel)
            {
                case 2:
                    level = 2;
                    damageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL2;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel2");
                    break;
                case 20:
                    level = 20;
                    damageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL20;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel20");
                    break;
                case 21:
                    level = 21;
                    damageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL21;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel21");
                    break;
                case 200:
                    level = 200;
                    damageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL200;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel200");
                    break;
                case 210:
                    level = 210;
                    damageMultiplier = Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL210;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel210");
                    break;
            }
        }
    }
}
