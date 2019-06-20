using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections;
using SimpleJSON;
using TowerDefend;

namespace Assets.Editor
{
    class ReadWaveAttributes : EditorWindow
    {
        [MenuItem("CustomEditor/Read Wave Attribute")]
        public static void ShowWindow()
        {
            //EditorWindow.GetWindow(typeof(ReadWaveJson));
            OpenFile();

        }

        void OnGUI()
        {
            //EditorGUILayout.LabelField("Width: ");
            //width = EditorGUILayout.IntField(width);
            //EditorGUILayout.LabelField("Height: ");
            //height = EditorGUILayout.IntField(height);
            //if (GUILayout.Button("Create"))
            //{
            //    CreatePlaceHolder();
            //}
            if (GUILayout.Button("Open File"))
            {
                OpenFile();
            }

        }

        static void OpenFile()
        {
            var path = EditorUtility.OpenFilePanel("Wave", @"D:\libgdx\Code(Qplay channel) 20140318\Code(Qplay channel)\BaoVeThanhDia-android\assets\data\PlayingScreenData", "txt");
            if (path == null) return;
            string text = System.IO.File.ReadAllText(path);
            ParseJsonToAttribute(text);
        }

        static void ParseJsonToAttribute(string text) {
            WaveManager waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            JSONNode root = JSON.Parse(text);
            waveManager.maxHealth = root["startHealth"].AsInt;
            waveManager.startingGold = root["coin"].AsInt;
            waveManager.Block=root["obstacleAmount"].AsInt;
            waveManager.starThreshold = new int[3];
            waveManager.maxTower = root["maxTowerAmount"].AsInt;
            JSONArray arrTmp = root["roundConditionToGetStars"].AsArray;
            waveManager.starThreshold[0] = arrTmp[0].AsInt;
            waveManager.starThreshold[1] = arrTmp[1].AsInt;
            waveManager.starThreshold[2] = arrTmp[2].AsInt;
        }
    }
}
