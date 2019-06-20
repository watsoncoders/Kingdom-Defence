using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TowerDefend
{
    class GlutonDragon:Enemy
    {
        private bool isHealing;
        void OnEnable()
        {
            this.speed = Constants.SPEED_GLUTONDRAGON;
            this.health = Constants.HEATH_GLUTONDRAGON;
            this.armor = Constants.ARMOR_GLUTONDRAGON;
            reward = Constants.REWARD_GLUTONDRAGON;
            base.OnEnable();
        }

        void OnDisable() {
            isHealing = false;
            transform.GetChild(2).gameObject.SetActive(false);
            base.OnDisable();
        }
        public override void CaculateDamage(int damage)
        {
            base.CaculateDamage(damage);
            if (!isHealing && 1 < this.health && this.health < maxHealth) { 
            //play health anime and increase health
                isHealing = true;
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(2).GetComponent<Animator>().Play("Heal");
                StartCoroutine(Healing());
            }
        }

        IEnumerator Healing()
        {
            while (isHealing)
            {
                yield return new WaitForSeconds(1);
                this.health = Mathf.Min(this.health + Constants.AMOUNT_HEALTH_REGENARATE_GLUTONDRAGON, this.maxHealth);
                if (healthBar != null)
                {
                    healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = this.health / maxHealth;
                }
                if (this.health >= this.maxHealth)
                {
                    isHealing = false;
                    transform.GetChild(2).gameObject.SetActive(false);
                    //play running anim
                }
            }
        }
    }
}
