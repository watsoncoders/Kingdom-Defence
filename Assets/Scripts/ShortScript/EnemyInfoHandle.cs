using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TowerDefend;
using Assets.Scripts;
public class EnemyInfoHandle : MonoBehaviour
{
    public GameObject enemyInfoDialog;
    private void DisplayEnemyInfo(string name, float health, float armor, float speed)
    {
        enemyInfoDialog.transform.GetChild(0).GetComponentInChildren<Text>().text = name;
        enemyInfoDialog.transform.GetChild(1).GetComponent<Text>().text = "Health: " + health.ToString();
        enemyInfoDialog.transform.GetChild(2).GetComponent<Text>().text = "Armor: " + armor.ToString();
        enemyInfoDialog.transform.GetChild(3).GetComponent<Text>().text = "Speed: " + speed.ToString();
        enemyInfoDialog.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void DisplayEnemyInfo(string name, float health, float armor, float speed, string skill)
    {
        enemyInfoDialog.transform.GetChild(0).GetComponentInChildren<Text>().text = name;
        enemyInfoDialog.transform.GetChild(1).GetComponent<Text>().text = "Health: " + health.ToString();
        enemyInfoDialog.transform.GetChild(2).GetComponent<Text>().text = "Armor: " + armor.ToString();
        enemyInfoDialog.transform.GetChild(3).GetComponent<Text>().text = "Speed: " + speed.ToString();
        enemyInfoDialog.transform.GetChild(3).gameObject.SetActive(true);
        enemyInfoDialog.transform.GetChild(4).GetComponent<Text>().text = skill;
    }
    public void Click(int i)
    {
        AudioManager.Instance.PlaySound(Constants.BUTTON_CLICK);
        EnemyTypes type = (EnemyTypes)i;
        switch (type)
        {
            case EnemyTypes.Runner:
                DisplayEnemyInfo("Runner", Constants.HEATH_RUNNER, Constants.ARMOR_RUNNER, Constants.SPEED_RUNNER);
                break;
            case EnemyTypes.DarkWarrior:
                DisplayEnemyInfo("DarkWarrior", Constants.HEATH_DARKWARRIOR, Constants.ARMOR_DARKWARRIOR, Constants.SPEED_DARKWARRIOR);
                break;
            case EnemyTypes.Butcher:
                DisplayEnemyInfo("Butcher", Constants.HEATH_BUTCHER, Constants.ARMOR_BUTCHER, Constants.SPEED_BUTCHER);
                break;
            case EnemyTypes.Ghost:
                DisplayEnemyInfo("Ghost", Constants.HEATH_GHOST, Constants.ARMOR_GHOST, Constants.SPEED_GHOST, "Invisible 3 seconds after being attacked");
                break;
            case EnemyTypes.Hellguarder:
                DisplayEnemyInfo("Hellguarder", Constants.HEATH_HELLGUARDER, Constants.ARMOR_HELLGUARDER, Constants.SPEED_HELLGUARDER, "Explode upon dead");
                break;
            case EnemyTypes.Darkwizard:
                DisplayEnemyInfo("DarkWizard", Constants.HEATH_DARKWIZARD, Constants.ARMOR_DARKWIZARD, Constants.SPEED_DARKWIZARD, "Immune every types of spell");
                break;
            case EnemyTypes.Necromancer:
                DisplayEnemyInfo("Necromancer", Constants.HEATH_NECROMANCER, Constants.ARMOR_NECROMANCER, Constants.SPEED_NECROMANCER, "Has a second chance to revive");
                break;
            case EnemyTypes.UndeadWorm:
                DisplayEnemyInfo("UndeadWorm", Constants.HEATH_UNDEADWORM, Constants.ARMOR_UNDEADWORM, Constants.SPEED_UNDEADWORM);
                break;
            case EnemyTypes.Shadow:
                break;
            case EnemyTypes.Glutondragon:
                break;
            case EnemyTypes.Wyvern:
                break;
            case EnemyTypes.Carrier:
                DisplayEnemyInfo("Carrier", Constants.HEATH_CARRIER, Constants.ARMOR_CARRIER, Constants.SPEED_CARRIER, "Spawn another flying friends every 3 seconds");
                break;
            case EnemyTypes.Tyrantdragon:
                DisplayEnemyInfo("Tyrantdragon", Constants.HEATH_TYRANTDRAGON, Constants.ARMOR_TYRANTDRAGON, Constants.SPEED_TYRANTDRAGON, "Explode upon dead");
                break;
            case EnemyTypes.Nightmareshipper:
                DisplayEnemyInfo("Nightmareshipper", Constants.HEATH_NIGHTMARESHIPPER, Constants.ARMOR_NIGHTMARESHIPPER, Constants.SPEED_NIGHTMARESHIPPER, "Can dodge an attack");
                break;
            case EnemyTypes.Immortaldevil:
                DisplayEnemyInfo("Immortaldevil", Constants.HEATH_IMMORTALDEVIL, Constants.ARMOR_IMMORTALDEVIL, Constants.SPEED_IMMORTALDEVIL, "Spawns Undead Worm");
                break;
        }
    }
}
