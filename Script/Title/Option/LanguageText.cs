using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ����̃e�L�X�g��ݒ�E�Ǘ�����N���X
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

    // �ݒ肵�������\������
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
