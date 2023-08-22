using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキンの情報を管理するクラス
/// </summary>
[System.Serializable]
public class SkinData
{
    public int skinID;
    public string japaneseSkinName;
    public string englishSkinName;
    public Sprite icon;
    public GameObject gamePrefab;
    public GameObject titlePrefab;
    public SkinStageType skinStageType;
    public int duplicationCount;
}
 
// スキンのステージタイプに次第でステージが切り替える
public enum SkinStageType
{
    Forest,
    Snow,
    Desert,
}