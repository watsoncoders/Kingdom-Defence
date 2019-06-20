using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TowerDefend;

namespace Assets.Scripts
{
    public class AutoDisable : MonoBehaviour
    {
        public float timeDisplay = 1;

        void OnEnable()
        {
            if (GameController.instance.Sound)
            {
                if (GetComponent<AudioSource>() != null)
                    GetComponent<AudioSource>().Play();
            }
            Invoke("DisableGameObject", timeDisplay);
        }

        private void DisableGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}
