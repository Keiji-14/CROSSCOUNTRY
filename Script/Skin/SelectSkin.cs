using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �X�L���؂�ւ���ʂ��Ǘ�����N���X
/// </summary>
public class SelectSkin : MonoBehaviour
{
    private int skinPageNum;

    [SerializeField] Transform titleParform;
    [Header("Image")]
    [SerializeField] Image skinImage;
    [SerializeField] Image setSkinImage;
    [Header("TextMeshPro")]
    [SerializeField] TextMeshProUGUI skinNameText;
    [SerializeField] TextMeshProUGUI skinPageConutText;
    [Header("Component")]
    private GameObject skinObj;
    [SerializeField] SkinController skinController;
    [SerializeField] PossessionSkinDatabase possSkinDatabase;

    void Start()
    {
        skinObj = GameObject.FindWithTag("Skin");
        skinController = skinObj.GetComponent<SkinController>();

        // �^�C�g����ʂ̃v���C���[�𐶐�����
        GameObject titlePlayerObj = possSkinDatabase.possSkins[skinController.setSkinNum].titlePrefab; ;
        Instantiate(titlePlayerObj, titlePlayerObj.transform.position, titlePlayerObj.transform.rotation, titleParform);
    }

    // �X�L����ύX���A���݂̃X�L���ݒ��ۑ�����
    public void SetSkin()
    {
        skinController.setSkinNum = skinPageNum;
        PlayerPrefs.SetInt("SKIN", skinPageNum);
        skinController.skinTypeNum = (int)possSkinDatabase.possSkins[skinPageNum].skinStageType;
        PlayerPrefs.SetInt("SKINTYPE", skinController.skinTypeNum);

        // �^�C�g����ʂ̃v���C���[��V������������
        GameObject titlePlayerObj = possSkinDatabase.possSkins[skinController.setSkinNum].titlePrefab;
        Instantiate(titlePlayerObj, titlePlayerObj.transform.position, titlePlayerObj.transform.rotation, titleParform);

        SetSkinIcon();
    }

    // ���݁A�I�����Ă���X�L����Ԃ�\������
    public void SetSkinIcon()
    {
        setSkinImage.sprite = possSkinDatabase.possSkins[skinController.setSkinNum].icon;
    }

    // ���̃y�[�W�ɐ؂�ւ���
    public void NextSkinPage()
    {
        skinPageNum++;
        // ��Ԍ��̃y�[�W�̎����������ԑO�̃y�[�W�ɐ؂�ւ���
        if (skinPageNum >= possSkinDatabase.possSkins.Count)
        {
            skinPageNum = 0;
        }

        SelectSkinPage();
    }

    // �O�̃y�[�W�ɐ؂�ւ���
    public void BackSkinPage()
    {
        skinPageNum--;
        // ��ԑO�̃y�[�W�̎���������Ō�̃y�[�W�ɐ؂�ւ���
        if (skinPageNum < 0)
        {
            skinPageNum = possSkinDatabase.possSkins.Count - 1;
        }

        SelectSkinPage();
    }

    // ���݂̃y�[�W�̃X�L����\������
    public void SelectSkinPage()
    {
        // ���݂̃y�[�W�̃X�L������ݒ�
        switch (LanguageManager.language)
        {
            case Language.Japanese:
                skinNameText.text = possSkinDatabase.possSkins[skinPageNum].japaneseSkinName;
                break;
            case Language.English:
                skinNameText.text = possSkinDatabase.possSkins[skinPageNum].englishSkinName;
                break;
        }
        
        // ���݂̃y�[�W�̃X�L���A�C�R����ݒ�
        skinImage.sprite = possSkinDatabase.possSkins[skinPageNum].icon;
        // ���݂̃y�[�W����\��
        skinPageConutText.text = (skinPageNum + 1) + " / " + possSkinDatabase.possSkins.Count;
    }
}
