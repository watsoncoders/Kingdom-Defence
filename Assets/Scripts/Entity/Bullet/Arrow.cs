using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefend
{
    public class Arrow : Bullet
    {
        private ArrowTypes arrowType;

        public ArrowTypes ArrowType
        {
            get { return arrowType; }
            set { arrowType = value; }
        }
        void OnEnable()
        {
            base.OnEnable();
            this.speed = Constants.ARROW_SPEED*speedMultiplier;
            this.damage = (int)(Constants.ARROW_DAMAGE*damageMultiplier);
            //switch (arrowType) { 
            //    case ArrowTypes.Normal:
            //        break;
            //    case ArrowTypes.FrozenLevel1:
            //        frozenAmount = 30;
            //        break;
            //    case ArrowTypes.FrozenLevel2:
            //        frozenAmount = 60;
            //        break;
            //    case ArrowTypes.FireLevel1:
            //        armorPierceAmount = 4;
            //        break;
            //    case ArrowTypes.FireLevel2:
            //        armorPierceAmount = 8;
            //        break;
            //}
        }
        void OnDisable()
        {
            arrowType = ArrowTypes.Normal;
        }
        void Update()
        {
            base.Update();
            BulletPointToTarget();
              MoveToTarget();
              if (gameObject.transform.position.Equals(target.transform.position))
              {
                  //caculate enemy heath
                  GameObject go; 
                  if (target != null && target.activeInHierarchy)
                  {
                      switch (arrowType) { 
                          case ArrowTypes.FrozenLevel1:
                              target.GetComponent<Enemy>().SpeedMultiply = 0.7f;
                              break;
                          case ArrowTypes.FrozenLevel2:
                              target.GetComponent<Enemy>().SpeedMultiply = 0.4f;
                              break;
                          case ArrowTypes.FireLevel1:
                              target.GetComponent<Enemy>().Armor -=4;
                              break;
                          case ArrowTypes.FireLevel2:
                              target.GetComponent<Enemy>().Armor -= 8f;
                              break;
                      }
                      target.GetComponent<Enemy>().CaculateDamage((int) (this.damage));
                      go = ObjectPoolerManager.Instance.bloodEffectPooler.GetPooledObject();
                      go.transform.position = target.transform.position;
                      go.SetActive(true);
                  }
                  gameObject.SetActive(false);
              }
        }
    }
}
