using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct StageObjectDefaultData<T>
{
    public string type;
    public Vector3 position;
    public Vector3 scale;
    public Vector3 rotationEuler;
    public T data;
}

public abstract class StageObjectBase : MonoBehaviour
{
    //public string jsonString { get; protected set; }

    public abstract void RegisterSerialize();

    protected StageObjectDefaultData<T> PrepareBaseData<T>(string typeName = null) where T : new()
    {
        StageObjectDefaultData<T> d = new StageObjectDefaultData<T>();
        d.type = string.IsNullOrEmpty(typeName) ? gameObject.name : typeName;
        d.position = transform.position;
        d.scale = transform.localScale;
        d.rotationEuler = transform.eulerAngles;
        return d;
    }

    protected void SendSavedData<T>(T data)
    {
        SaveStageDeta save = FindObjectOfType<SaveStageDeta>();

        save.Add(JsonUtility.ToJson(data, true));
    }
}
