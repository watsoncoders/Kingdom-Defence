using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using TowerDefend;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip[] sounds, musics;

        public static AudioManager Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public void PlaySound(int i)
        {
            if (GameController.instance.Sound)
            {
                StartCoroutine(PlaySound(sounds[i]));
            }
        }

        public void PlayMusic(int i)
        {
            if (GameController.instance.Music)
                StartCoroutine(PlaySound(musics[i]));
        }

        private IEnumerator PlayMusic(AudioClip clip)
        {
            GameObject go = ObjectPoolerManager.Instance.audioPooler.GetPooledObject();
            go.SetActive(true);
            go.GetComponent<AudioSource>().clip = clip;
            go.GetComponent<AudioSource>().loop = true;
            go.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(clip.length);
        }

        private IEnumerator PlaySound(AudioClip clip)
        {
            GameObject go = ObjectPoolerManager.Instance.audioPooler.GetPooledObject();
            go.transform.position = Vector3.zero;
            go.SetActive(true);
            go.GetComponent<AudioSource>().clip = clip;
            go.GetComponent<AudioSource>().priority = 64;
            go.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(clip.length);
            go.SetActive(false);
        }
    }
}
