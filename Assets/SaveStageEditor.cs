using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.Json;

public class SaveStageEditor
{
    [MenuItem("Tools/ステージをJsonで保存")]
    public static void SaveStage()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SaveStageDeta save = Object.FindAnyObjectByType<SaveStageDeta>();
        if(!save)
        {
            Debug.LogError("SaveStageDeta がシーンにありません");
            return;
        }
        // ★ シーン内の全 StageObjectBase を探す
        StageObjectBase[] objects = Object.FindObjectsOfType<StageObjectBase>();

        foreach (var obj in objects)
        {
            // ★ Editor でも RegisterSerialize を呼ぶ
            obj.RegisterSerialize();
        }

        SaveStageDeta saveData = Object.FindObjectOfType<SaveStageDeta>();
        saveData.Save();

        Debug.Log($"ステージデータを保存しました");
    }
}
