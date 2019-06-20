using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace TowerDefend
{
    class ImmortalDevil : Enemy
    {
        void OnEnable()
        {
            this.speed = Constants.SPEED_IMMORTALDEVIL;
            this.health = Constants.HEATH_IMMORTALDEVIL;
            this.armor = Constants.ARMOR_IMMORTALDEVIL;
            reward = Constants.REWARD_IMMORTALDEVIL;
            base.OnEnable();

            InvokeRepeating("SpawnMinions", 5, Constants.TIME_BETWEEN_SPAWN_MINIONS_IMMORTALDEVIL);
        }

        void OnDisable()
        {
            base.OnDisable();
            //CancelInvoke();
        }

        private void SpawnMinions()
        {
            if (isPause) return;
            transform.GetChild(0).GetComponent<Animator>().Play("Spawn");
            GameObject go;
            for (int i = 0; i < 5; i++)
            {
                go = ObjectPoolerManager.Instance.eUndeadWormPooler.GetPooledObject();
                go.GetComponent<Enemy>().Waypoints = this.Waypoints;
                go.SetActive(true);
                go.GetComponent<Enemy>().CurrentWaypoint = this.CurrentWaypoint;
                go.transform.position = transform.position+i*0.2f*Vector3.left;
            }
        }
    }
}
