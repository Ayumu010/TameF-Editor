using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAxis
{
    X,
    Y,
    Z
}

//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct MoveFloorObjectDate
{
    public MoveAxis Axis;
    public float Speed;
    public float LimitDistance;
    public int PortID;
}

public class MoveFloor : StageObjectBase
{
    [SerializeField]
    public MoveFloorObjectDate Data;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
        var save = PrepareBaseData<MoveFloorObjectDate>("MoveFloor");
        save.childObjectData = Data;
        SendSavedData(save);
    }
}
