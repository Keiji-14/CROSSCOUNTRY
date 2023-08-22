using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 言語のテキストを設定・管理するクラス
/// </summary>
[System.Serializable]
public class LanguageText : MonoBehaviour
{
    TextMeshProUGUI thisText;

    public string japaneseText;
    public string englishText;

    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        SetLanguage();
    }

    // 設定した言語を表示する
    private void SetLanguage()
    {
        switch (LanguageManager.language)
        {
            case Language.Japanese:
                thisText.text = japaneseText;
                break;
            case Language.English:
                thisText.text = englishText;
                break;
        }
    }
}
