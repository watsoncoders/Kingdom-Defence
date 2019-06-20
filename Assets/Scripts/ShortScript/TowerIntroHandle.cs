using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TowerDefend;
using Assets.Scripts;
public class TowerIntroHandle : MonoBehaviour
{
    public GameObject towerInfoDialog;
    private void DisplayTowerInfo(string name, float price, float damage, float range, float speed)
    {
        towerInfoDialog.transform.GetChild(0).GetComponentInChildren<Text>().text = name;
        towerInfoDialog.transform.GetChild(1).GetComponent<Text>().text = "Damage: " + damage.ToString();
        towerInfoDialog.transform.GetChild(2).GetComponent<Text>().text = "Range: " + range.ToString();
        towerInfoDialog.transform.GetChild(3).GetComponent<Text>().text = "Speed: " + speed.ToString();
        towerInfoDialog.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void DisplayTowerInfo(string name, int price, float damage, string range, string speed, string detail)
    {
        towerInfoDialog.transform.GetChild(0).GetComponentInChildren<Text>().text = name;
        towerInfoDialog.transform.GetChild(1).GetComponent<Text>().text = "Price: " + price.ToString();
        towerInfoDialog.transform.GetChild(2).GetComponent<Text>().text = "Damage: " + damage.ToString();
        towerInfoDialog.transform.GetChild(4).GetComponent<Text>().text = "Range: " + range.ToString();
        towerInfoDialog.transform.GetChild(3).GetComponent<Text>().text = "Speed: " + speed.ToString();
        towerInfoDialog.transform.GetChild(5).gameObject.SetActive(true);
        towerInfoDialog.transform.GetChild(5).GetComponent<Text>().text = detail;
    }
    public void Click(int i)
    {

        AudioManager.Instance.PlaySound(Constants.BUTTON_CLICK);
        TowerTypes type = (TowerTypes)i;
        switch (type)
        {
            case TowerTypes.Normal:
                DisplayTowerInfo("Small Tower", Constants.NORMAL_TOWER_COST, Constants.NORMAL_TOWER_DAMAGE_MULTIPLIER * Constants.ARROW_DAMAGE, "Short", "Very Slow", Strings.NORMAL_TOWER_DETAIL);
                break;
            case TowerTypes.NormalIceLv1:
                DisplayTowerInfo("Small Ice Tower", Constants.NORMAL_TOWER_FROZEN_LEVEL1_COST, Constants.NORMAL_TOWER_DAMAGE_MULTIPLIER * Constants.ARROW_DAMAGE, "Short", "Slow", Strings.FROZENLV1_TOWER_DETAIL);
                break;
            case TowerTypes.NormalIceLv2:
                DisplayTowerInfo("Big Ice Tower", Constants.NORMAL_TOWER_FROZEN_LEVEL2_COST, Constants.NORMAL_TOWER_DAMAGE_MULTIPLIER * Constants.ARROW_DAMAGE, "Short", "Averange", Strings.FROZENLV2_TOWER_DETAIL);
                break;
            case TowerTypes.NormalFireLv1:
                DisplayTowerInfo("Small Fire Tower", Constants.NORMAL_TOWER_FIRE_LEVEL1_COST, Constants.NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL1 * Constants.ARROW_DAMAGE, "Short", "Slow", Strings.FIRELV1_TOWER_DETAIL);
                break;
            case TowerTypes.NormalFireLv2:
                DisplayTowerInfo("Big Fire Tower", Constants.NORMAL_TOWER_FIRE_LEVEL2_COST, Constants.NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL2 * Constants.ARROW_DAMAGE, "Short", "Averange", Strings.FIRELV2_TOWER_DETAIL);
                break;
            //***********normal******

            case TowerTypes.Canon:
                DisplayTowerInfo("Small Canon Tower", Constants.CANON_TOWER_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER * Constants.MORTAR_DAMAGE, "Short", "Very Slow", Strings.CANON_TOWER_DETAIL);
                break;
            case TowerTypes.CanonLv2:
                DisplayTowerInfo("Big Canon Tower", Constants.CANON_TOWER_LEVEL2_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL2 * Constants.MORTAR_DAMAGE, "Short", "Very Slow", Strings.CANONLV2_TOWER_DETAIL);
                break;
            case TowerTypes.CanonGoldLv1:
                DisplayTowerInfo("Golden Canon Tower", Constants.CANON_TOWER_LEVEL3_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL3 * Constants.MORTAR_DAMAGE, "Short", "Very Slow", Strings.GOLDEN_CANON_TOWER_DETAIL);
                break;
            case TowerTypes.CanonGoldLv2:
                DisplayTowerInfo("Super Canon Tower", Constants.CANON_TOWER_LEVEL4_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL4 * Constants.MORTAR_DAMAGE, "Short", "Slow", Strings.SUPER_CANON_TOWER_DETAIL);
                break;
            case TowerTypes.CanonBloodLv1:
                DisplayTowerInfo("Bloody Canon Tower", Constants.CANON_TOWER_LEVEL3_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL3 * Constants.MORTAR_DAMAGE, "Short", "Slow", Strings.BLOODY_CANON_TOWER_DETAIL);
                break;
            case TowerTypes.CanonBloodLv2:
                DisplayTowerInfo("Fury Canon Tower", Constants.CANON_TOWER_LEVEL4_COST, Constants.CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL4 * Constants.MORTAR_DAMAGE, "Short", "Slow", Strings.FURY_CANON_TOWER_DETAIL);
                break;
            //*********canon**************

            case TowerTypes.LongRange:
                DisplayTowerInfo("Small Archer Tower", Constants.LONG_RANGE_TOWER_COST, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER * Constants.ARROW_DAMAGE, "Long", "Fast", Strings.SMALL_ARCHER_TOWER_DETAIL);
                break;
            case TowerTypes.LongRangeLv2:
                DisplayTowerInfo("Big Archer Tower", Constants.LONG_RANGE_TOWER_COST_LEVEL2, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL2 * Constants.ARROW_DAMAGE, "Long", "Very Fast", Strings.BIG_ARCHER_TOWER_DETAIL);
                break;
            case TowerTypes.LongRangeMarkLv1:
                DisplayTowerInfo("Markman Tower", Constants.LONG_RANGE_TOWER_COST_LEVEL20, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL20 * Constants.ARROW_DAMAGE, "Long", "Very Fast", Strings.MARKMEN_TOWER_DETAIL);
                break;
            case TowerTypes.LongRangeMarkLv2:
                DisplayTowerInfo("God Tower", Constants.LONG_RANGE_TOWER_COST_LEVEL200, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL200 * Constants.ARROW_DAMAGE, "Long", "Very Fast", Strings.GOD_TOWER_DETAIL);
                break;
            case TowerTypes.LongRangeSuperLv1:
                DisplayTowerInfo("Super Archer Tower", Constants.LONG_RANGE_TOWER_COST_LEVEL21, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL21 * Constants.ARROW_DAMAGE, "Long", "Very Fast", Strings.SUPER_ARCHER_TOWER_DETAIL);
                break;
            case TowerTypes.LongRangeSuperLv2:
                DisplayTowerInfo("War Tower", Constants.LONG_RANGE_TOWER_COST_LEVEL210, Constants.LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL210 * Constants.ARROW_DAMAGE, "Long", "Very Fast", Strings.WAR_ARCHER_TOWER_DETAIL);
                break;
            //*******Longrange******
            case TowerTypes.Magic:
                DisplayTowerInfo("Small Magic Tower", Constants.MAGIC_TOWER_COST, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER * Constants.MAGIC_BALL_DAMAGE, "Short", "Averange", Strings.SMALL_MAGIC_TOWER_DETAIL);
                break;
            case TowerTypes.MagicLv2:
                DisplayTowerInfo("Big Magic Tower", Constants.MAGIC_TOWER_COST_LEVEL2, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL2 * Constants.MAGIC_BALL_DAMAGE, "Averange", "Fast", Strings.BIG_MAGIC_TOWER_DETAIL);
                break;
            case TowerTypes.MagicMysteriousLv1:
                DisplayTowerInfo("Mysterious Tower", Constants.MAGIC_TOWER_COST_LEVEL20, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL20 * Constants.MAGIC_BALL_DAMAGE, "Very Long", "Fast", Strings.MYSTERIOUS_TOWER_DETAIL);
                break;
            case TowerTypes.MagicMysteriousLv2:
                DisplayTowerInfo("Retributive Tower", Constants.MAGIC_TOWER_COST_LEVEL200, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL200 * Constants.MAGIC_BALL_DAMAGE, "Very Long", "Fast", Strings.RETRIBUTIVE_TOWER_DETAIL);
                break;
            case TowerTypes.MagicStormLv1:
                DisplayTowerInfo("Stormy Tower", Constants.MAGIC_TOWER_COST_LEVEL21, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL21 * Constants.MAGIC_BALL_DAMAGE, "Averange", "Fast", Strings.STORMY_DETAIL);
                break;
            case TowerTypes.MagicStormLv2:
                DisplayTowerInfo("War God Tower", Constants.MAGIC_TOWER_COST_LEVEL210, Constants.MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL210 * Constants.MAGIC_BALL_DAMAGE, "Averange", "Fast", Strings.WAR_GOD_TOWER_DETAIL);
                break;
            //********Magic**************
            case TowerTypes.Soul:
                DisplayTowerInfo("Soul Tower", Constants.SOUL_TOWER_COST, 0, "Short", "Averange", Strings.SOUL_TOWER_DETAIL);
                break;
            case TowerTypes.SoulLv2:
                DisplayTowerInfo("Dark Soul Tower", Constants.SOUL_TOWER_COST_LEVEL2, 0, "Averange", "Averange", Strings.DARK_SOUL_TOWER_DETAIL);
                break;
            case TowerTypes.SoulSealLv1:
                DisplayTowerInfo("Sealed Tower", Constants.SOUL_TOWER_COST_LEVEL20, 0, "Averange", "Fast", Strings.SEALED_TOWER_DETAIL);
                break;
            case TowerTypes.SoulSealLv2:
                DisplayTowerInfo("Black Sealed Tower", Constants.SOUL_TOWER_COST_LEVEL200, 0, "Averange", "Fast", Strings.BLACK_SEALED_TOWER_DETAIL);
                break;
            case TowerTypes.SoulFrozenLv1:
                DisplayTowerInfo("Frozen Tower", Constants.SOUL_TOWER_COST_LEVEL21, 0, "Averange", "Fast", Strings.FROZEN_TOWER_DETAIL);
                break;
            case TowerTypes.SoulFrozenLv2:
                DisplayTowerInfo("Big Frozen Tower", Constants.SOUL_TOWER_COST_LEVEL210, 0, "Averange", "Fast", Strings.BIG_FROZEN_TOWER_DETAIL);
                break;
            //*****************Soul***************
        }
    }
}
