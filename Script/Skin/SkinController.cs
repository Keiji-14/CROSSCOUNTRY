using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�L�����Ǘ�����N���X
/// </summary>
public class SkinController : MonoBehaviour
{
    private const string firstSkinKey = "IsSkinTime";
    private static SkinController instance;

    public int setSkinNum;
    public int skinTypeNum;
    [Header("Component")]
    [SerializeField] SkinDatabase skinDatabase;
    [SerializeField] PossessionSkinDatabase possSkinDatabase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (PlayerPrefs.HasKey(firstSkinKey))
        {
            // ���ڈȍ~�N����
            // �ݒ肵���X�L�������Ăяo��
            setSkinNum = PlayerPrefs.GetInt("SKIN", 0);
            skinTypeNum = PlayerPrefs.GetInt("SKINTYPE", 0);
            possSkinDatabase.LoadPossessionSkins();
        }
        else
        {
            // ����N����
            // ���߂ɏ����X�L�����𖢏�����Ԃɂ���
            possSkinDatabase.ClearPossessionSkins();
            // �f�t�H���g�X�L��������Ԃɂ���
            PlayerPrefs.SetInt(firstSkinKey, 1);
            SkinData firestObtainedSkin = skinDatabase.skins[0];
            // ���肵���X�L������ۑ�����
            possSkinDatabase.AddObtainedSkin(firestObtainedSkin);
            possSkinDatabase.SavePossessionSkins();
            // �X�L�����f�t�H���g�X�L���ɐݒ肷��
            setSkinNum = firestObtainedSkin.skinID;
            skinTypeNum = (int)firestObtainedSkin.skinStageType;
            PlayerPrefs.SetInt("SKIN", setSkinNum);
            PlayerPrefs.SetInt("SKINTYPE", skinTypeNum);
        }
    }
}
