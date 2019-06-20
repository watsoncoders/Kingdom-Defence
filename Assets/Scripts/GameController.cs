using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace TowerDefend
{
    public class GameController : MonoBehaviour
    {
        [HideInInspector]
        public static GameController instance { get; private set; }
        private List<Action> subscribers = new List<Action>();
        private int gem;

        public int Gem
        {
            get { return gem; }
            set
            {
                gem = value;
                buttonManager.SaveGemToData();
            }
        }

        public WaveManager waveManager;
        public ButtonManager buttonManager;
        public GameObject tmpTextGold;
        public bool[] UpgradedItem;
        [HideInInspector]
        public bool Sound = false, Music = false;

        private GameStates gameState;

        public GameStates GameState
        {
            get { return gameState; }
            set
            {
                gameState = value;
                buttonManager.LoadingLevel = true;
            }
        }

        public void OnApplicationQuit()
        {
            SaveSoundState();
            
        }

        void Start()
        {
            //Money = 1000;
            gameState = GameStates.Menu;
            SoundManager();
			GoogleAdmob.RequestBanner();
			GoogleAdmob.RequestInterstitial();
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                (new UpgradeDetailHandle()).UpgradedItemEffect();
            }
            else
                DestroyImmediate(gameObject);
        }

        //Update is called once per frame
        void Update()
        {
        }

        void FixedUpdate()
        {
            switch (gameState)
            {
                case GameStates.Menu:
				if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Show();
                    break;
                case GameStates.LevelMap:
				if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Show();
                    break;
                case GameStates.Playing:
				if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Hide();
                    break;
                case GameStates.End:
				if (GoogleAdmob.bannerView!=null) GoogleAdmob.bannerView.Show();
                    break;
            }
        }
        #region Subcribe
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
        #endregion

        public void Pause()
        {
            if (gameState == GameStates.Pause)
            {
                gameState = GameStates.Playing;
            }
            else
            {
				if (GoogleAdmob.interstitial.IsLoaded())
					GoogleAdmob.ShowInterstitial();
                gameState = GameStates.Pause;
            }
            //Debug.Log("Broadcast requested, No of Subscribers = " + subscribers.Count);
            foreach (var subscriber in subscribers)
            {
                subscriber();
            }
        }

        public bool IsPause()
        {
            return gameState == GameStates.Pause ? true : false;
        }

        public void DecreaseHealth()
        {
            waveManager.Health--;
        }

        private void SaveSoundState(SoundState state)
        {
            PlayerPrefs.SetInt(Strings.SOUND_SETTING, (int)state);
        }
        private void SaveSoundState()
        {
            if (Sound && Music) SaveSoundState(SoundState.All);
            else
                if (Sound) SaveSoundState(SoundState.SoundOn);
                else
                    if (Music) SaveSoundState(SoundState.MuteAll);
                    else SaveSoundState(SoundState.MuteAll);

        }
        private SoundState LoadSoundState()
        {
            if (PlayerPrefs.HasKey(Strings.SOUND_SETTING))
            {
                return (SoundState)PlayerPrefs.GetInt(Strings.SOUND_SETTING);
            }
            else
            {
                //Reset sound state.
                SaveSoundState(SoundState.All);
                return SoundState.All;
            }
        }

        private void SoundManager()
        {
            SoundState st = LoadSoundState();

            switch (st)
            {
                case SoundState.MuteAll: Sound = false; Music = false; break;
                case SoundState.SoundOn: Sound = true; Music = false; break;
                case SoundState.MusicOn: Sound = false; Music = false; break;
                case SoundState.All: Sound = true; Music = true; break;
            }

        }

        #region LoadLevelAsync
        public void LoadLevelAsync(string level)
        {
            StartCoroutine(LoadAsync(level));
        }

        private IEnumerator LoadAsync(string level)
        {
            AsyncOperation async = Application.LoadLevelAsync(level);
            while (async.isDone)
            {
                yield return new WaitForSeconds(0.05f);
                buttonManager.DisplayCanvasAsync(async.progress);
            }
            yield return new WaitForSeconds(0.2f);
        }

        public void LoadLevelAsync(int level)
        {
            StartCoroutine(LoadAsync(level));
        }

        private IEnumerator LoadAsync(int level)
        {
            //yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(FadeIn());
            buttonManager.DisplayCanvasAsync(0f);
            yield return StartCoroutine(FadeOut());
            AsyncOperation async = Application.LoadLevelAsync(level);
            while (!async.isDone)
            {
                buttonManager.DisplayCanvasAsync(async.progress);
                yield return new WaitForSeconds(0.01f);
            }
            buttonManager.DisplayCanvasAsync(1);
            //yield return StartCoroutine(FadeIn());
            //yield return StartCoroutine(FadeOut());
            //yield return StartCoroutine(Fade());
            //yield return new WaitForSeconds(0.1f);
        }

        private IEnumerator FadeIn()
        {
            //Setup a default blank texture for fading if none is supplied
            float fadeOutTime = 0.5f, fadeInTime = 0.5f;
            Color fadeColor = Color.black;

            //Material fadeMaterial = new Material("Shader \"Plane/No zTest\" {" +
            //"SubShader { Pass { " +
            //"    Blend SrcAlpha OneMinusSrcAlpha " +
            //"    ZWrite Off Cull Off Fog { Mode Off } " +
            //"    BindChannels {" +
            //"      Bind \"color\", color }" +
            //"} } }");

            float t = 0.0f;
            while (t < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                t = Mathf.Clamp01(t + Time.deltaTime / fadeOutTime);
                DrawingUtilities.DrawQuad(/*fadeMaterial, */fadeColor, t);
            }

            //while (t > 0.0f)
            //{
            //    yield return new WaitForEndOfFrame();
            //    t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
            //    DrawingUtilities.DrawQuad(/*fadeMaterial,*/ fadeColor, t);
            //}
        }

        private IEnumerator FadeOut()
        {
            //Setup a default blank texture for fading if none is supplied
            float fadeOutTime = 0.5f, fadeInTime = 0.5f;
            Color fadeColor = Color.black;

            //Material fadeMaterial = new Material("Shader \"Plane/No zTest\" {" +
            //"SubShader { Pass { " +
            //"    Blend SrcAlpha OneMinusSrcAlpha " +
            //"    ZWrite Off Cull Off Fog { Mode Off } " +
            //"    BindChannels {" +
            //"      Bind \"color\", color }" +
            //"} } }");

            float t = 1f;
            //while (t < 1.0f)
            //{
            //    yield return new WaitForEndOfFrame();
            //    t = Mathf.Clamp01(t + Time.deltaTime / fadeOutTime);
            //    DrawingUtilities.DrawQuad(/*fadeMaterial, */fadeColor, t);
            //}

            while (t > 0.0f)
            {
                yield return new WaitForEndOfFrame();
                t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
                DrawingUtilities.DrawQuad(/*fadeMaterial,*/ fadeColor, t);
            }
        }
        private static class DrawingUtilities
        {
            //Helper utility to draw a full screen texture
            public static void DrawQuad(/*Material aMaterial, */Color aColor, float aAlpha)
            {
                aColor.a = aAlpha;
                var shader = Shader.Find("Hidden/Internal-Colored");
                Material mat = new Material(shader);
                mat.SetPass(0);
                GL.PushMatrix();
                GL.LoadOrtho();
                GL.Begin(GL.QUADS);
                GL.Color(aColor);
                GL.Vertex3(0, 0, -1);
                GL.Vertex3(0, 1, -1);
                GL.Vertex3(1, 1, -1);
                GL.Vertex3(1, 0, -1);
                GL.End();
                GL.PopMatrix();
            }
        }
        #endregion

        #region Ads

        #endregion

        #region Gem
        public void FreeGem()
        {
            // Add your code here.
            GameController.instance.Gem += Constants.GEM_REWARD;
        }

        public void FreeGem(int gem)
        {
            // Add your code here.
            GameController.instance.Gem += gem;
        }
        #endregion
    }
}