using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TowerDefend;
public class SettingCtrl : MonoBehaviour
{
    public Sprite[] checkBoxSprite;
    public GameObject bgSound = null;

    void Start()
    {
        Invoke("PlayBGSound", 0.25f);
    }

    private void PlayBGSound()
    {
        if (GameController.instance.Music)
        {
            if (bgSound == null) return;
            bgSound.GetComponent<AudioSource>().Play();
        }
    }
    public void Click()
    {
        if (GameController.instance.Sound)
            GetComponent<AudioSource>().Play();
        if (!transform.GetChild(0).gameObject.activeInHierarchy)
        {
            GetComponent<Animator>().Play("SettingDialogOpen");
            if (GameController.instance.Sound)
            {
                transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[1];
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[0];
            }

            if (GameController.instance.Music && GameController.instance.Sound)
            {
                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[1];
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[0];
            }
        }
        else
        {
            GetComponent<Animator>().Play("SettingDialogClose");
        }
    }

    public void MusicBtnClick()
    {
        GameController.instance.Music = !GameController.instance.Music;
        if (GameController.instance.Music && GameController.instance.Sound)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[1];
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[0];
        }
        if (GameController.instance.Music && GameController.instance.Sound)
        {
            if (bgSound != null)
                bgSound.GetComponent<AudioSource>().Play();
        }
        else
            if (bgSound != null)
                bgSound.GetComponent<AudioSource>().Stop();
    }

    public void SoundBtnClick()
    {
        GameController.instance.Sound = !GameController.instance.Sound;
        if (GameController.instance.Sound)
        {
            transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[1];
            if (GameController.instance.Music && GameController.instance.Sound)
                transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[1];
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[0];
            //if (GameController.instance.Music && GameController.instance.Sound)
            GameController.instance.Sound = false;
            GameController.instance.Music = false;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = checkBoxSprite[0];           
        }
        if (GameController.instance.Music && GameController.instance.Sound)
        {
            if (bgSound != null)
                bgSound.GetComponent<AudioSource>().Play();
        }
        else
            if (bgSound != null)
                bgSound.GetComponent<AudioSource>().Stop();
    }
    public void Play()
    {
        if (GameController.instance != null)
        {
            if (GameController.instance.Sound)
                GetComponent<AudioSource>().Play();
            GameController.instance.GameState = GameStates.LevelMap;
            GameController.instance.LoadLevelAsync(1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


}
