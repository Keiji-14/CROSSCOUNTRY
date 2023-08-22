using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R�C�����Ǘ�����N���X
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

    // �R�C���������������������ɏ�������ۑ����鎞�Ɏd�l
    public static void SavePossCoin()
    {
        PlayerPrefs.SetInt("COIN", CoinController.possCoin);
        PlayerPrefs.Save();
    }
}
