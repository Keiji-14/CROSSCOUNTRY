using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインを管理するクラス
/// </summary>
public class CoinController : MonoBehaviour
{
    public static int possCoin;
    private static CoinController instance;

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
    }

    void Start()
    {
        possCoin = PlayerPrefs.GetInt("COIN", 0);
    }

    // コイン所持数が増減した時に所持数を保存する時に仕様
    public static void SavePossCoin()
    {
        PlayerPrefs.SetInt("COIN", CoinController.possCoin);
        PlayerPrefs.Save();
    }
}
