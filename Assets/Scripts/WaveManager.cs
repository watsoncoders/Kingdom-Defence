using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System;
namespace TowerDefend
{
    [System.Serializable]
    public class EnemyType
    {
        public EnemyTypes eType;
        public int roadNumber = 0;
        [HideInInspector]
        public int spawnedEnemy = 0;
        [HideInInspector]
        public float lastSpawnedTime = 0;
        [HideInInspector]
        public bool hasSpawned = false;
        public float firstSpawnTime = 0f;
        public float spawnInterval = 0f;
        public int maxEnemies = 20;

    }
    [System.Serializable]
    public class Wave
    {
        public EnemyType[] enemyTypes;

        public int CountEnemyNumber(EnemyTypes enemyType)
        {
            int count = 0;
            foreach (EnemyType element in enemyTypes)
            {
                if (element.eType == enemyType) count += element.maxEnemies;
            }
            return count;

        }
    }
    public class WaveManager : MonoBehaviour
    {
        private int health;
        public int NormalTower = 99, LongRangTower = 99, CanonTower = 99, MagicTower = 99, Block = 0, SoulTower = 5;
        public bool[] NormalTowerCondition = { true, true, true, true, true },
            CanonTowerCondition = { true, true, true, true, true, true },
            LongRangeTowerCondition = { true, true, true, true, true, true },
            MagicTowerCondition = { true, true, true, true, true, true },
            SoulTowerCondition = { true, true, true, true, true, true };
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                GameController.instance.buttonManager.HealthBannerHandle(health, maxHealth);
            }
        }
        private float money = 100;
        private int mana;
        private List<GameObject> enemiesList = new List<GameObject>();

        public int Mana
        {
            get { return mana; }
            set
            {
                mana = value;
                GameController.instance.buttonManager.DisplayMana();
            }
        }
        public float Money
        {
            get { return money; }
            set
            {
                money = value;
                //check this
                GameController.instance.buttonManager.GoldBannerHandle((int)money);
            }
        }

        private bool isPause, waveCompleteHandle;

        public int level;
        private int towerBuildedCount = 0;
        public int maxTower, startingGold = 50;
        public int maxHealth = 10;
        public int[] starThreshold;
        public bool AStarMap;
        public GameObject[] roads;
        public Wave[] waves;
        public int enemiesSpawned, currentWave, tempCurrentWay;
        private float lastCompetedWaveTime;
        private List<Action> subscribers = new List<Action>();
        private List<GameObject> pathFragments = new List<GameObject>();
        private Vector2[] path = { Vector2.zero, Vector2.one };
        private bool[] roadInUse;

        #region towerBuildSubcriber
        //The Subscribe method for manager
        public void Subscribe(Action subscriber)
        {
            //Debug.Log("Subscriber registered");
            subscribers.Add(subscriber);
        }
        //The Unsubscribe method for manager
        public void UnSubscribe(Action subscriber)
        {
            //Debug.Log("Subscriber registered");
            subscribers.Remove(subscriber);
        }
        //Clear subscribers method for manager
        public void ClearAllSubscribers()
        {
            subscribers.Clear();
        }
        public void OnSellTower()
        {
            towerBuildedCount -= 2;
            OnContructTower();
        }

        public void OnContructBlock()
        {
            this.Block--;
            DisableAllPath();
            foreach (var subscriber in subscribers)
            {
                subscriber();
            }
        }

        public void OnContructTower()
        {
            towerBuildedCount++;
            GameController.instance.buttonManager.DisplayTowerRemain(maxTower - towerBuildedCount);
            DisableAllPath();
            foreach (var subscriber in subscribers)
            {
                subscriber();
            }
        }
        #endregion

        public bool CanBuildTower()
        {
            return towerBuildedCount < maxTower;
        }

        public void DrawPath(Vector3[] path)
        {
            CancelInvoke("DisableAllPath");
            float distance = 0;
            int pathFragmentCount = 0;
            GameObject go = null;
            for (int i = 0; i < path.Length - 1; i++)
            {
                distance = Vector2.Distance(path[i], path[i + 1]);
                pathFragmentCount = (int)(distance / 0.4f);
                for (int j = 0; j < pathFragmentCount; j++)
                {
                    go = ObjectPoolerManager.Instance.pathFragmentPooler.GetPooledObject();
                    go.transform.position = path[i] + (path[i + 1] - path[i]) / pathFragmentCount * j;
                    //1

                    Vector3 newDirection = (path[i + 1] - path[i]);
                    //2
                    float x = newDirection.x;
                    float y = newDirection.y;
                    float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;
                    go.transform.rotation =
                        Quaternion.AngleAxis(rotationAngle, Vector3.forward);
                    go.SetActive(true);
                    pathFragments.Add(go);
                }

            }
            Invoke("DisableAllPath", 1.5f);
        }

        private void DisableAllPath()
        {
            foreach (GameObject go in pathFragments)
            {
                go.SetActive(false);
            }
            pathFragments.Clear();
        }

        void SpawnEnemy(EnemyType enemyType)
        {
            if (isPause)
            {
                enemyType.lastSpawnedTime = Time.time;
            }
            if (!enemyType.hasSpawned)
            {
                enemyType.hasSpawned = true;
                enemyType.lastSpawnedTime = Time.time;
                enemyType.spawnedEnemy = 0;
            }
            else
            {
                if (enemyType.spawnedEnemy < enemyType.maxEnemies)
                {
                    if (((enemyType.spawnedEnemy == 0 && Time.time - enemyType.lastSpawnedTime > enemyType.firstSpawnTime) ||
                             (enemyType.spawnedEnemy != 0 && Time.time - enemyType.lastSpawnedTime > enemyType.spawnInterval)) && enemyType.spawnedEnemy < enemyType.maxEnemies)
                    {
                        enemyType.lastSpawnedTime = Time.time;
                        GameObject go = null;
                        switch (enemyType.eType)
                        {
                            case EnemyTypes.Runner:
                                go = ObjectPoolerManager.Instance.eRunnerPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.DarkWarrior:
                                go = ObjectPoolerManager.Instance.eDarkWarriorPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Butcher:
                                go = ObjectPoolerManager.Instance.eButcherPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Ghost:
                                go = ObjectPoolerManager.Instance.eGhostPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Hellguarder:
                                go = ObjectPoolerManager.Instance.eHellguarderPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Darkwizard:
                                go = ObjectPoolerManager.Instance.eDarkwizardPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Necromancer:
                                go = ObjectPoolerManager.Instance.eNecromancerPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.UndeadWorm:
                                go = ObjectPoolerManager.Instance.eUndeadWormPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Shadow:
                                go = ObjectPoolerManager.Instance.eShadowPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Glutondragon:
                                go = ObjectPoolerManager.Instance.eGlutondragonPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Wyvern:
                                go = ObjectPoolerManager.Instance.eWyvernPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);

                                go.SetActive(true);
                                break;
                            case EnemyTypes.Carrier:
                                go = ObjectPoolerManager.Instance.eCarrierPooler.GetPooledObject();

                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);

                                go.SetActive(true);
                                break;
                            case EnemyTypes.Tyrantdragon:
                                go = ObjectPoolerManager.Instance.eTyrantdragonPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Nightmareshipper:
                                go = ObjectPoolerManager.Instance.eNightmareshipperPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Immortaldevil:
                                go = ObjectPoolerManager.Instance.eImmortaldevilPooler.GetPooledObject();
                                go.GetComponent<Enemy>().Waypoints = SetWayPoint(enemyType.roadNumber);
                                if (AStarMap) go.GetComponent<Enemy>().AutoFindPath = true;
                                go.SetActive(true);
                                break;
                        }
                        if (go != null)
                        {
                            enemiesList.Add(go);
                            go.GetComponent<EnemyDestroyDelegate>().enemyDelegate += OnEnemyDestroy;
                            if (enemyType.spawnedEnemy == 0) go.GetComponent<Enemy>().FirstOne = true;
                        }
                        enemyType.spawnedEnemy++;
                    }
                }
                else
                {
                    if (enemyType.spawnedEnemy == enemyType.maxEnemies)
                    {
                        CheckEndWave();
                        enemyType.spawnedEnemy++;
                    }
                }
            }
        }

        protected void OnEnemyDestroy(GameObject enemy)
        {
            enemiesList.Remove(enemy);
        }

        private Vector3[] SetWayPoint(int roadNumber)
        {
            if (roadNumber >= roads.Length) roadNumber = 0;
            Vector3[] wayPoints = new Vector3[roads[roadNumber].transform.childCount];
            for (int i = 0; i < roads[roadNumber].transform.childCount; i++)
            {
                wayPoints[i] = roads[roadNumber].transform.GetChild(i).position;
            }
            return wayPoints;
        }

        public bool CheckBlockPath(Transform placeHolder)
        {
            if (placeHolder == null) return false;
            var guo = new GraphUpdateObject(placeHolder.GetComponent<BoxCollider2D>().bounds);
            //List<GraphNode> nodeList = new List<GraphNode>();
            for (int i = 0; i < roads.Length; i++)
            {
                if (!GraphUpdateUtilities.UpdateGraphsNoBlock(guo, AstarPath.active.GetNearest(roads[i].transform.GetChild(0).position).node,
                    AstarPath.active.GetNearest(roads[i].transform.GetChild(1).position).node, false))
                {
                    return false;
                }
            }

            return true;
        }

        void CheckEndWave()
        {
            bool waveComplete = true;
            for (int i = 0; i < waves[currentWave].enemyTypes.Length; i++)
            {
                if (waves[currentWave].enemyTypes[i].spawnedEnemy < waves[currentWave].enemyTypes[i].maxEnemies)
                {
                    waveComplete = false;
                    break;
                }

            }
            if (waveComplete)
            {
                if (tempCurrentWay == currentWave)
                {
                    tempCurrentWay++;
                    if (tempCurrentWay < waves.Length)
                        StartCoroutine("DisplayWaveReward");
                }
                lastCompetedWaveTime = Time.time;
            }
        }

        void Start()
        {
            roadInUse = new bool[roads.Length];
            GameController.instance.waveManager = this;
            GameController.instance.GameState = GameStates.Playing;
            Time.timeScale = 1;
            Invoke("Initiate", 1f);
        }


        private void Initiate()
        {
            currentWave = -1;
            GameController.instance.GameState = GameStates.Playing;
            if (GameController.instance.UpgradedItem[Constants.UPGRADE_STARTING_MANA_INDEX])
                mana = 150;
            else
                mana = 100;

            if (GameController.instance.UpgradedItem[Constants.UPGRADE_STARTING_GOLD_INDEX])
                Money = startingGold * Constants.STARTING_GOLD_MULTIPLY;
            else
                Money = startingGold;

            Health = maxHealth;
            tempCurrentWay = 0;
            StartCoroutine("DisplayWaveReward");
            enemiesSpawned = 0;

            GameController.instance.Subscribe(PauseHandled);
            GameController.instance.buttonManager.WaveBannerHandle(currentWave, waves.Length);
            GameController.instance.buttonManager.DisplayTowerRemain(maxTower - towerBuildedCount);
            GameController.instance.buttonManager.DisplayMana();
        }

        void OnDisable()
        {
            Time.timeScale = 1.0f;
            GameController.instance.buttonManager.DisableAllCanvasElement();
            GameController.instance.UnSubscribe(PauseHandled);
            pathFragments.Clear();
        }

        void Update()
        {
            if (isPause) lastCompetedWaveTime += Time.deltaTime;
            if (currentWave != tempCurrentWay)
            {
                if (Time.time - lastCompetedWaveTime > Constants.TIME_BETWEEN_WAVE)
                {
                    currentWave = tempCurrentWay;
                    GameController.instance.buttonManager.DisplayStarOnPlay(CalcStarGained());
                    GameController.instance.buttonManager.WaveBannerHandle(currentWave, waves.Length);
                }
                if (tempCurrentWay >= waves.Length && !waveCompleteHandle)
                {
                    waveCompleteHandle = true;
                    GameController.instance.buttonManager.AllWaveComplete();
                }
            }

            if (currentWave < 0) return;

            if (currentWave < waves.Length)
            {
                for (int i = 0; i < waves[currentWave].enemyTypes.Length; i++)
                {
                    SpawnEnemy(waves[currentWave].enemyTypes[i]);
                }
            }
        }

        void PauseHandled()
        {
            isPause = GameController.instance.IsPause();
        }

        private IEnumerator DisplayWaveReward()
        {
            float startTime = Time.time;
            GameController.instance.buttonManager.DisplayWaveReward(5);
            while (Time.time - startTime < 2)
            {
                if (isPause) startTime += 1;
                lastCompetedWaveTime = Time.time;
                yield return new WaitForSeconds(1);
            }
            startTime = Time.time;
            GameController.instance.buttonManager.DisplayWaveReward(3);
            while (Time.time - startTime < 2)
            {
                lastCompetedWaveTime = Time.time;
                if (isPause) startTime += 1;
                yield return new WaitForSeconds(1);
            }
            GameController.instance.buttonManager.DisplayWaveReward(1);
            startTime = Time.time;
            while (Time.time - startTime <= Constants.TIME_BETWEEN_WAVE)
            {
                if (isPause) startTime += 1;
                yield return new WaitForSeconds(1);
            }
            GameController.instance.buttonManager.DisplayWaveReward(0);
        }

        public void SpawnWaveImmediate()
        {
            lastCompetedWaveTime -= Constants.TIME_BETWEEN_WAVE;
            StopCoroutine("DisplayWaveReward");
        }

        public Wave NextWaveInfo()
        {
            if (currentWave + 1 < waves.Length)
                return waves[currentWave + 1];
            return null;
        }

        public int EnemiesRemainAlive()
        {
            return enemiesList.Count;
        }

        public int CalcStarGained()
        {
            if (currentWave >= starThreshold[2] - 1) return 3;
            else
                if (currentWave >= starThreshold[1] - 1) return 2;
                else
                    if (currentWave >= starThreshold[0] - 1) return 1;
                    else
                        return 0;

        }
    }
}


