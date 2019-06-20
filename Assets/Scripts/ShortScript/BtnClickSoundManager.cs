using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefend;
using Assets.Scripts;
using UnityEngine;
namespace Assets.Scripts.ShortScript
{
    public class BtnClickSoundManager : MonoBehaviour
    {
        public bool SkillBook = false;
        private bool isOpen = false;
        public void SoundOnClick()
        {
            AudioManager.Instance.PlaySound(Constants.BUTTON_CLICK);
            if (SkillBook)
            {
                if (isOpen)
                {
                    //GetComponent<Animator>().Stop();
                    GetComponent<Animator>().Play("SkillBookClose");
                    isOpen = false;
                }
                else
                {
                    isOpen = true;
                    //GetComponent<Animator>().Stop();
                    GetComponent<Animator>().Play("SkillBook");
                }
            }
        }
    }
}
