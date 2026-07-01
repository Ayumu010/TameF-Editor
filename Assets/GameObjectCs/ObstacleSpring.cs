using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct ObstacleSpringDate
{
}

public class ObstacleSpring : StageObjectBase
{
    [SerializeField]
    public ObstacleSpringDate Data;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
       var save = PrepareBaseData<ObstacleSpringDate>("Spring");
        save.childObjectData = Data;
        SendSavedData(save);
    }
}
