using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TowerDefend
{
    public class MagicBall:Bullet
    {
        public bool IceBall=false;
        void OnEnable()
        {
            base.OnEnable();
            this.speed = Constants.MAGIC_BALL_SPEED * speedMultiplier;
            this.damage = (int) (Constants.MAGIC_BALL_DAMAGE*damageMultiplier);
        }

        void OnDisable() {
            IceBall = false;
        }

        void Update()
        {
            base.Update();
            MoveToTarget();
            if (gameObject.transform.position.Equals(target.transform.position))
            {
                //caculate enemy heath
                GameObject go;
                if (target != null && target.activeInHierarchy)
                {
                    if (IceBall) target.GetComponent<Enemy>().SpeedMultiply = 0.8f;
                    target.GetComponent<Enemy>().CaculateDamage((int)(this.damage));
                    go = ObjectPoolerManager.Instance.bloodEffectPooler.GetPooledObject();
                    go.transform.position = target.transform.position;
                    go.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
