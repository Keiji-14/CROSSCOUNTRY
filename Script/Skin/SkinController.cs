using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキンを管理するクラス
/// </summary>
public class SkinController : MonoBehaviour
{
    private const string firstSkinKey = "IsSkinTime";
    private static SkinController instance;

    public int setSkinNum;
    public int skinTypeNum;
    [Header("Component")]
    [SerializeField] SkinDatabase skinDatabase;
    [SerializeField] PossessionSkinDatabase possSkinDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.HasKey(firstSkinKey))
        {
            // 二回目以降起動時
            // 設定したスキン情報を呼び出す
            setSkinNum = PlayerPrefs.GetInt("SKIN", 0);
            skinTypeNum = PlayerPrefs.GetInt("SKINTYPE", 0);
            possSkinDatabase.LoadPossessionSkins();
        }
        else
        {
            // 初回起動時
            // 初めに所持スキン情報を未所持状態にする
            possSkinDatabase.ClearPossessionSkins();
            // デフォルトスキンを入手状態にする
            PlayerPrefs.SetInt(firstSkinKey, 1);
            SkinData firestObtainedSkin = skinDatabase.skins[0];
            // 入手したスキン情報を保存する
            possSkinDatabase.AddObtainedSkin(firestObtainedSkin);
            possSkinDatabase.SavePossessionSkins();
            // スキンをデフォルトスキンに設定する
            setSkinNum = firestObtainedSkin.skinID;
            skinTypeNum = (int)firestObtainedSkin.skinStageType;
            PlayerPrefs.SetInt("SKIN", setSkinNum);
            PlayerPrefs.SetInt("SKINTYPE", skinTypeNum);
        }
    }
}
