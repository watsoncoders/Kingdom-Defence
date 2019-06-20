using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace TowerDefend
{
    public class Frozen:Skill
    {
        public override void DoSkill()
        {
            //frozen all enemy
            base.DoSkill();
            transform.position = Vector3.zero;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            StartCoroutine(_DoSkill());
        }

        private IEnumerator _DoSkill() {
            yield return new WaitForSeconds(0.02f);
            if (enemiesInRange.Count > 0)
            {
                foreach (GameObject enemy in enemiesInRange)
                {
                    Instantiate(Resources.Load("EFrozen"), enemy.transform.position, Quaternion.identity);
                    enemy.GetComponent<Enemy>().SpeedMultiply = 0;
                }
            }
            Destroy(gameObject, 0);
        }
    }
}
