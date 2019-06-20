using System;
using System.Collections;
using System.Linq;
using UnityEngine;
namespace TowerDefend
{
    class Meteor : Skill
    {

        public override void DoSkill()
        {
            StartCoroutine(_DoSkill());
        }

        private IEnumerator _DoSkill()
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.01f);
            GetComponentInChildren<Animator>().enabled = true;
            if (enemiesInRange.Count > 0)
            {
                int count = enemiesInRange.Count;
                for (int i = 0; i < count; i++)
                {
                    if (enemiesInRange[i] == null) continue;
                    enemiesInRange[i].GetComponent<EnemyDestroyDelegate>().enemyDelegate -= OnEnemyDestroy;
                    enemiesInRange[i].GetComponent<Enemy>().CaculateDamage(this.damage);
                }
            }
            GetComponentInChildren<Animator>().Play("Skill");
            Destroy(gameObject, 1);

        }

        void Start()
        {
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_METEOR_DAMAGE_INDEX])
                damage = Constants.METEOR_DAMAGE * Constants.METEOR_DAMAGE;
            else
                damage = Constants.METEOR_DAMAGE;
        }


    }
}
