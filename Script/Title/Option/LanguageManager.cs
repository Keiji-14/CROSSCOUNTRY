using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����̎��
public enum Language
{
    Japanese,
    English
}

/// <summary>
/// ����ݒ���Ǘ�����N���X
/// </summary>
public class LanguageManager : MonoBehaviour
{
    private const string firstLanguageKey = "IsLanguageTime";
    public static Language language;

    [SerializeField] Image japaneseBtnImg;
    [SerializeField] Image englishBtnImg;
    [Header("Sprite")]
    [SerializeField] Sprite onBtnImg;
    [SerializeField] Sprite offBtnImg;

    void Start()
    {
        if (PlayerPrefs.HasKey(firstLanguageKey))
        {
            // �ۑ�����Ă��錾���ǂݍ���
            int savedLanguage = PlayerPrefs.GetInt("LANGUAGE", 0);
            language = (Language)savedLanguage;
        }
        else
        {
            // ����N�����A�܂��ۑ�����Ă��Ȃ��ꍇ�̃f�t�H���g����
            PlayerPrefs.SetInt(firstLanguageKey, 1);
            Language defaultLanguage = Language.Japanese;
            SaveLanguage(defaultLanguage);
        }
    }

    void Update()
    {
        // �ݒ肵�Ă��錾��̃{�^����_��������
        switch (language)
        {
            case Language.Japanese:
                japaneseBtnImg.sprite = onBtnImg;
                englishBtnImg.sprite = offBtnImg;
                break;
            case Language.English:
                englishBtnImg.sprite = onBtnImg;
                japaneseBtnImg.sprite = offBtnImg;
                break;
        }
    }

    // �w�肵������̐ݒ��ۑ�����
    public void SaveLanguage(Language language)
    {
        int languageValue = (int)language;
        PlayerPrefs.SetInt("LANGUAGE", languageValue);
        PlayerPrefs.Save();
    }

    // ���{��ɐ؂�ւ�
    public void SwitchToJapan()
    {
        language = Language.Japanese;
        SaveLanguage(language);
    }

    // �p��ɐ؂�ւ�
    public void SwitchToEnglish()
    {
        language = Language.English;
        SaveLanguage(language);
    }
}
