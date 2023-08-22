using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキンの所持情報を保持するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "ObtainedSkinDatabase", menuName = "Create Obtained Skin Database")]
public class PossessionSkinDatabase : ScriptableObject
{
    public List<SkinData> possSkins = new List<SkinData>();

    // 所持しているか確認する関数
    public SkinData FindSkin(int skinID)
    {
        return possSkins.Find(skin => skin.skinID == skinID);
    }

    // 新しいスキンを入手した時に追加保存する関数
    public void AddObtainedSkin(SkinData skin)
    {
        possSkins.Add(skin);
    }

    // 所持スキンデータを削除する(初回起動時に使用)
    public void ClearPossessionSkins()
    {
        possSkins.Clear();
    }

    // 所持スキンデータを保存
    public void SavePossessionSkins()
    {
        // possSkinsリストのデータをJson形式で保存
        string jsonData = JsonUtility.ToJson(this);
        Debug.Log(jsonData);
        PlayerPrefs.SetString("PossessionSkins", jsonData);
        PlayerPrefs.Save();
    }

    // 所持スキンデータを復元
    public void LoadPossessionSkins()
    {
        if (PlayerPrefs.HasKey("PossessionSkins"))
        {
            // 保存されたデータを復元
            string jsonData = PlayerPrefs.GetString("PossessionSkins");
            Debug.Log(jsonData);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }
}