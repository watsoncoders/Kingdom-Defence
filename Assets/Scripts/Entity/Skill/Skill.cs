using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class Skill:MonoBehaviour
    {
        protected int damage;
        public List<GameObject> enemiesInRange;

        public virtual void DoSkill() {
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
