using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveStageDeta : MonoBehaviour
{
    public List<string> dateList = new List<string>();

    public void Add(string json)
    {
        Debug.Log(json);
        dateList.Add(json);
    }


    public void Save()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string folderPath = Path.Combine(Application.dataPath, "StageDataJson");
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, sceneName + ".json");

        // dateList をそのまま使う（重複追加しない）
        string inner = string.Join(",", dateList);
        string fileContent = "{\n  \"Gameobj\": [\n" + inner + "\n  ]\n}";

        try
        {
            File.WriteAllText(filePath, fileContent, new UTF8Encoding(false));
            Debug.Log($"Saved stage data to: {filePath} (count={dateList.Count})");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save stage data: {ex.Message}");
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
                save.Save();
            }
        }
    }

}
