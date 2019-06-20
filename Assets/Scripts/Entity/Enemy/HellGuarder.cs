using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    class HellGuarder:Enemy
    {

        public List<GameObject> enemiesInRange;
        protected void OnEnable()
        {
            this.speed = Constants.SPEED_HELLGUARDER;
            this.health = Constants.HEATH_HELLGUARDER;
            this.armor = Constants.ARMOR_HELLGUARDER;
            reward = Constants.REWARD_HELLGUARD;
            base.OnEnable();

        }

        protected void OnDisable() {
            base.OnDisable();
            GameObject tmp=ObjectPoolerManager.Instance.hellGuardExplodePooler.GetPooledObject();
            tmp.transform.position = transform.position;
            tmp.SetActive(true);
            //need to instantiate destroy and decrease heath of nearby enemy.
            foreach (GameObject go in enemiesInRange) {
                go.GetComponent<Enemy>().CaculateDamage(Constants.EXPLODE_DAMAGE_HELLGUARD);
            }
            enemiesInRange.Clear();
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            // 2
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
                EnemyDestroyDelegate enemy =
                    other.gameObject.GetComponent<EnemyDestroyDelegate>();
                enemy.enemyDelegate += OnEnemyDestroy;
            }
        }
        // 3
        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
                EnemyDestroyDelegate enemy =
                    other.gameObject.GetComponent<EnemyDestroyDelegate>();
                enemy.enemyDelegate -= OnEnemyDestroy;
            }
        }

        protected void OnEnemyDestroy(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

    }
}
