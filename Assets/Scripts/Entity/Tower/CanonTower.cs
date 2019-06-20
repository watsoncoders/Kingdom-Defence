using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class CanonTower : Tower
    {
        private MortarTypes mortarType;
        void OnEnable()
        {
            base.OnEnable();
            cost = Constants.CANON_TOWER_COST;
            towerType = TowerTypes.Canon;
            mortarType = MortarTypes.Normal;
            //upgrade speed shooting
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_CANON_ATTACK_SPEED_INDEX])
                AttackSpeedMultiplier = Constants.UPGRADE_CANON_ATTACK_SPEED_MULTIPLY;
            //upgrade damage
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_CANON_ATTACK_DAMAGE_INDEX])
                DamageMultiplier = Constants.UPGRADE_CANON_ATTACK_DAMAGE_MULTIPLY;
        }
        void Update()
        {
            
            if (!isPause&&enemiesInRange.Count != 0)
            {
                if ((Time.time - lastShotTime) * attackSpeedMultiplier > Constants.CANON_TOWER_WAIT_TIME)
                {
                    Shoot(enemiesInRange[0]);
                    lastShotTime = Time.time;
                }
            }
        }
        protected override void Shoot(GameObject target)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
            GameObject newBullet = ObjectPoolerManager.Instance.mortarPooler.GetPooledObject();
            newBullet.GetComponent<Bullet>().Target = target;
            newBullet.GetComponent<Mortar>().mortarType = mortarType;
            newBullet.GetComponent<Bullet>().DamageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER * damageMultiplier;
            newBullet.transform.position = transform.position + Vector3.up;
            newBullet.SetActive(true);
        }

        public override int UpgradeCost(int nextLevel)
        {
            int cost = 0;
            switch (nextLevel)
            {
                case 2:
                    cost = Constants.CANON_TOWER_LEVEL2_COST;
                    break;
                case 20:
                    cost = Constants.CANON_TOWER_LEVEL3_COST;
                    break;
                case 21:
                    cost = Constants.CANON_TOWER_LEVEL3_COST;
                    break;
                case 200:
                    cost = Constants.CANON_TOWER_LEVEL4_COST;
                    break;
                case 210:
                    cost = Constants.CANON_TOWER_LEVEL4_COST;
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
            GameObject go = ObjectPoolerManager.Instance.canonUpgradePooler.GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            switch (nextLevel)
            {
                case 2:
                    level = 2;
                    damageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL2;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel2");
                    break;
                case 20:
                    level = 20;
                    mortarType = MortarTypes.StunnedLevel1;
                    damageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL3;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel20");
                    break;
                case 21:
                    level = 21;
                    damageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL3;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel21");
                    break;
                case 200:
                    level = 200;
                    mortarType = MortarTypes.StunnedLevel2;
                    damageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL4;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel200");
                    break;
                case 210:
                    level = 210;
                    damageMultiplier = Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL4;
                    transform.GetChild(0).GetComponent<Animator>().Play("NormalLevel210");
                    break;
            }
        }
    }
}


