using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃~���[�g�̊Ǘ�
/// </summary>
public class AudioController : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.mute = AudioManager.isMute;
    }
}
