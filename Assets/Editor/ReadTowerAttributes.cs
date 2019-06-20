using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using TowerDefend;
using UnityEngine;
using SimpleJSON;
namespace Assets.Editor
{
    class ReadTowerAttributes:EditorWindow
    {
        [MenuItem("CustomEditor/Read Tower Attribute")]
        public static void ShowWindow()
        {
            //EditorWindow.GetWindow(typeof(ReadWaveJson));
            OpenFile();

        }

        private static void OpenFile() {
            var path = EditorUtility.OpenFilePanel("Tower", @"D:\libgdx\Code(Qplay channel) 20140318\Code(Qplay channel)\BaoVeThanhDia-android\assets\data\PlayingScreenData", "txt");
            if (path == null) return;
            string text = System.IO.File.ReadAllText(path);
            ParseJsonToTowerAttribute(text);
        }

        private static void ParseJsonToTowerAttribute(string text)
        {
            WaveManager waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
            JSONNode root = JSON.Parse(text);
            JSONArray towerAttributes = root["towerAttributes"].AsArray;
            waveManager.NormalTower = towerAttributes[0]["amount"].AsInt;
            waveManager.CanonTower = towerAttributes[1]["amount"].AsInt;
            waveManager.LongRangTower = towerAttributes[7]["amount"].AsInt;
            waveManager.MagicTower = towerAttributes[13]["amount"].AsInt;
            waveManager.SoulTower = towerAttributes[19]["amount"].AsInt;
            //for (int i = 0; i < towerAttributes.Count; i++) { 
            //normal tower
            if (towerAttributes[28]["isActive"]==null||!towerAttributes[28]["isActive"].AsBool) waveManager.NormalTowerCondition[4] = false;
            if (towerAttributes[27]["isActive"] == null || !towerAttributes[27]["isActive"].AsBool) waveManager.NormalTowerCondition[3] = false;
            if (towerAttributes[26]["isActive"] == null || !towerAttributes[26]["isActive"].AsBool) waveManager.NormalTowerCondition[2] = false;
            if (towerAttributes[25]["isActive"] == null || !towerAttributes[25]["isActive"].AsBool) waveManager.NormalTowerCondition[1] = false;
            if (towerAttributes[0]["isActive"] == null || !towerAttributes[0]["isActive"].AsBool) waveManager.NormalTowerCondition[0] = false;
            for (int i = 1; i < 25; i++)
            {
                if (towerAttributes[i]["isActive"] == null || !towerAttributes[i]["isActive"].AsBool) {
                    if (i < 7) waveManager.CanonTowerCondition[i-1] = false;
                    if (i < 13&&i>6) waveManager.LongRangeTowerCondition[i-7] = false;
                    if (i < 19&&i>12) waveManager.MagicTowerCondition[i - 13] = false;
                    if (i < 24&&i>18) waveManager.SoulTowerCondition[i - 19] = false;
                }
            }
            //}
        }
    }
}
