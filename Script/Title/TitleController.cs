using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// タイトル画面に表示する項目
enum TitleWindow
{
    Title,
    Skin,
    Gacha,
    Option,
    Language,
    Credit
}

/// <summary>
/// タイトル画面の管理
/// </summary>
public class TitleController : MonoBehaviour
{
    public bool isPressBtn;
    private TitleWindow titleWindow;

    [Header("ButtonImage")]
    [SerializeField] Image onMuteBtnImg;
    [SerializeField] Image onShadowBtnImg;
    [Header("Sprite")]
    [SerializeField] Sprite onBtnImg;
    [SerializeField] Sprite offBtnImg;
    [Header("SkinCanvas")]
    [SerializeField] GameObject[] titlePlayer;
    [Header("SkinCanvas")]
    [SerializeField] GameObject skinCanvas;
    [SerializeField] GameObject skinWindow;
    [SerializeField] GameObject gachaWindow;
    [Header("OptionCanvas")]
    [SerializeField] GameObject optionCanvas;
    [SerializeField] GameObject optionWindow;
    [SerializeField] GameObject languageWindow;
    [SerializeField] GameObject creditWindow;
    [Header("Component")]
    [SerializeField] FadeController fadeController;
    [SerializeField] SelectSkin selectSkin;

    void Start()
    {
        fadeController.isFadeIn = true;

        // タイトル画面開始時に保存している設定を呼び出す
        SetMuteOptionBtn();
        SetShadowOptionBtn();
    }

    void Update()
    {
        // ボタンが押されるまでは処理は通さない
        if (!isPressBtn)
            return;

        // タイトル画面の表示項目の切り替え
        switch (titleWindow)
        {
            case TitleWindow.Title:
                skinCanvas.SetActive(false);
                skinWindow.SetActive(false);
                optionCanvas.SetActive(false);
                optionWindow.SetActive(false);
                break;
            case TitleWindow.Skin:
                skinCanvas.SetActive(true);
                skinWindow.SetActive(true);
                gachaWindow.SetActive(false);
                break;
            case TitleWindow.Gacha:
                skinWindow.SetActive(false);
                gachaWindow.SetActive(true);
                break;
            case TitleWindow.Option:
                optionCanvas.SetActive(true);
                optionWindow.SetActive(true);
                languageWindow.SetActive(false);
                creditWindow.SetActive(false);
                break;
            case TitleWindow.Language:
                optionWindow.SetActive(false);
                languageWindow.SetActive(true);
                break;
            case TitleWindow.Credit:
                optionWindow.SetActive(false);
                creditWindow.SetActive(true);
                break;
        }

        isPressBtn = false;
    }

    // スキン画面の表示する関数
    public void OpenSkin()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Skin;

        selectSkin.SetSkinIcon();
        selectSkin.SelectSkinPage();
    }

    // ガチャ画面の表示する関数
    public void OpenGacha()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Gacha;
    }

    // オプション画面の表示する関数
    public void OpenOption()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Option;
    }

    // 言語切り替え画面の表示する関数
    public void OpenLanguag()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Language;
    }

    // クレジット画面の表示する関数
    public void OpenCredit()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Credit;
    }

    // オプション画面を閉じる関数
    public void TitleBack()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Title;
    }

    // オプション画面のミュートボタンの画像切り替え処理
    public void SetMuteOptionBtn()
    {
        if (AudioManager.isMute)
        {
            onMuteBtnImg.sprite = onBtnImg;
        }
        else
        {
            onMuteBtnImg.sprite = offBtnImg;
        }
    }

    // オプション画面の影切り替えボタンの画像切り替え処理
    public void SetShadowOptionBtn()
    {
        if (ShadowOption.isOnShadow)
        {
            onShadowBtnImg.sprite = offBtnImg;
        }
        else
        {
            onShadowBtnImg.sprite = onBtnImg;
        }
    }

    // オプション画面でミュート切り替えの処理
    public void SwitchToMute()
    {
        var trueNum = 1;
        var falseNum = 0;
        if (AudioManager.isMute)
        {
            AudioManager.isMute = false;
            PlayerPrefs.SetInt("ONMUTE", falseNum);
        }
        else
        {
            AudioManager.isMute = true;
            PlayerPrefs.SetInt("ONMUTE", trueNum);
        }
        SetMuteOptionBtn();
    }

    // オプション画面で影の表示非表示の切り替え処理
    public void SwitchToShadowOption()
    {
        var trueNum = 1;
        var falseNum = 0;
        if (ShadowOption.isOnShadow)
        {
            ShadowOption.isOnShadow = false;
            PlayerPrefs.SetInt("ONSHADOW", falseNum);
        }
        else
        {
            ShadowOption.isOnShadow = true;
            PlayerPrefs.SetInt("ONSHADOW", trueNum);
        }
        SetShadowOptionBtn();
    }
}
