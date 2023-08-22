using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 言語の種類
public enum Language
{
    Japanese,
    English
}

/// <summary>
/// 言語設定を管理するクラス
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
            // 保存されている言語を読み込む
            int savedLanguage = PlayerPrefs.GetInt("LANGUAGE", 0);
            language = (Language)savedLanguage;
        }
        else
        {
            // 初回起動時、まだ保存されていない場合のデフォルト言語
            PlayerPrefs.SetInt(firstLanguageKey, 1);
            Language defaultLanguage = Language.Japanese;
            SaveLanguage(defaultLanguage);
        }
    }

    void Update()
    {
        // 設定している言語のボタンを点灯させる
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

    // 指定した言語の設定を保存する
    public void SaveLanguage(Language language)
    {
        int languageValue = (int)language;
        PlayerPrefs.SetInt("LANGUAGE", languageValue);
        PlayerPrefs.Save();
    }

    // 日本語に切り替え
    public void SwitchToJapan()
    {
        language = Language.Japanese;
        SaveLanguage(language);
    }

    // 英語に切り替え
    public void SwitchToEnglish()
    {
        language = Language.English;
        SaveLanguage(language);
    }
}
