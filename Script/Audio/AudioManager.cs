using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音関係を管理するクラス
/// </summary>
public class AudioManager : MonoBehaviour
{
    private const string firstMuteKey = "IsMuteTime";
    public static int isMuteNum;
    public static bool isMute;

    public AudioSource bgmSource;
    public AudioSource seSource2D;
    public AudioSource seSource3D;

    void Awake()
    {
        if (PlayerPrefs.HasKey(firstMuteKey))
        {
            // 2回目以降の起動は保存された設定を呼び出す
            isMuteNum = PlayerPrefs.GetInt("ONMUTE", 0);

            if (isMuteNum == 0)
            {
                isMute = false;
            }
            else if (isMuteNum == 1)
            {
                isMute = true;
            }
        }
        else
        {
            // 初回起動はミュートにしない
            PlayerPrefs.SetInt(firstMuteKey, 1);
            PlayerPrefs.SetInt("ONMUTE", 0);
            PlayerPrefs.Save();

            isMute = false;
        }
    }

    // BGMを鳴らす処理
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }
   
    // SEを鳴らす処理（ボタンになど）
    public void PlaySE2D(AudioClip seClip)
    {
        seSource2D.PlayOneShot(seClip);
    }

    // SEを鳴らす処理（プレイヤーに影響する効果音など）
    public void PlaySE3D(AudioClip seClip)
    {
        seSource3D.PlayOneShot(seClip);
    }

    // 通常時のピッチ
    public void DefaultPicht()
    {
        seSource3D.pitch = 2;
    }

    // 木に乗る時のピッチ
    public void LowPicht()
    {
        seSource3D.pitch = 1;
    }

    // BGMを止める処理
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
