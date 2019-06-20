using UnityEngine;
using System.Collections.Generic;
namespace TowerDefend
{
    public class Tower : MonoBehaviour
    {
        //public Sprite[] sprites;
        protected bool isPause;
        protected float range;
        protected float lastShotTime;

        protected TowerTypes towerType;

        public TowerTypes TowerType
        {
            get { return towerType; }
            set { towerType = value; }
        }

        protected float damageMultiplier;

        public float DamageMultiplier
        {
            get { return damageMultiplier * Random.Range(0.5f, 1f); }
            set { damageMultiplier = value; }
        }

        protected float attackSpeedMultiplier;

        public float AttackSpeedMultiplier
        {
            get { return attackSpeedMultiplier; }
            set { attackSpeedMultiplier = value; }
        }
        protected GameObject placeHolder;

        public GameObject PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value; }
        }

        public float Range
        {
            get { return range; }
            set { range = value; }
        }
        protected int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        protected float timeBetweenTwoShoot;

        public float TimeBetweenTwoShoot
        {
            get { return timeBetweenTwoShoot; }
            set { timeBetweenTwoShoot = value; }
        }

        protected int cost;

        public virtual int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public List<GameObject> enemiesInRange;


        #region MonoBehavior
        // Use this for initialization
        protected void Start()
        {
            enemiesInRange = new List<GameObject>();
        }

        protected void OnEnable()
        {
            lastShotTime = Time.time;
            placeHolder = null;
            damageMultiplier = 1f;
            attackSpeedMultiplier = 1f;
            level = 1;
            PauseHandle();
            GameController.instance.Subscribe(PauseHandle);
        }

        protected void Update()
        {
            if (!isPause) return;
        }

        protected void OnDisable()
        {
            GameController.instance.UnSubscribe(PauseHandle);
            if (placeHolder != null)
            {
                placeHolder.SetActive(true);
                placeHolder = null;
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

        public void SwitchAOE()
        {
            GetComponentInChildren<TowerSpriteManager>().OnMouseDown();
        }

        #endregion
        protected void OnEnemyDestroy(GameObject enemy)
        {
            enemiesInRange.Remove(enemy);
        }

        public virtual void Upgrade(int nextLevel)
        {

        }

        public virtual int UpgradeCost(int nextLevel)
        {
            return 0;
        }

        public void Buy()
        {
            if (GameController.instance.waveManager.Money >= cost)
            {
                // decrease money
                GameController.instance.waveManager.Money -= cost;
                gameObject.SetActive(true);
                if (towerType != TowerTypes.Block)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = true;
                    GameController.instance.waveManager.OnContructTower();
                }
                else
                {
                    GameController.instance.waveManager.OnContructBlock();
                }

                placeHolder.SetActive(false);
                return;
            }
            else
            {
                //not enough money
                GameController.instance.buttonManager.AlertDisplay("Not Enough Money");
                gameObject.SetActive(false);
                return;
            }
        }

        public void Sell()
        {
            // increase money by cost/2.then disable game object.
            GameController.instance.waveManager.Money += (int)cost / 2;
            gameObject.SetActive(false);
            if (towerType == TowerTypes.Block)
            {
                GameController.instance.waveManager.Block+=2;
                GameController.instance.waveManager.OnContructBlock();
            }
            if (GameController.instance.waveManager.AStarMap)
                AstarPath.active.Scan();
            if (towerType != TowerTypes.Block)
                GameController.instance.waveManager.OnSellTower();
        }
        protected virtual void Shoot(GameObject target) { }

        protected virtual void PauseHandle()
        {
            isPause = GameController.instance.IsPause();
        }
    }
}