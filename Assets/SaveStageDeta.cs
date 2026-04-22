using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStageDeta : MonoBehaviour
{
    public List<string> dateList =new List<string>();

    public void Add(string json)
    {
        dateList.Add(json);
        //Debug.Log(dateList[0]);
    }


    public void Save(List<string> json)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        string folderPath = Path.Combine(Application.dataPath, "StageDataJson");
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, sceneName + ".json");

        using (StreamWriter sw = new StreamWriter(filePath, false))
        {
            foreach (string line in json)
            {
                sw.WriteLine(line);
            }
        }
        dateList.Clear();
    }

    ///拡張で保存ボタンを作る
    [CustomEditor(typeof(SaveStageDeta))]
    public class SaveCSVEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // 元の Inspector を表示
            DrawDefaultInspector();

            if (GUILayout.Button("ステージをCSVで保存"))
            {
                SaveStageDeta save = (SaveStageDeta)target;
                save.Save(save.dateList);
            }
        }
    }

}
