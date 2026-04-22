using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////オブジェクト固有のデータをまとめる構造体
//[SerializeField, Serializable]
//public struct BoxData
//{
//    public int hp;
//    public int maxHp;
//}

//基底データとオブジェクト固有のデータをまとめる構造体
[SerializeField, Serializable]
public struct BoxSerializeData
{
    public StageObjectDefaultData baseData;
    //public BoxData boxData;
}


public class Box : StageObjectBase
{
    //// 固有のプロパティ（Inspectorで設定可能）
    //public int boxHp = 1;
    //public int boxMaxHp = 1;

    // Start で自動的にシリアライズして jsonString を作る例
    void Start()
    {
        RegisterSerialize();
    }

    // StageObjectBase の抽象メソッドを実装
    public override void RegisterSerialize()
    {
        // 基底情報を取得（型名を明示したいなら引数で渡す）
        StageObjectDefaultData baseData = PrepareBaseData("Box");

        //// 箱固有データを作る
        //BoxData extra = new BoxData
        //{
        //    hp = boxHp,
        //    maxHp = boxMaxHp
        //};

        // 保存用データにまとめる
        BoxSerializeData save = new BoxSerializeData
        {
            baseData = baseData,
            //boxData = extra
        };

        // 基底のヘルパーで JSON にする（jsonString に入る）
        SendSavedData(save);
    }
}
