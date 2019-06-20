using UnityEngine;
using System.Collections;
using TowerDefend;
public class LoadLevel : MonoBehaviour
{
    public void LoadLvl(int i)
    {
        if (i == 0) GameController.instance.GameState = GameStates.Menu;
        else
            if (i == 1) GameController.instance.GameState = GameStates.LevelMap;
            else
                if (i < 6)
                    GameController.instance.GameState = GameStates.Upgrade;
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
}
