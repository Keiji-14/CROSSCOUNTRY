using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���֌W���Ǘ�����N���X
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
            // 2��ڈȍ~�̋N���͕ۑ����ꂽ�ݒ���Ăяo��
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
            // ����N���̓~���[�g�ɂ��Ȃ�
            PlayerPrefs.SetInt(firstMuteKey, 1);
            PlayerPrefs.SetInt("ONMUTE", 0);
            PlayerPrefs.Save();

            isMute = false;
        }
    }

    // BGM��炷����
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }
   
    // SE��炷�����i�{�^���ɂȂǁj
    public void PlaySE2D(AudioClip seClip)
    {
        seSource2D.PlayOneShot(seClip);
    }

    // SE��炷�����i�v���C���[�ɉe��������ʉ��Ȃǁj
    public void PlaySE3D(AudioClip seClip)
    {
        seSource3D.PlayOneShot(seClip);
    }

    // �ʏ펞�̃s�b�`
    public void DefaultPicht()
    {
        seSource3D.pitch = 2;
    }

    // �؂ɏ�鎞�̃s�b�`
    public void LowPicht()
    {
        seSource3D.pitch = 1;
    }

    // BGM���~�߂鏈��
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
