using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct TrapDoorAxisDescDate
{
    public Vector3 InitialRotation;
    public MoveAxis Axis;
    public float Speed;
    public int PortID;
}

public class TrapDoorAxisDesc : StageObjectBase
{
    [SerializeField]
    public TrapDoorAxisDescDate Data;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
       var save = PrepareBaseData<TrapDoorAxisDescDate>("TrapDoorAxis");
        save.childObjectData = Data;
        SendSavedData(save);
    }
}
