using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend

{
    class NightmareShipper:Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_NIGHTMARESHIPPER;
            this.health = Constants.HEATH_NIGHTMARESHIPPER;
            this.armor = Constants.ARMOR_NIGHTMARESHIPPER;
            reward = Constants.REWARD_NIGHTMARESHIPPER;
            base.OnEnable();

        }

        public override void CaculateDamage(int damage)
        {
            if (Random.Range(0, 100) > Constants.DODGE_PERCENT_NIGHTMARESHIPPER)
                base.CaculateDamage(damage);
            else { 
            //play dodge anim.
                Debug.Log("Dodge");
            }
        }
    }
}
