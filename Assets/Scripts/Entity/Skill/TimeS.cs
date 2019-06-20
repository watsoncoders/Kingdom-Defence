using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace TowerDefend
{
    public class TimeS : Skill
    {
        public override void DoSkill()
        {
            transform.position = Vector3.zero;
            //GetComponent<CircleCollider2D>().enabled = false;
            //GetComponent<CircleCollider2D>().enabled = true;
            GameController.instance.Pause();
            StartCoroutine(DestroySkill());
        }

        private IEnumerator DestroySkill()
        {
            yield return new WaitForSeconds(Constants.TIMESKILLDURATION);
            GameController.instance.Pause();
            Destroy(gameObject);
            yield return null;
        }
    }
}
