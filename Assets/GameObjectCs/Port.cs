using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct PortObjectDate
{
    public int PortID;
}

public class Port : StageObjectBase
{
    [SerializeField]
    public PortObjectDate Data;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
        var save = PrepareBaseData<PortObjectDate>("Port");
        save.childObjectData = Data;
        SendSavedData(save);
    }
}
