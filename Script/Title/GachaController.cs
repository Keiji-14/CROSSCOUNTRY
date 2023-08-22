using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ガチャ機能を管理するクラス
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
            // 必要コインが満たしてない場合、ガチャを引けない様にしてコイン不足画面を表示する
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

    // タグから対応したオブジェクトからComponentを取得
    private void GetComponentObject()
    {
        audioObj = GameObject.FindWithTag("Audio");
        audioManager = audioObj.GetComponent<AudioManager>();
    }

    // ガチャを引く時に呼び出す
    public void DrawGacha()
    {
        isDrow = true;
        DrowingGacha();

        // ガチャの抽選処理
        int randomSkinNum = Random.Range(0, skinDatabase.skins.Count);/* ガチャの抽選結果 */;
        SkinData obtainedSkin = skinDatabase.skins[randomSkinNum];

        SkinData existingSkin = possSkinDatabase.FindSkin(obtainedSkin.skinID);

        ResultSkin(obtainedSkin);

        // 重複チェック
        if (existingSkin != null)
        {
            existingSkin.duplicationCount++;
        }
        else
        {
            // 手に入ったスキン情報をPossessionSkinDatabaseに追加して
            possSkinDatabase.AddObtainedSkin(obtainedSkin);
            possSkinDatabase.SavePossessionSkins();
        }

        // 必要コイン分減らして保存する
        CoinController.possCoin -= needCoinNum;
        CoinController.SavePossCoin();

        StartCoroutine(OpenGachaBox());
    }

    // ガチャ中は戻るボタンなど一部非表示にする
    private void DrowingGacha()
    {
        for (int i = 0; i < drowingHiddenObj.Length; i++)
        {
            drowingHiddenObj[i].SetActive(false);
        }
    }

    // ガチャ演出後に結果を表示する
    IEnumerator OpenGachaBox()
    {
        yield return new WaitForSeconds(1.0f);
        gachaAnim.SetTrigger("open");
        audioManager.PlaySE2D(gachaSE);
        yield return new WaitForSeconds(2.0f);
        gachaResultWindow.SetActive(true);
    }

    // ガチャ結果を表示
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

    // ガチャ結果にある戻るボタン
    public void CloseResultWindow()
    {
        StartCoroutine(CloseGachaBox());
    }

    // 一部非表示にしていたオブジェクトを表示する
    private void DrowFinishGacha()
    {
        for (int i = 0; i < drowingHiddenObj.Length; i++)
        {
            drowingHiddenObj[i].SetActive(true);
        }
    }

    // ガチャ終了演出を見せた後にガチャ画面に戻す
    IEnumerator CloseGachaBox()
    {
        gachaResultWindow.SetActive(false);
        gachaAnim.SetTrigger("close");
        yield return new WaitForSeconds(2.0f);

        isDrow = false;
        DrowFinishGacha();
    }
}
