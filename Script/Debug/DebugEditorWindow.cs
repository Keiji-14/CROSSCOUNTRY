using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// �R�C���𑦍��ɓ��肳����@�\
/// </summary>
#if true
public class DebugEditorWindow : EditorWindow
{
    [MenuItem("Debug/GetCoin")]
    private static void GetCoin()
    {
        CoinController.possCoin = PlayerPrefs.GetInt("COIN", 0); 
        CoinController.possCoin += 10;

        CoinController.SavePossCoin();
        Debug.Log("PossCoinNum:" + CoinController.possCoin);
    }
}
#endif

#if false
public class DebugEditorWindow : MonoBehaviour
{
    public void GetCoin()
    {
        CoinController.possCoin = PlayerPrefs.GetInt("COIN", 0);
        CoinController.possCoin += 10;

        CoinController.SavePossCoin();
        Debug.Log("PossCoinNum:" + CoinController.possCoin);
    }
}
#endif
