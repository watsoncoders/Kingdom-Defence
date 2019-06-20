using UnityEngine;
using UnityEditor;
using System.Collections;
namespace Assets.Editor
{

    public class MapGenerate : EditorWindow
    {
        public int width=18, height=10;
        [MenuItem("CustomEditor/Map Generate")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MapGenerate));
            //generate the place holder
            //if (GameObject.Find("PlaceHolders"))
            //    DestroyImmediate(GameObject.Find("PlaceHolders").gameObject);
            //GameObject parent = new GameObject("PlaceHolders");
            //float offsetX = -8.5f, offsetY = -4.5f;
            //for (int i = 0; i < 18; i++)
            //    for (int j = 0; j < 10; j++)
            //    {
            //        GameObject go = Instantiate(Resources.Load("PlaceHolder"), new Vector3(i + offsetX, j + offsetY, 0), Quaternion.identity) as GameObject;
            //        go.transform.SetParent(parent.transform);
            //    }
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Width: ");
            width = EditorGUILayout.IntField(width);
            EditorGUILayout.LabelField("Height: ");
            height = EditorGUILayout.IntField(height);
            if (GUILayout.Button("Create"))
            {
                CreatePlaceHolder();
            }
        }

        private void CreatePlaceHolder(){
            if (GameObject.Find("PlaceHolders"))
                DestroyImmediate(GameObject.Find("PlaceHolders").gameObject);
            GameObject parent = new GameObject("PlaceHolders");
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    GameObject go = Instantiate(Resources.Load("PlaceHolder"), new Vector3(i-(int)(width/2), j-(int)(height/2)+0.5f, 0), Quaternion.identity) as GameObject;
                    go.transform.SetParent(parent.transform);
                }
        }



    }
}