using UnityEngine;
using System.Collections;
namespace TowerDefend
{
    public class Bullet : MonoBehaviour
    {
        protected bool isPause;
        protected float startTime;
        protected Vector3 startPosition;
        protected int frozenAmount;

        public int FrozenAmount
        {
            get { return frozenAmount; }
            set { frozenAmount = value; }
        }

        protected int armorPierceAmount;

        public int ArmorPierceAmount
        {
            get { return armorPierceAmount; }
            set { armorPierceAmount = value; }
        }

        public float stunnedTime;

        public float StunnedTime
        {
            get { return stunnedTime; }
            set { stunnedTime = value; }
        }

        protected float distanceToTarget;

        //public Vector3 StartPosition
        //{
        //    get { return startPosition; }
        //    set { startPosition = value; }
        //}
        protected float speed;
        protected float speedMultiplier;

        public float SpeedMultiplier
        {
            get { return speedMultiplier; }
            set { speedMultiplier = value; }
        }


        protected int damage;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        protected GameObject target;

        public GameObject Target
        {
            get { return target; }
            set { target = value; }
        }

        protected float damageMultiplier;

        public float DamageMultiplier
        {
            get { return damageMultiplier; }
            set { damageMultiplier = value; }
        }

        protected void BulletPointToTarget()
        {
            Vector3 newDirection = target.transform.position - transform.position;
            float rotationAngle = (Mathf.Atan2(newDirection.y, newDirection.x) * 180) / Mathf.PI - 90;
            GameObject sprite = (GameObject)
                gameObject.transform.FindChild("Sprite").gameObject;
            sprite.transform.rotation =
                Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

        protected void MortarAglineWithVelocity()
        {
            if (target != null)
            {
                //calculate angle between bullet and target.
                float angle = Vector2.Angle(GetComponent<Rigidbody2D>().velocity, new Vector2(0, -1));
                if (GetComponent<Rigidbody2D>().velocity.x > 0)
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                else
                    transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
            else return;
        }

        //dan bay thang
        protected void MoveToTarget()
        {
            float timeInterval = Time.time - startTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, target.transform.position, timeInterval * speed / distanceToTarget);
        }

        #region MonoBehavior
        // Use this for initialization
        protected void OnEnable()
        {
            isPause = true;
            startTime = Time.time;
            startPosition = transform.position;
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            GameController.instance.Subscribe(PauseHandle);
            if (GameController.instance.Sound)
            {
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();
            }
        }

        void OnDisable()
        {
            stunnedTime = 0;
            frozenAmount = 0;
            armorPierceAmount = 0;
            GameController.instance.UnSubscribe(PauseHandle);
        }

        protected void FixedUpdate()
        {
            if (isPause) return;
        }

        protected void Update()
        {
            if (isPause) return;
        }
        #endregion

        protected virtual void PauseHandle()
        {
            isPause = GameController.instance.IsPause();
        }
    }
}