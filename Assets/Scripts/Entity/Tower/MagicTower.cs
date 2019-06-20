using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class MagicTower : Tower
    {
        void OnEnable()
        {
            base.OnEnable();
            cost = Constants.MAGIC_TOWER_COST;
            towerType = TowerTypes.Magic;
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_MAGIC_DAMAGE_INDEX])
                DamageMultiplier = Constants.UPGRADE_MAGIC_DAMAGE_MULTIPLY;
        }

        void OnDisable()
        {
            base.OnDisable();
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_MAGIC_RANGE_INDEX])
            {
                GetComponent<CircleCollider2D>().radius = Constants.MAGIC_TOWER_RANGE;
            }
        }
        void Update()
        {
            if (!isPause && enemiesInRange.Count != 0)
            {
                if ((Time.time - lastShotTime) * attackSpeedMultiplier > Constants.MAGIC_TOWER_WAIT_TIME)
                {
                    if (level == 21 || level == 210)
                    {
                        for (int i = 0; i < enemiesInRange.Count(); i++)
                        {
                            Shoot(enemiesInRange[i]);
                            if (i == 5) break;
                        }
                    }
                    else
                        if (level == 20 || level == 200)
                        {
                            for (int i = 0; i < enemiesInRange.Count(); i++)
                            {
                                Shoot(enemiesInRange[i]);
                                if (i == 1) break;
                            }
                        }
                        else
                            Shoot(enemiesInRange[0]);

                    lastShotTime = Time.time;
                }
            }
        }

        protected override void Shoot(GameObject target)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
            transform.GetChild(1).gameObject.SetActive(false);
            Invoke("EnableMagicBall", Constants.MAGIC_TOWER_SHOOT_PLAYING_TIME);
            GameObject newBullet = ObjectPoolerManager.Instance.magicBallPooler.GetPooledObject();
            newBullet.GetComponent<Bullet>().Target = target;
            if (level == 200)
                newBullet.GetComponent<MagicBall>().IceBall = true;
            newBullet.GetComponent<Bullet>().DamageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER * damageMultiplier;
            newBullet.GetComponent<Bullet>().SpeedMultiplier = Constants.MAGIC_TOWER_SPEED_MULTIPLIER;
            newBullet.transform.position = transform.position + Vector3.up;
            newBullet.SetActive(true);
        }

        private void EnableMagicBall()
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        public override int UpgradeCost(int nextLevel)
        {
            int cost = 0;
            switch (nextLevel)
            {
                case 2:
                    cost = Constants.MAGIC_TOWER_COST_LEVEL2;
                    break;
                case 20:
                    cost = Constants.MAGIC_TOWER_COST_LEVEL20;
                    break;
                case 21:
                    cost = Constants.MAGIC_TOWER_COST_LEVEL21;
                    break;
                case 200:
                    cost = Constants.MAGIC_TOWER_COST_LEVEL200;
                    break;
                case 210:
                    cost = Constants.MAGIC_TOWER_COST_LEVEL210;
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
            GameObject go = ObjectPoolerManager.Instance.magicUpgradePooler.GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            switch (nextLevel)
            {
                case 2:
                    level = 2;
                    damageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL2;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel2");
                    break;
                case 20:
                    level = 20;
                    damageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL20;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel20");
                    break;
                case 21:
                    level = 21;
                    damageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL21;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel21");
                    break;
                case 200:
                    level = 200;
                    damageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL200;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel200");
                    break;
                case 210:
                    level = 210;
                    damageMultiplier = Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL210;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel210");
                    break;
            }
        }
    }
}
