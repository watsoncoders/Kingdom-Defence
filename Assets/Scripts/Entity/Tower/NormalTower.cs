using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class NormalTower : Tower
    {
        private ArrowTypes arrowType;
        private bool doubleDamage = false;
        void OnEnable()
        {
            base.OnEnable();
            doubleDamage = GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_DOUBLE_DAMAGE_INDEX];
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_COST_INDEX])
                cost = (int)(Constants.NORMAL_TOWER_COST * Constants.UPGRADE_BASIC_COST_MULTIPLY);
            else
                cost = Constants.NORMAL_TOWER_COST;
            arrowType = ArrowTypes.Normal;
            towerType = TowerTypes.Normal;
            
            //upgrade speed shooting
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_ATTACK_SPEED_INDEX])
                AttackSpeedMultiplier = Constants.UPGRADE_BASIC_ATTACK_SPEED_MULTIPLY;
        }

        void OnDisable()
        {
            base.OnDisable();
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_RANGE_INDEX])
            {
                GetComponent<CircleCollider2D>().radius = Constants.NORMAL_TOWER_RANGE;
                transform.GetChild(0).GetChild(0).localScale = Vector2.one;
            }
        }

        void Update()
        {
            if (!isPause && enemiesInRange.Count != 0)
            {
                if ((Time.time - lastShotTime) * attackSpeedMultiplier > Constants.NORMAL_TOWER_WAIT_TIME)
                {
                    Shoot(enemiesInRange[0]);
                    if (doubleDamage && UnityEngine.Random.Range(0, 100) < Constants.UPGRADE_BASIC_DOUBLE_DAMAGE_PERCENT)
                    {
                        //display x2 damage
                        Debug.Log("Basic deal x2 damage");
                        Shoot(enemiesInRange[0]);
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
            newBullet.GetComponent<Arrow>().ArrowType = arrowType;
            newBullet.GetComponent<Bullet>().DamageMultiplier = Constants.NORMAL_TOWER_DAMAGE_MULTIPLIER * damageMultiplier;
            newBullet.GetComponent<Bullet>().SpeedMultiplier = Constants.NORMAL_TOWER_SPEED_MULTIPLIER;
            newBullet.transform.position = transform.position + Vector3.up;
            newBullet.SetActive(true);
        }

        public override int UpgradeCost(int nextLevel)
        {
            int cost = 0;
            switch (nextLevel)
            {
                case Constants.NORMAL_TOWER_FROZEN_LEVEL1:
                    cost = Constants.NORMAL_TOWER_FROZEN_LEVEL1_COST;
                    break;
                case Constants.NORMAL_TOWER_FROZEN_LEVEL2:
                    cost = Constants.NORMAL_TOWER_FROZEN_LEVEL2_COST;
                    break;
                case Constants.NORMAL_TOWER_FIRE_LEVEL1:
                    cost = Constants.NORMAL_TOWER_FIRE_LEVEL1_COST;
                    break;
                case Constants.NORMAL_TOWER_FIRE_LEVEL2:
                    cost = Constants.NORMAL_TOWER_FIRE_LEVEL2_COST;
                    break;
            }
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BASIC_COST_INDEX])
                return (int)(cost * Constants.UPGRADE_BASIC_COST_MULTIPLY);
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
            switch (nextLevel)
            {
                case Constants.NORMAL_TOWER_FROZEN_LEVEL1:
                    level = Constants.NORMAL_TOWER_FROZEN_LEVEL1;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalFrozenLevelOne");
                    arrowType = ArrowTypes.FrozenLevel1;
                    break;
                case Constants.NORMAL_TOWER_FIRE_LEVEL1:
                    level = Constants.NORMAL_TOWER_FIRE_LEVEL1;
                    damageMultiplier = Constants.NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL1;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalFireLevelOne");
                    arrowType = ArrowTypes.FireLevel1;
                    break;
                case Constants.NORMAL_TOWER_FIRE_LEVEL2:
                    level = Constants.NORMAL_TOWER_FIRE_LEVEL2;
                    damageMultiplier = Constants.NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL2;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalFireLevelTwo");
                    arrowType = ArrowTypes.FireLevel2;
                    break;
                case Constants.NORMAL_TOWER_FROZEN_LEVEL2:
                    level = Constants.NORMAL_TOWER_FROZEN_LEVEL2;
                    //damageMultiplier = Constants.NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL2;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalFrozenLevelTwo");
                    arrowType = ArrowTypes.FrozenLevel2;
                    break;
            }
        }
    }
}

