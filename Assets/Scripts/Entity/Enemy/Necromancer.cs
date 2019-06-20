using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefend
{
    class Necromancer : Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_NECROMANCER;
            this.health = Constants.HEATH_NECROMANCER;
            this.armor = Constants.ARMOR_NECROMANCER;
            reward = Constants.REWARD_NECROMANCER;
            GetComponent<CircleCollider2D>().enabled = true;
            base.OnEnable();

        }

        new public void CaculateDamage(int damage)
        {
            if (this.health < 1) return;
            this.health -= (int)(damage * (1 - 0.05f * armor));
            if (healthBar != null)
            {
                healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = this.health / maxHealth;
            }

            if (this.health < 1)
            {
                enemyState = EnemyStates.Die;
                anim.Play("Dead");
                if (UnityEngine.Random.Range(0, 100) > Constants.PERCENT_REVIVE_NECROMANCER)
                    StartCoroutine(Revive());
                else
                    StartCoroutine(DelayDestroy());
            }
        }


        // to do need to instantiate revive animation
        private IEnumerator Revive()
        {
            GameObject go = ObjectPoolerManager.Instance.necromanceReviveEffectPooler.GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<EnemyDestroyDelegate>().enemyDelegate(gameObject);
            yield return new WaitForSeconds(Constants.REVIVE_WAIT_TIME_NECROMANCER);
            GetComponent<CircleCollider2D>().enabled = true;
            anim.Play("Run");
            enemyState = EnemyStates.Run;
            this.health = Constants.HEATH_NECROMANCER;
            CaculateDamage(0);
        }
    }
}
