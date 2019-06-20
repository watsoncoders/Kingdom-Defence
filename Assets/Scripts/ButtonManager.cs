using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
namespace TowerDefend
{
    public class ButtonManager : MonoBehaviour
    {
        public GameObject waveBanner, healthBanner, goldBanner, timeCountDownBanner, pauseDialog, sumaryDialog;
        public GameObject waveInfo, waveReward, skillBook, waveInfoBtn, alertMessage, towerRemainField;
        public GameObject[] levels;
        public GameObject starFieldPlaying, gemField, gemAlert;
        public Sprite[] star;
        private bool countingDown;
        [HideInInspector]
        public bool LoadingLevel = false;
        private WaveManager waveManager = null;
        public GameObject CanvasAsync;
        #region Monobehavior
        void Start()
        {
            LoadingLevel = true;
            //DisplayGem();
        }

        void FixedUpdate()
        {
            if (LoadingLevel && Application.loadedLevel == 1)
            {
                StopAllCoroutines();
                LoadingLevel = false;
                transform.GetChild(1).gameObject.SetActive(false);
                Invoke("DisplayLevelStar", 0.005f);
                Invoke("DisplayGem", 0.05f);
            }
            if (LoadingLevel && Application.loadedLevel != 1 &&
                GameController.instance.GameState != GameStates.LevelMap)
            {
                StopAllCoroutines();
                LoadingLevel = false;
                transform.GetChild(0).gameObject.SetActive(false);
                if (Application.loadedLevel > 5)
                    transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        #endregion
        public void PauseBtnHandle()
        {
            GameController.instance.Pause();
            pauseDialog.SetActive(!pauseDialog.activeInHierarchy);
        }

        public void FastForwardBtnManager()
        {
            if (Time.timeScale < 1.5) Time.timeScale = 2f;
            else Time.timeScale = 1.0f;
        }

        public void HideDisplayWaveReward()
        {
            if (waveManager == null) waveManager = GameController.instance.waveManager;
            waveManager.SpawnWaveImmediate();
            waveManager.Mana += int.Parse(waveReward.GetComponentInChildren<Text>().text);
            AlertDisplay("+ " + waveReward.GetComponentInChildren<Text>().text + " Mana");
            waveReward.SetActive(false);
            waveInfoBtn.SetActive(false);
            countingDown = false;
        }

        public void LevelChose(int i)
        {
            if (i == 0) GameController.instance.GameState = GameStates.Menu;
            else
                if (i == 1) GameController.instance.GameState = GameStates.LevelMap;
                else
                    if (i < 6) GameController.instance.GameState = GameStates.Upgrade;
                    else
                    {
                        //alert out of gem here.
                        if (GameController.instance.Gem < 10)
                        {
                            GameController.instance.buttonManager.GemAlert(Strings.OUT_OF_GEM);
                            return;
                        }
                        GameController.instance.GameState = GameStates.Playing;
                        GameController.instance.Gem -= 10;
                    }
            GameController.instance.LoadLevelAsync(i);
        }

        public void DisplayMana()
        {
            if (waveManager == null) waveManager = GameController.instance.waveManager;
            skillBook.GetComponentInChildren<Text>().text = waveManager.Mana.ToString();
        }

        public void DisplayWaveReward(int i)
        {
            if (i == 1) CountDownHandle(Constants.TIME_BETWEEN_WAVE);
            if (i > 0)
            {
                if (!waveInfoBtn.activeInHierarchy) waveInfoBtn.SetActive(true);
                waveReward.SetActive(true);
                waveReward.GetComponentInChildren<Text>().text = i.ToString();
            }
            else
            {
                if (waveInfoBtn.activeInHierarchy) waveInfoBtn.SetActive(false);
                waveReward.SetActive(false);
                countingDown = false;
            }
        }


        //Display Wave Info
        public void WaveInfoBtn()
        {
            //display wave info and pause game
            waveInfo.SetActive(!waveInfo.activeInHierarchy);
            GameController.instance.Pause();
            if (waveInfo.activeInHierarchy)
            {
                Wave nextWave = GameController.instance.waveManager.NextWaveInfo();
                if (nextWave == null) return;

                int[] enemyCount = new int[15];
                for (int i = 0; i < 15; i++)
                {
                    enemyCount[i] = 0;
                    enemyCount[i] = nextWave.CountEnemyNumber((EnemyTypes)i);
                }
                GameObject go = null;
                for (int i = 0; i < 15; i++)
                {
                    if (enemyCount[i] > 0)
                    {
                        switch ((EnemyTypes)i)
                        {
                            case EnemyTypes.Runner:
                                go = Instantiate(Resources.Load("RunnerInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.DarkWarrior:
                                go = Instantiate(Resources.Load("DarkWarriorInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Butcher:
                                go = Instantiate(Resources.Load("ButcherInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Carrier:
                                go = Instantiate(Resources.Load("CarrierInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Darkwizard:
                                go = Instantiate(Resources.Load("DarkWizardInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Ghost:
                                go = Instantiate(Resources.Load("GhostInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Glutondragon:
                                go = Instantiate(Resources.Load("GlutonDragonInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Hellguarder:
                                go = Instantiate(Resources.Load("HellGuarderInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Immortaldevil:
                                go = Instantiate(Resources.Load("ImmortalDevilInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Tyrantdragon:
                                go = Instantiate(Resources.Load("TyrrantDragonInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Necromancer:
                                go = Instantiate(Resources.Load("NecromancerInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Nightmareshipper:
                                go = Instantiate(Resources.Load("NightmareShipperInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.UndeadWorm:
                                go = Instantiate(Resources.Load("UndeadWormInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                            case EnemyTypes.Wyvern:
                                go = Instantiate(Resources.Load("WyvernInfo"), waveInfo.transform.position, Quaternion.identity) as GameObject;
                                go.transform.SetParent(waveInfo.transform);
                                go.transform.localPosition = Vector3.zero;
                                go.SetActive(true);
                                break;
                        }
                        if (go != null)
                            go.GetComponentInChildren<Text>().text = "X " + enemyCount[i];
                    }

                }

                switch (waveInfo.transform.childCount - 1)
                {
                    case 1:
                        //do nothing.
                        break;
                    case 2:
                        waveInfo.transform.GetChild(1).localPosition = 125 * Vector2.left;
                        waveInfo.transform.GetChild(2).localPosition = 25 * Vector2.right;
                        Debug.Log(waveInfo.transform.GetChild(2).localPosition);
                        break;
                    case 3:
                        waveInfo.transform.GetChild(1).localPosition = 50 * Vector2.up;
                        waveInfo.transform.GetChild(2).localPosition = 50 * Vector2.down + 125 * Vector2.left;
                        waveInfo.transform.GetChild(3).localPosition = 50 * Vector2.down + 25 * Vector2.right;
                        break;
                    case 4:
                        waveInfo.transform.GetChild(1).localPosition = 50 * Vector2.up + 25 * Vector2.right;
                        waveInfo.transform.GetChild(2).localPosition = 50 * Vector2.up + 125 * Vector2.left;
                        waveInfo.transform.GetChild(3).localPosition = 50 * Vector2.down + 125 * Vector2.left;
                        waveInfo.transform.GetChild(4).localPosition = 50 * Vector2.down + 25 * Vector2.right;
                        break;
                }
            }
            else
            {
                for (int i = 1; i < waveInfo.transform.childCount; i++)
                {
                    Destroy(waveInfo.transform.GetChild(i).gameObject);
                }
            }
        }

        public void WaveInfoBtnHandle()
        {
            waveInfoBtn.SetActive(!waveInfoBtn.activeInHierarchy);
        }

        //Handle Wave banner display
        public void WaveBannerHandle(int current, int max)
        {
            current = Mathf.Min(current + 1, max);
            GameController.instance.buttonManager = this;
            waveBanner.GetComponentInChildren<Text>().text = current.ToString() + "/" + max.ToString();
            waveBanner.transform.FindChild("Mask").GetComponent<Image>().fillAmount = (float)current / max;
        }


        //Time count down
        public void CountDownHandle(int time)
        {
            GameController.instance.buttonManager = this;
            countingDown = true;
            StartCoroutine(DoCountDown(time));
            timeCountDownBanner.SetActive(true);
        }

        private IEnumerator DoCountDown(int time)
        {
            float maxTime = time - 1;
            float fTime = time - 1;
            while (fTime > 0)
            {
                yield return new WaitForSeconds(0.05f);
                //check if player is viewing wave info
                if (!GameController.instance.IsPause())
                {
                    fTime -= 0.05f;
                    timeCountDownBanner.GetComponentInChildren<Text>().text = ((int)fTime).ToString();
                    timeCountDownBanner.transform.FindChild("Mask").GetComponent<Image>().fillAmount = fTime / maxTime;
                }
                if (!countingDown) break;
            }
            countingDown = false;
            timeCountDownBanner.SetActive(false);
        }

        //Handle Health banner display
        public void HealthBannerHandle(int health, int max)
        {
            if (health > 0)
            {
                healthBanner.GetComponentInChildren<Text>().text = health.ToString() + "/" + max.ToString();
                healthBanner.transform.FindChild("Mask").GetComponent<Image>().fillAmount = (float)health / max;
            }
            else
            {
                SumaryDisplay();
                GameController.instance.Pause();
            }
        }

        public void DisplayStarOnPlay(int i)
        {
            if (i > 0)
            {
                if (starFieldPlaying.transform.GetChild(i - 1).gameObject.activeSelf == false)
                    starFieldPlaying.transform.GetChild(i - 1).gameObject.SetActive(true);
            }
            else
            {

                for (int j = 0; j < starFieldPlaying.transform.childCount; j++)
                {
                    starFieldPlaying.transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
        public void DisplayTowerRemain(int towerRemain)
        {
            towerRemainField.GetComponent<Text>().text = "Tower remains: " + towerRemain + ".";
        }
        //Handle Gold banner display
        public void GoldBannerHandle(int amount)
        {
            goldBanner.GetComponentInChildren<Text>().text = amount.ToString();
        }

        public void AlertDisplay(string s)
        {
            alertMessage.GetComponent<Text>().text = s;
            alertMessage.SetActive(true);
            Invoke("AlertDisable", 1);
        }

        private void AlertDisable()
        {
            alertMessage.SetActive(false);
        }

        private void SumaryDisplay()
        {
            if (GameController.instance.waveManager.EnemiesRemainAlive() < 1 ||
                GameController.instance.waveManager.Health < 1)
            {
                DisplayStarOnPlay(0);
                sumaryDialog.SetActive(true);
                Invoke("PlayStarAnim", 1);
            }
            else
                Invoke("SumaryDisplay", 1);
        }

        private void PlayStarAnim()
        {
            int count = 0;
            count = GameController.instance.waveManager.CalcStarGained();
            sumaryDialog.transform.GetChild(5).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(6).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(1).gameObject.SetActive(false);
            switch (count)
            {
                case 0:
                    sumaryDialog.transform.GetChild(1).gameObject.SetActive(false);
                    sumaryDialog.transform.GetChild(5).gameObject.SetActive(true);
                    sumaryDialog.transform.GetChild(6).gameObject.SetActive(false);
                    //GameController.instance.Gem -= 10;
                    break;
                case 1:
                    sumaryDialog.transform.GetChild(1).gameObject.SetActive(true);
                    sumaryDialog.GetComponent<Animator>().Play("OneStar");
                    GameController.instance.Gem += 20;
                    break;
                case 2:
                    sumaryDialog.transform.GetChild(1).gameObject.SetActive(true);
                    sumaryDialog.GetComponent<Animator>().Play("TwoStar");
                    GameController.instance.Gem += 20;
                    break;
                case 3:
                    sumaryDialog.transform.GetChild(1).gameObject.SetActive(true);
                    sumaryDialog.GetComponent<Animator>().Play("ThreeStar");
                    GameController.instance.Gem += 20;
                    break;
            }
            SaveLevelStar(GameController.instance.waveManager.level, count);
        }
        public void AllWaveComplete()
        {
            AlertDisplay("All waves completed");
            Invoke("SumaryDisplay", 0);
            Time.timeScale = 1;
        }

        public void Replay()
        {
            if (pauseDialog.activeInHierarchy)
                PauseBtnHandle();
            if (sumaryDialog.activeInHierarchy)
            {
                sumaryDialog.transform.GetChild(5).gameObject.SetActive(false);
                sumaryDialog.transform.GetChild(6).gameObject.SetActive(false);
                sumaryDialog.transform.GetChild(1).gameObject.SetActive(false);
                sumaryDialog.SetActive(false);
                GameController.instance.Pause();
            }
            GameController.instance.GameState = GameStates.Menu;
            GameController.instance.LoadLevelAsync(Application.loadedLevel);
        }

        public void SwitchToLevelChoose()
        {
            sumaryDialog.SetActive(false);
            sumaryDialog.transform.GetChild(5).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(6).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(1).gameObject.SetActive(false);

            LevelChose(1);
        }

        public void SwitchToMenu()
        {
            sumaryDialog.SetActive(false);
            sumaryDialog.transform.GetChild(5).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(6).gameObject.SetActive(false);
            sumaryDialog.transform.GetChild(1).gameObject.SetActive(false);
            LevelChose(0);
        }
        #region handle star level display

        private void DisplayLevelStar()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            int starCount;

            for (int i = 0; i < Constants.LEVEL_COUNT; i++)
            {
                if (i != 0)
                {
                    levels[i].SetActive(false);
                    starCount = ReadLevelStar(i);
                    if (starCount != 4)
                    {
                        levels[i].SetActive(true);
                        levels[i].transform.GetChild(0).GetComponent<Image>().sprite = star[starCount];
                    }
                }
                else
                {
                    levels[0].SetActive(false);
                    starCount = ReadLevelStar(0);
                    if (starCount != 4)
                    {
                        levels[i].SetActive(true);
                        levels[i].transform.GetChild(0).GetComponent<Image>().sprite = star[starCount];
                    }
                    else
                    {
                        levels[i].SetActive(true);
                        levels[i].transform.GetChild(0).GetComponent<Image>().sprite = star[0];
                    }
                }
            }

        }

        public void SaveLevelStar(int level, int starCount)
        {
            string levelStarData;
            if (!PlayerPrefs.HasKey(Strings.LEVEL_STAR_DATA))
                ResetLevelStar();
            levelStarData = PlayerPrefs.GetString(Strings.LEVEL_STAR_DATA);
            levelStarData = levelStarData.Insert(level, starCount.ToString());
            levelStarData = levelStarData.Remove(level + 1, 1);
            //check if star is greater than star count threshold => open next level
            if (starCount > 0)
            {
                levelStarData = levelStarData.Insert(level + 1, "0");
                levelStarData = levelStarData.Remove(level + 2, 1);
            }
            PlayerPrefs.SetString(Strings.LEVEL_STAR_DATA, levelStarData);
        }

        private void ResetLevelStar()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Constants.LEVEL_COUNT; i++)
            {
                sb.Append("4");
            }
            string levelStar = sb.ToString();
            PlayerPrefs.SetString(Strings.LEVEL_STAR_DATA, levelStar);
        }

        public int ReadLevelStar(int level)
        {
            string levelStarData;
            if (!PlayerPrefs.HasKey(Strings.LEVEL_STAR_DATA))
                ResetLevelStar();
            levelStarData = PlayerPrefs.GetString(Strings.LEVEL_STAR_DATA);
            return int.Parse(levelStarData[level].ToString());
        }
        #endregion

        #region Gem Manager

        private void DisplayGem()
        {
            if (GameController.instance.GameState == GameStates.LevelMap)
            {
                LoadGemFromData();
                gemField.transform.GetChild(0).GetComponent<Text>().text = GameController.instance.Gem.ToString();
            }
        }

        private void LoadGemFromData()
        {
            if (PlayerPrefs.HasKey(Strings.GEM_COUNT))
            {
                GameController.instance.Gem = PlayerPrefs.GetInt(Strings.GEM_COUNT) + DailyBonusGem();
            }
            else
            {
                GameController.instance.Gem = 50;
                SaveGemToData();
            }
        }

        private int DailyBonusGem()
        {
            int i = -1;
            DateTime now = new DateTime();
            if (PlayerPrefs.HasKey(Strings.LAST_PLAYED_DAY))
            {
                i = PlayerPrefs.GetInt(Strings.LAST_PLAYED_DAY);
                PlayerPrefs.SetInt(Strings.LAST_PLAYED_DAY, now.DayOfYear);
                if (i < now.DayOfYear)
                {
                    GemAlert(Strings.DAILY_GEM_BONUS_MESSAGE);
                    return Constants.GEM_REWARD;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //i = now.DayOfYear;
                GemAlert(Strings.DAILY_GEM_BONUS_MESSAGE);
                PlayerPrefs.SetInt(Strings.LAST_PLAYED_DAY, now.DayOfYear);
                return Constants.GEM_REWARD;
            }
        }

        public void SaveGemToData()
        {
            PlayerPrefs.SetInt(Strings.GEM_COUNT, GameController.instance.Gem);
        }

        public void GemAlert(string message)
        {
            gemAlert.SetActive(true);
            gemAlert.GetComponentInChildren<Text>().text = message;
        }
        #endregion

        public void DisplayCanvasAsync(float f)
        {
            if (f < 0.99f)
            {
                if (!CanvasAsync.activeInHierarchy) CanvasAsync.SetActive(true);
                CanvasAsync.transform.GetChild(2).GetChild(0).GetComponent<Image>().fillAmount = f;
                CanvasAsync.transform.GetChild(3).GetComponent<Text>().text = (int)(f * 100) + " %";
            }
            else
            {
                CanvasAsync.SetActive(false);
            }
        }

        public void DisableAllCanvasElement()
        {
            if (waveReward != null)
            {
                waveReward.SetActive(false);
                
            }

            
            if (timeCountDownBanner != null)
                timeCountDownBanner.SetActive(false);
        }
    }
}
