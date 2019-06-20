using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefend
{
    class Ghost:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_GHOST;
            this.health = Constants.HEATH_GHOST;
            this.armor = Constants.ARMOR_GHOST;
            reward = Constants.REWARD_GHOST;
            GetComponent<CircleCollider2D>().enabled = true;
            base.OnEnable();

        }

        public override void CaculateDamage(int damage)
        {
            this.health -= (int)(damage * (1 - 0.05f * armor));
            if (healthBar != null)
            {
                healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = this.health / maxHealth;
            }

            if (this.health < 1)
            {
                enemyState = EnemyStates.Die;
                StartCoroutine(DelayDestroy());
                anim.Play("Dead");
            }
            else
            {
                StartCoroutine(Invisible());
            }
        }

        IEnumerator Invisible() {
            //need to disable collision and play invisible anim.
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponentInChildren<Animator>().Play("Invisible");
            GetComponent<EnemyDestroyDelegate>().enemyDelegate(gameObject);
            yield return new WaitForSeconds(Constants.INVISIBLE_WAIT_TIME_GHOST);
            GetComponent<CircleCollider2D>().enabled = true;
            //need to enable collison and run anim.
        }
    }
}
