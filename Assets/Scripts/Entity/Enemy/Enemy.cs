using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pathfinding;
namespace TowerDefend
{
    public class Enemy : MonoBehaviour
    {
        public bool FirstOne = false, LastOne = false;
        protected GameObject healthBar;
        protected bool isPause, isDestroyed;
        protected EnemyStates enemyState;
        public Animator anim;
        protected float originalScale;
        public Vector3[] waypoints;
        private bool isArmorReduced, isSpeedReduced;
        protected bool Stunning;
        private float stunnedTime;
        public virtual float StunnedTime
        {
            set
            {
                if (!Stunning)
                {
                    stunnedTime = value;
                    Stunning = true;
                    Invoke("UnStunned", stunnedTime);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetComponent<Animator>().Play("Stun");
                }
            }
            get { return stunnedTime; }
        }
        //protected float sumStunnedTime;

        public bool AutoFindPath = false;
        public Vector3[] Waypoints
        {
            get { return waypoints; }
            set { waypoints = value; }
        }

        protected int currentWaypoint = 0;
        public int CurrentWaypoint
        {
            get { return currentWaypoint; }
            set { currentWaypoint = value; }
        }

        protected float health, maxHealth;
        public float speed, speedMultiply;
        protected float armor, maxArmor;
        protected int reward;

        public virtual float Armor
        {
            get { return armor; }
            set
            {
                if (!isArmorReduced)
                {
                    isArmorReduced = true;
                    armor = Mathf.Max(value, 0);
                    Invoke("RestoreArmor", 2);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetComponent<Animator>().Play("Damage");
                }
            }
        }

        public virtual float SpeedMultiply
        {
            set
            {
                if (!isSpeedReduced)
                {
                    speedMultiply = value;
                    isSpeedReduced = true;
                    if (speedMultiply > 0.1f)
                        Invoke("RestoreSpeed", 2);
                    else
                        Invoke("RestoreSpeed", Constants.FROZENSKILLDURATION);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetComponent<Animator>().Play("Frozen");
                }
            }
            get { return speedMultiply; }
        }

        public int Reward
        {
            get { return reward; }
            set { reward = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float Heath
        {
            get { return this.health; }
            set { this.health = value; }
        }


        #region MonoBehavior

        protected void OnEnable()
        {
            PauseHandle();

            GameController.instance.Subscribe(PauseHandle);
            if (GameController.instance.waveManager != null)
                GameController.instance.waveManager.Subscribe(OnTowerConstruct);

            isSpeedReduced = false;
            isArmorReduced = false;
            isDestroyed = false;
            Stunning = false;
            this.health += Mathf.Min(GameController.instance.waveManager.currentWave, 50) * Constants.AMOUNT_HEALTH_INCREASE_PER_WAVE;
            this.maxHealth = this.health;
            maxArmor = this.armor;
            speedMultiply = 1;

            enemyState = EnemyStates.Run;

            currentWaypoint = 0;
            transform.position = waypoints[currentWaypoint];

            healthBar = ObjectPoolerManager.Instance.heathBarPooler.GetPooledObject();
            healthBar.transform.position = transform.position + Vector3.up;
            healthBar.transform.localScale = Vector3.one;
            healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            healthBar.SetActive(true);
            if (AutoFindPath) GetComponent<Seeker>().StartPath(waypoints[0], waypoints[1], OnPathComplete);
            else
            {
                if (FirstOne)
                    GameController.instance.waveManager.DrawPath(waypoints);
            }
        }
        public void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                waypoints = null;
                waypoints = p.vectorPath.ToArray();
                if (FirstOne) GameController.instance.waveManager.DrawPath(waypoints);
            }
            currentWaypoint = 0;
        }

        protected void OnDisable()
        {
            CancelInvoke();
            GetComponent<EnemyDestroyDelegate>().DisableGO();
            GameController.instance.UnSubscribe(PauseHandle);
            if (GameController.instance.waveManager != null)
                GameController.instance.waveManager.UnSubscribe(OnTowerConstruct);

            transform.GetChild(1).gameObject.SetActive(false);

            enemyState = EnemyStates.Die;
            if (healthBar != null)
            {
                healthBar.SetActive(false);
                healthBar = null;
            }

            AutoFindPath = false;
            currentWaypoint = 0;
            waypoints = null;
        }

        // Update is called once per frame
        protected void Update()
        {
            if (isPause) return;
            if (enemyState == EnemyStates.Run)
            {
                Vector3 endPosition = waypoints[currentWaypoint + 1];

                if (!Stunning)
                {
                    transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * speedMultiply * Time.deltaTime);
                }
                // 3 
                healthBar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, healthBar.transform.position.z);
                if (gameObject.transform.position == endPosition)
                {
                    if (currentWaypoint < waypoints.Length - 2)
                    {
                        // 3.a 
                        currentWaypoint++;
                        // TODO: Rotate into move direction
                    }
                    else
                    {
                        // 3.b 
                        GameController.instance.DecreaseHealth();
                        gameObject.SetActive(false);
                        // TODO: deduct health
                    }
                }
                //4 HealthBar

            }
        }
        #endregion

        protected void RestoreArmor()
        {
            isArmorReduced = false;
            this.armor = maxArmor;
            transform.GetChild(1).gameObject.SetActive(false);
        }
        protected void RestoreSpeed()
        {
            transform.GetChild(1).gameObject.SetActive(false);
            isSpeedReduced = false;
            this.speedMultiply = 1;
        }

        protected void UnStunned()
        {
            transform.GetChild(1).gameObject.SetActive(false);
            Stunning = false;
            //sumStunnedTime += stunnedTime;
            stunnedTime = 0;
        }
        public virtual void CaculateDamage(int damage)
        {
            this.health -= (int)(damage * (1 - 0.05f * armor));
            if (healthBar != null)
            {
                healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = this.health / maxHealth;
            }

            if (this.health < 1)
            {
                enemyState = EnemyStates.Die;
                //if()
                GetComponent<EnemyDestroyDelegate>().DisableGO();
                if (!isDestroyed)
                    StartCoroutine(DelayDestroy());
                anim.Play("Dead");
            }
        }
        public IEnumerator DelayDestroy()
        {
            isDestroyed = true;
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_BONUS_GOLD_INDEX])
                GameController.instance.waveManager.Money += reward * Constants.BONUS_GOLD_MULTIPLY;
            else
                GameController.instance.waveManager.Money += reward;
            yield return new WaitForSeconds(0.35f);
            gameObject.SetActive(false);
        }

        protected void RotateIntoMoveDirection()
        {
            //1
            Vector3 newStartPosition = waypoints[currentWaypoint];
            Vector3 newEndPosition = waypoints[currentWaypoint + 1];
            Vector3 newDirection = (newEndPosition - newStartPosition);
            //2
            float x = newDirection.x;
            float y = newDirection.y;
            float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
            //3
            GameObject sprite = (GameObject)
                gameObject.transform.FindChild("Sprite").gameObject;
            sprite.transform.rotation =
                Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

        protected virtual void PauseHandle()
        {
            isPause = GameController.instance.IsPause();
        }

        protected void OnTowerConstruct()
        {
            if (AutoFindPath)
            {
                GetComponent<Seeker>().StartPath(transform.position, waypoints[waypoints.Length - 1], OnPathComplete);
            }
            else
            {
                GameController.instance.waveManager.DrawPath(waypoints);
            }
        }
    }
}