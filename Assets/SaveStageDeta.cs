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


    [Serializable]
    public class TypeOnly
    {
        public string type;
    }

    public void Save()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string folderPath = Path.Combine(Application.dataPath, "StageDataJson");
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, sceneName + ".json");

        var portItems = new List<string>();
        var otherItems = new List<string>();

        foreach (var raw in dateList)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                otherItems.Add(raw);
                continue;
            }

            try
            {
                // JsonUtility で type フィールドだけ読み取る
                var wrapper = JsonUtility.FromJson<TypeOnly>(raw);
                if (!string.IsNullOrEmpty(wrapper?.type) &&
                    wrapper.type.StartsWith("Port", StringComparison.OrdinalIgnoreCase))
                {
                    portItems.Add(raw);
                }
                else
                {
                    otherItems.Add(raw);
                }
            }
            catch (Exception)
            {
                // パースに失敗したら安全側で other に入れる
                otherItems.Add(raw);
            }
        }

        // Port群を先頭にして順序を維持
        var ordered = new List<string>(portItems.Count + otherItems.Count);
        ordered.AddRange(portItems);
        ordered.AddRange(otherItems);

        // JSON 組み立て（要素は既に JSON オブジェクト文字列である前提）
        string inner = string.Join(",\n", ordered);
        string fileContent = "{\n  \"Gameobj\": [\n" + inner + "\n  ]\n}";

        try
        {
            File.WriteAllText(filePath, fileContent, new UTF8Encoding(false));
            Debug.Log($"Saved stage data to: {filePath} (count={dateList.Count})");
            dateList.Clear(); // 成功時のみクリア
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save stage data: {ex.ToString()}");
            // 失敗時は dateList を保持しておく
        }
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
