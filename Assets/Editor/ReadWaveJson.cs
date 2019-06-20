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
    class ReadWaveJson : EditorWindow
    {
        [MenuItem("CustomEditor/Read Wave Json")]
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
            string text = System.IO.File.ReadAllText(path);
            ParseJsonToWaves(text);
        }

        static void ParseJsonToWaves(string text)
        {
            WaveManager waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            JSONNode root = JSON.Parse(text);
            JSONNode roundList = root["roundList"];
            waveManager.waves = new TowerDefend.Wave[roundList.Count];
            for (int i = 0; i < roundList.Count; i++)
            {
                //JSONNode wave = roundList[i]["waveList"];
                waveManager.waves[i] = ParseToWave(roundList[i]);
                //for (int j = 0; j < wave.Count; j++) {
                //    ParseToEnemyType(wave[j]);
                //}
            }


        }

        static TowerDefend.Wave ParseToWave(JSONNode node)
        {
            JSONNode _wave = node["waveList"];
            TowerDefend.Wave wave = new TowerDefend.Wave();
            wave.enemyTypes = new TowerDefend.EnemyType[_wave.Count];
            for (int j = 0; j < _wave.Count; j++)
            {
                wave.enemyTypes[j]=ParseToEnemyType(_wave[j]);
            }
            return wave;

        }
        static EnemyType ParseToEnemyType(JSONNode node)
        {
            EnemyType enemyType = new EnemyType();
            enemyType.eType = (EnemyTypes)(node["enemyType"].AsInt-1);
            enemyType.maxEnemies = node["enemyTotal"].AsInt;
            enemyType.spawnInterval = node["spawnRate"].AsFloat;
            enemyType.firstSpawnTime = node["delayStart"].AsFloat;
            return enemyType;
        }
    }
}
