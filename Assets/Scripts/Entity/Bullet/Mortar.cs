using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class Mortar : Bullet
    {
        public MortarTypes mortarType;
        private Vector2 dropPoint;
        private Vector2 velocity;
        private float timeMortarFlying;
        protected List<GameObject> enemiesInRange;

        public void Shoot()
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().velocity += CalculateForce();
            startTime = Time.time;
        }

        //calculate force when shoots a mortar
        private Vector2 CalculateForce()
        {
            float deltaY, deltaX;
            //calculate distance from target to object
            Vector2 tmp = dropPoint - (Vector2)transform.position;
            deltaY = tmp.y;
            deltaX = tmp.x;
            float yVelc = deltaY < 0 ? Constants.MORTAR_Y_VELOCITY / 2 : Constants.MORTAR_Y_VELOCITY;
            float t = (yVelc
                + Mathf.Sqrt(yVelc * yVelc - 2 * Constants.GRAVITATIONAL_ACCELERATION * deltaY)) / Constants.GRAVITATIONAL_ACCELERATION;
            timeMortarFlying = t;
            //calculate velocity in x axis.
            float x = deltaX / t;
            return new Vector2(x, yVelc);
        }

        protected void OnEnemyDestroy(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        void OnEnable()
        {
            base.OnEnable();
            enemiesInRange = new List<GameObject>();
            dropPoint = target.transform.position;
            this.damage = (int)(Constants.MORTAR_DAMAGE * damageMultiplier);
            Shoot();
        }

        void OnDisable()
        {
            mortarType = MortarTypes.Normal;
            dropPoint = Vector2.zero;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timeMortarFlying = 0;
            startTime = 0;
            enemiesInRange.Clear();
        }

        void FixedUpdate()
        {

        }

        new protected void Update()
        {
            if (Time.time - startTime > timeMortarFlying)
            {
                if (enemiesInRange.Count != 0)
                {
                    foreach (GameObject enemy in enemiesInRange.ToList())
                    {
                        switch (mortarType)
                        {
                            case MortarTypes.StunnedLevel1:
                                enemy.GetComponent<Enemy>().StunnedTime = 1;
                                break;
                            case MortarTypes.StunnedLevel2:
                                enemy.GetComponent<Enemy>().StunnedTime = 2;
                                break;
                            case MortarTypes.Normal:
                                break;
                        }
                        enemy.GetComponent<Enemy>().CaculateDamage(this.damage);
                        enemy.GetComponent<EnemyDestroyDelegate>().enemyDelegate -= OnEnemyDestroy;
                    }
                }
                GameObject go = ObjectPoolerManager.Instance.mortarExplodePooler.GetPooledObject();
                go.transform.position = transform.position;
                go.SetActive(true);
                gameObject.SetActive(false);
            }
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

        protected override void PauseHandle()
        {
            base.PauseHandle();
            if (isPause)
            {
                velocity = GetComponent<Rigidbody2D>().velocity;
                GetComponent<Rigidbody2D>().Sleep();
            }
            else
            {
                GetComponent<Rigidbody2D>().WakeUp();
                GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
    }
}
