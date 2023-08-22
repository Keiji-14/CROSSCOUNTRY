using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �K�`���@�\���Ǘ�����N���X
/// </summary>
public class GachaController : MonoBehaviour
{
    private const int needCoinNum = 10;

    private bool isDrow;
    [Header("GameObject")]
    [SerializeField] GameObject lackWindow;
    [SerializeField] GameObject gachaResultWindow;
    [SerializeField] GameObject[] drowingHiddenObj;
    [Header("Button")]
    [SerializeField] Button DrawGachaBtn;
    [Header("Image")]
    [SerializeField] Image resultSkinImage;
    [Header("TextMeshPro")]
    [SerializeField] TextMeshProUGUI possCoinText;
    [SerializeField] TextMeshProUGUI resultSkinNameText;
    [Header("Animator")]
    [SerializeField] Animator gachaAnim;
    [Header("AudioClip")]
    [SerializeField] AudioClip gachaSE;
    [Header("Component")]
    private GameObject audioObj;
    [SerializeField] AudioManager audioManager;
    public SkinDatabase skinDatabase;
    public PossessionSkinDatabase possSkinDatabase;

    void Start()
    {
        GetComponentObject();
    }

    void Update()
    {
        if (!isDrow)
        {
            // �K�v�R�C�����������ĂȂ��ꍇ�A�K�`���������Ȃ��l�ɂ��ăR�C���s����ʂ�\������
            if (CoinController.possCoin >= needCoinNum)
            {
                DrawGachaBtn.interactable = true;
                lackWindow.SetActive(false);
            }
            else
            {
                DrawGachaBtn.interactable = false;
                lackWindow.SetActive(true);
            }
            gachaResultWindow.SetActive(false);
        }

        possCoinText.text = CoinController.possCoin.ToString("0");
    }

    // �^�O����Ή������I�u�W�F�N�g����Component���擾
    private void GetComponentObject()
    {
        audioObj = GameObject.FindWithTag("Audio");
        audioManager = audioObj.GetComponent<AudioManager>();
    }

    // �K�`�����������ɌĂяo��
    public void DrawGacha()
    {
        isDrow = true;
        DrowingGacha();

        // �K�`���̒��I����
        int randomSkinNum = Random.Range(0, skinDatabase.skins.Count);/* �K�`���̒��I���� */;
        SkinData obtainedSkin = skinDatabase.skins[randomSkinNum];

        SkinData existingSkin = possSkinDatabase.FindSkin(obtainedSkin.skinID);

        ResultSkin(obtainedSkin);

        // �d���`�F�b�N
        if (existingSkin != null)
        {
            existingSkin.duplicationCount++;
        }
        else
        {
            // ��ɓ������X�L������PossessionSkinDatabase�ɒǉ�����
            possSkinDatabase.AddObtainedSkin(obtainedSkin);
            possSkinDatabase.SavePossessionSkins();
        }

        // �K�v�R�C�������炵�ĕۑ�����
        CoinController.possCoin -= needCoinNum;
        CoinController.SavePossCoin();

        StartCoroutine(OpenGachaBox());
    }

    // �K�`�����͖߂�{�^���Ȃǈꕔ��\���ɂ���
    private void DrowingGacha()
    {
        for (int i = 0; i < drowingHiddenObj.Length; i++)
        {
            drowingHiddenObj[i].SetActive(false);
        }
    }

    // �K�`�����o��Ɍ��ʂ�\������
    IEnumerator OpenGachaBox()
    {
        yield return new WaitForSeconds(1.0f);
        gachaAnim.SetTrigger("open");
        audioManager.PlaySE2D(gachaSE);
        yield return new WaitForSeconds(2.0f);
        gachaResultWindow.SetActive(true);
    }

    // �K�`�����ʂ�\��
    private void ResultSkin(SkinData resultSkinData)
    {
        resultSkinImage.sprite = resultSkinData.icon;

        switch (LanguageManager.language)
        {
            case Language.Japanese:
                resultSkinNameText.text = resultSkinData.japaneseSkinName;
                break;
            case Language.English:
                resultSkinNameText.text = resultSkinData.englishSkinName;
                break;
        }
    }

    // �K�`�����ʂɂ���߂�{�^��
    public void CloseResultWindow()
    {
        StartCoroutine(CloseGachaBox());
    }

    // �ꕔ��\���ɂ��Ă����I�u�W�F�N�g��\������
    private void DrowFinishGacha()
    {
        for (int i = 0; i < drowingHiddenObj.Length; i++)
        {
            drowingHiddenObj[i].SetActive(true);
        }
    }

    // �K�`���I�����o����������ɃK�`����ʂɖ߂�
    IEnumerator CloseGachaBox()
    {
        gachaResultWindow.SetActive(false);
        gachaAnim.SetTrigger("close");
        yield return new WaitForSeconds(2.0f);

        isDrow = false;
        DrowFinishGacha();
    }
}
