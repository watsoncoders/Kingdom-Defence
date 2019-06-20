using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using TowerDefend;
using System.Text;
using Assets.Scripts;

public class UpgradeDetailHandle : MonoBehaviour
{

    #region UpgradeDescription
    private string[] upgradeName ={
                                               "Starting Gold",
                                              "Bonus Gold",
                                              "Meteor Shower Effect Area",
                                              "Meteor Shower Damage",
                                              "Freeze Duration",
                                              "Void Duration",
                                              "Fast Cooldown",
                                              "Starting Mana",
                                              "Build Cost",
                                              "Attack Speed",
                                              "Double Damage",
                                              "Attack Range",
                                              "Max Damage",
                                              "Attack Damage",
                                              "Attack Speed",
                                              "Mini Stun",
                                              "Attack Speed",
                                              "Attack Damage",
                                              "Build Cost",
                                              "Double Arrow",
                                              "Chaos Damage",
                                              "Attack Range",
                                              "Attack Damage",
                                              "United we Stand"
                                           };

    private string[] upgradeDescription ={"Increases starting gold by 20%",
                                                      "Earns more 5% gold when you successfully stop each enemy",
                                                      "Meteor shower set ground on fire for 5 seconds, burning enemies walking over it ",
                                                      "Increases 50% meteor shower’s damage ",

                                                      "Increases duration of freezing skill by 4 seconds",
                                                 "Increases duration of void skill by 4 seconds",
                                                 "Reduces cooldown of  void skill and freezing skill by 50%",
                                                 "Increases the max of your mana by 50%",
                                                 "Reduces basic tower’s costs by 30%",
                                                 "Increases basic tower ‘s attack rate by 20%",
                                                 "Basic tower have 15% chance to do double damage ",
                                                 "Increases basic tower’s attack range by 50%",
                                                 "Canon have 15% chance of dealing max damage ",
                                                 "Increases canon tower’s attack damage by 15%",
                                                 "Reduces canon tower’ special abilities reload times by 15% (attack rate increase 15%)",
                                                 "Canon towers have a chance of stunning their target on every attack (15% stunning enemies for 1s)",
                                                 "Increases marksmen attack rate by 15%",
                                                 "Increases marksmen attack damage by 15%",
                                                 "Reduces archer tower’s costs by 15%",
                                                 "Marksmen have 10% chance of shooting 2 arrows at the same time",
                                                 "Mage tower’s bolts have 25% chance to ignore magic resistance (flying unit)",
                                                 "Increases mage tower’s attack range by 30%",
                                                 "Increases mage tower’s attack damage by 10%",
                                                 "For every other mage tower built, each mage tower get a bonus to damage (3% per mage tower)"
                                                 };

    #endregion

    public GameObject upgradeDetail, currentStarBanner, alertUpgrade;
    public GameObject[] upgradeIconBtn;
    public Sprite[] upgradeIcon, upgradeIconAlt;
    private int currentUpgradeItem = -1;
    private int[] upgradeCost ={Constants.UPGRADE_STARTING_GOLD_COST,
                                  Constants.UPGRADE_BONUS_GOLD_COST,
                                  Constants.UPGRADE_METEOR_AREA_COST,
                                  Constants.UPGRADE_METEOR_DAMAGE_COST,
                                  Constants.UPGRADE_FREEZE_DURATION_COST,
                                  Constants.UPGRADE_VOID_DURATION_COST,
                                  Constants.UPGRADE_FAST_COOLDOWN_COST,
                                  Constants.UPGRADE_STARTING_MANA_COST,
                                  Constants.UPGRADE_BASIC_COST_COST,
                                  Constants.UPGRADE_BASIC_ATTACK_SPEED_COST,
                                  Constants.UPGRADE_BASIC_DOUBLE_DAMAGE_COST,
                                  Constants.UPGRADE_BASIC_RANGE_COST,
                                  Constants.UPGRADE_CANON_MAX_DAMAGE_COST,
                                  Constants.UPGRADE_CANON_ATTACK_DAMAGE_COST,
                                  Constants.UPGRADE_CANON_ATTACK_SPEED_COST,
                                  Constants.UPGRADE_CANON_MINI_STUN_COST,
                                  Constants.UPGRADE_ARCHER_ATTACK_SPEED_COST,
                                  Constants.UPGRADE_ARCHER_ATTACK_DAMAGE_COST,
                                  Constants.UPGRADE_ARCHER_COST_COST,
                                  Constants.UPGRADE_ARCHER_DOUBLE_ARROW_COST,
                                  Constants.UPGRADE_MAGIC_CHAOS_DAMAGE_COST,
                                  Constants.UPGRADE_MAGIC_RANGE_COST,
                                  Constants.UPGRADE_MAGIC_DAMAGE_COST,
                                  Constants.UPGRADE_MAGIC_UNITED_COST
                              };

    void Start()
    {
        currentUpgradeItem = 0;
        for (int i = 0; i < upgradeIcon.Length; i++)
        {
            if (UpgradeCheck(i)) upgradeIconBtn[i].GetComponent<Image>().sprite = upgradeIconAlt[i];
        }
        currentStarBanner.GetComponentInChildren<Text>().text = StarRemain().ToString();

    }
    public void Click(int i)
    {
        AudioManager.Instance.PlaySound(Constants.BUTTON_CLICK);
        currentUpgradeItem = i;
        if (UpgradeCheck(i))
        {
            upgradeIconBtn[i].GetComponent<Image>().sprite = upgradeIconAlt[i];
            upgradeDetail.transform.GetChild(0).GetComponent<Image>().sprite = upgradeIconAlt[i];
            upgradeDetail.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            upgradeDetail.transform.GetChild(3).gameObject.SetActive(true);
            upgradeDetail.transform.GetChild(0).GetComponent<Image>().sprite = upgradeIcon[i];
        }
        upgradeDetail.transform.GetChild(1).GetComponent<Text>().text = upgradeName[i];
        upgradeDetail.transform.GetChild(2).GetComponent<Text>().text = upgradeDescription[i];
        upgradeDetail.transform.GetChild(4).GetComponentInChildren<Text>().text = upgradeCost[i].ToString();
        currentStarBanner.GetComponentInChildren<Text>().text = StarRemain().ToString();
    }

    private bool UpgradeCheck(int j)
    {
        if (j == -1) return true;
        string upgradeData;
        if (PlayerPrefs.HasKey(Strings.UPGRADE_DATA)) upgradeData = PlayerPrefs.GetString(Strings.UPGRADE_DATA);
        else
        {
            upgradeData = ResetUpgrade();
        }
        if (upgradeData[j] == '1') return true;
        return false;
    }

    public void BuyUpgrade()
    {
        string upgradeData;
        if (PlayerPrefs.HasKey(Strings.UPGRADE_DATA)) upgradeData = PlayerPrefs.GetString(Strings.UPGRADE_DATA);
        else
        {
            upgradeData = ResetUpgrade();
        }

        //check star
        if (StarRemain() > upgradeCost[currentUpgradeItem] &&
            ((UpgradeCheck(currentUpgradeItem - 1) || currentUpgradeItem % 4 == 0) && !UpgradeCheck(currentUpgradeItem)))
        {
            AudioManager.Instance.PlaySound(Constants.UPGRADE_SUCCESSFULL);
            upgradeData = upgradeData.Insert(currentUpgradeItem, "1");
            upgradeData = upgradeData.Remove(currentUpgradeItem + 1, 1);
            UpgradeMessage(Strings.UPGRADE_SUCESSFULS);
            currentStarBanner.GetComponentInChildren<Text>().text = StarRemain().ToString();
        }
        else

            //alert missing star
            if (StarRemain() < upgradeCost[currentUpgradeItem])
                UpgradeMessage(Strings.NOT_ENOUGH_STAR);
            else
                if (!UpgradeCheck(currentUpgradeItem - 1))
                    UpgradeMessage(Strings.REQUIRED_UPGRADE_PREVIOUS_ITEM_FIRST);
        PlayerPrefs.SetString(Strings.UPGRADE_DATA, upgradeData);
        UpgradedItemEffect();
        Click(currentUpgradeItem);
    }

    public string ResetUpgrade()
    {

        //return star
        string upgradeData;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Constants.NUMBER_OF_UPGRADE; i++)
        {
            sb.Append("0");
        }
        upgradeData = sb.ToString();
        PlayerPrefs.SetString(Strings.UPGRADE_DATA, upgradeData);
        UpgradedItemEffect();
        return upgradeData;
    }

    public void ResetUpgradeNoReturn()
    {
        string upgradeData;
        AudioManager.Instance.PlaySound(Constants.BUTTON_CLICK);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < upgradeIcon.Length; i++)
        {
            sb.Append("0");
        }
        upgradeData = sb.ToString();
        PlayerPrefs.SetString(Strings.UPGRADE_DATA, upgradeData);
        for (int i = 0; i < upgradeIcon.Length; i++)
        {
            if (!UpgradeCheck(i)) upgradeIconBtn[i].GetComponent<Image>().sprite = upgradeIcon[i];
        }
        currentStarBanner.GetComponentInChildren<Text>().text = StarRemain().ToString();
        UpgradedItemEffect();
    }

    private int AllUpgradeCost()
    {
        int allUpgradeCost = 0;
        for (int i = 0; i < upgradeIcon.Length; i++)
        {
            if (UpgradeCheck(i))
                allUpgradeCost += upgradeCost[i];
        }
        return allUpgradeCost;
    }

    private int StarRemain()
    {
        int starCount = TotalStarGained() - AllUpgradeCost();
        //if (PlayerPrefs.HasKey(Strings.STAR_COUNT)) starCount = PlayerPrefs.GetInt(Strings.STAR_COUNT);
        //else
        //{
        //    SaveGainStar(-1);
        //}
        return starCount;
        //return 100;
    }


    private int TotalStarGained()
    {
        string levelStarData;
        if (!PlayerPrefs.HasKey(Strings.LEVEL_STAR_DATA))
            return 0;
        levelStarData = PlayerPrefs.GetString(Strings.LEVEL_STAR_DATA);
        int starTotal = 0;
        for (int i = 0; i < Constants.LEVEL_COUNT; i++)
        {
            int j = int.Parse(levelStarData[i].ToString());
            if (j != 4) starTotal += j;
        }
        //Debug.Log(starTotal);
        return starTotal;
    }

    private void SaveGainStar(int i)
    {
        if (i == -1)
        {
            PlayerPrefs.SetInt(Strings.STAR_COUNT, 0);
        }
        else
        {
            int j = PlayerPrefs.GetInt(Strings.STAR_COUNT);
            PlayerPrefs.SetInt(Strings.STAR_COUNT, j + i);
        }
    }

    private void UpgradeMessage(string message)
    {
        alertUpgrade.SetActive(true);
        alertUpgrade.GetComponentInChildren<Text>().text = message;
    }

    public void UpgradedItemEffect()
    {
        GameController.instance.UpgradedItem = new bool[24];
        for (int i = 0; i < 24; i++)
        {
            GameController.instance.UpgradedItem[i] = UpgradeCheck(i) ? true : false;
        }
    }
}
