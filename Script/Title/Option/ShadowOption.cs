using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e���Ǘ�����N���X
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
            // 2��ڈȍ~�̋N����
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
            // ����N�����A�܂��ۑ�����Ă��Ȃ��ꍇ�̃f�t�H���g�ݒ�
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
            // �e����
            lightComponent.shadows = LightShadows.Hard;
        }
        else
        {
            // �e�Ȃ�
            lightComponent.shadows = LightShadows.None;
        }
    }
}
