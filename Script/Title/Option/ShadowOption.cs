using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 影を管理するクラス
/// </summary>
public class ShadowOption : MonoBehaviour
{
    private const string firstTimeKey = "IsFirstTime";

    public static int isOnShadowNum;
    public static bool isOnShadow;

    Light lightComponent;

    void Start()
    {
        if (PlayerPrefs.HasKey(firstTimeKey))
        {
            // 2回目以降の起動時
            isOnShadowNum = PlayerPrefs.GetInt("ONSHADOW", 0);

            if (isOnShadowNum == 0)
            {
                isOnShadow = false;
            }
            else if (isOnShadowNum == 1)
            {
                isOnShadow = true;
            }
        }
        else
        {
            // 初回起動時、まだ保存されていない場合のデフォルト設定
            PlayerPrefs.SetInt(firstTimeKey, 1);
            PlayerPrefs.SetInt("ONSHADOW", 1);
            PlayerPrefs.Save();

            isOnShadow = true;
        }
        
        lightComponent = GetComponent<Light>();
    }

    void Update()
    {
        if (isOnShadow)
        {
            // 影あり
            lightComponent.shadows = LightShadows.Hard;
        }
        else
        {
            // 影なし
            lightComponent.shadows = LightShadows.None;
        }
    }
}
