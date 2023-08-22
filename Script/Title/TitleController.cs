using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �^�C�g����ʂɕ\�����鍀��
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
/// �^�C�g����ʂ̊Ǘ�
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

        // �^�C�g����ʊJ�n���ɕۑ����Ă���ݒ���Ăяo��
        SetMuteOptionBtn();
        SetShadowOptionBtn();
    }

    void Update()
    {
        // �{�^�����������܂ł͏����͒ʂ��Ȃ�
        if (!isPressBtn)
            return;

        // �^�C�g����ʂ̕\�����ڂ̐؂�ւ�
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

    // �X�L����ʂ̕\������֐�
    public void OpenSkin()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Skin;

        selectSkin.SetSkinIcon();
        selectSkin.SelectSkinPage();
    }

    // �K�`����ʂ̕\������֐�
    public void OpenGacha()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Gacha;
    }

    // �I�v�V������ʂ̕\������֐�
    public void OpenOption()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Option;
    }

    // ����؂�ւ���ʂ̕\������֐�
    public void OpenLanguag()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Language;
    }

    // �N���W�b�g��ʂ̕\������֐�
    public void OpenCredit()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Credit;
    }

    // �I�v�V������ʂ����֐�
    public void TitleBack()
    {
        isPressBtn = true;
        titleWindow = TitleWindow.Title;
    }

    // �I�v�V������ʂ̃~���[�g�{�^���̉摜�؂�ւ�����
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

    // �I�v�V������ʂ̉e�؂�ւ��{�^���̉摜�؂�ւ�����
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

    // �I�v�V������ʂŃ~���[�g�؂�ւ��̏���
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

    // �I�v�V������ʂŉe�̕\����\���̐؂�ւ�����
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
