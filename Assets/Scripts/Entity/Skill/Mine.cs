using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefend
{
    class Mine:Skill
    {
        private bool ready;
        void OnEnable() {
            damage = Constants.MINE_DAMAGE;
            ready = false;
        }

        public override void DoSkill()
        {
            base.DoSkill();
            ready = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("Standby");
        }

        void FixedUpdate() {
            if (ready&&enemiesInRange.Count > 0) {
                Invoke("Explode",Constants.MINE_EXPLODE_WAIT_TIME);
                ready = false;
            }
        }

        private void Explode() {
            for (int i = 0; i < enemiesInRange.Count;i++ )
            {
                if (enemiesInRange[i] == null) continue;
                enemiesInRange[i].GetComponent<EnemyDestroyDelegate>().enemyDelegate -= OnEnemyDestroy;
                enemiesInRange[i].GetComponent<Enemy>().CaculateDamage(this.damage);
            }
            GetComponent<Animator>().Play("Skill");
            Destroy(gameObject, 1);
        }

    }
}
