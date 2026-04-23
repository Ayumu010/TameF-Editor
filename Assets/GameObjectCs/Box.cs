using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct BoxSerializeData
{
    public int hp;
    public int maxHp;
}

public class Box : StageObjectBase
{
    [SerializeField]
    public BoxSerializeData Data;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
       var save = PrepareBaseData<BoxSerializeData>("Box");
        save.data = Data;
        SendSavedData(save);
    }
}
