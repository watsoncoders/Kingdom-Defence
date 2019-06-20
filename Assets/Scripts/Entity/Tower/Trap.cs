using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefend;
using UnityEngine;

namespace Assets.Scripts.Entity.Tower
{
    public class Trap : TowerDefend.Tower
    {
        public TrapType trapType;
        void OnEnable()
        {
            base.OnEnable();
            switch (trapType)
            {
                case TrapType.Poison:
                    break;
                case TrapType.Fire:
                    break;
                case TrapType.Ice:
                    break;

            }
        }

        void Update()
        {
            if (enemiesInRange.Count != 0)
            {
                if ((Time.time - lastShotTime) * attackSpeedMultiplier > Constants.TRAP_WAIT_TIME)
                {
                    lastShotTime = Time.time;
                    Shoot();
                }
            }
        }

        protected void Shoot()
        {
            int count = enemiesInRange.Count;
            GameObject go = null;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    go = enemiesInRange[i];
                }
                else
                    continue;

                go.GetComponent<Enemy>().CaculateDamage(Constants.TRAP_DAMAGE);

                if (trapType == TrapType.Ice)
                {
                    go.GetComponent<Enemy>().SpeedMultiply = Constants.ICE_TRAP_FREEZE;
                }
                if (trapType == TrapType.Fire)
                {
                    go.GetComponent<Enemy>().Armor -= Constants.FIRE_TRAP_AMOR_PIERCE;
                }
            }
        }

        new protected void OnTriggerEnter2D(Collider2D other)
        {
            // 2
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
                if (trapType == TrapType.Poison)
                    other.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
                EnemyDestroyDelegate enemy =
                    other.gameObject.GetComponent<EnemyDestroyDelegate>();
                enemy.enemyDelegate += OnEnemyDestroy;
            }
        }

        // 3
        new protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
                if (trapType == TrapType.Poison)
                    other.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                EnemyDestroyDelegate enemy =
                    other.gameObject.GetComponent<EnemyDestroyDelegate>();
                enemy.enemyDelegate -= OnEnemyDestroy;
            }
        }


    }
}
