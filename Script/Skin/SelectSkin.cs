using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// スキン切り替え画面を管理するクラス
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

        // タイトル画面のプレイヤーを生成する
        GameObject titlePlayerObj = possSkinDatabase.possSkins[skinController.setSkinNum].titlePrefab; ;
        Instantiate(titlePlayerObj, titlePlayerObj.transform.position, titlePlayerObj.transform.rotation, titleParform);
    }

    // スキンを変更し、現在のスキン設定を保存する
    public void SetSkin()
    {
        skinController.setSkinNum = skinPageNum;
        PlayerPrefs.SetInt("SKIN", skinPageNum);
        skinController.skinTypeNum = (int)possSkinDatabase.possSkins[skinPageNum].skinStageType;
        PlayerPrefs.SetInt("SKINTYPE", skinController.skinTypeNum);

        // タイトル画面のプレイヤーを新しく生成する
        GameObject titlePlayerObj = possSkinDatabase.possSkins[skinController.setSkinNum].titlePrefab;
        Instantiate(titlePlayerObj, titlePlayerObj.transform.position, titlePlayerObj.transform.rotation, titleParform);

        SetSkinIcon();
    }

    // 現在、選択しているスキン状態を表示する
    public void SetSkinIcon()
    {
        setSkinImage.sprite = possSkinDatabase.possSkins[skinController.setSkinNum].icon;
    }

    // 次のページに切り替える
    public void NextSkinPage()
    {
        skinPageNum++;
        // 一番後ろのページの時だったら一番前のページに切り替える
        if (skinPageNum >= possSkinDatabase.possSkins.Count)
        {
            skinPageNum = 0;
        }

        SelectSkinPage();
    }

    // 前のページに切り替える
    public void BackSkinPage()
    {
        skinPageNum--;
        // 一番前のページの時だったら最後のページに切り替える
        if (skinPageNum < 0)
        {
            skinPageNum = possSkinDatabase.possSkins.Count - 1;
        }

        SelectSkinPage();
    }

    // 現在のページのスキンを表示する
    public void SelectSkinPage()
    {
        // 現在のページのスキン名を設定
        switch (LanguageManager.language)
        {
            case Language.Japanese:
                skinNameText.text = possSkinDatabase.possSkins[skinPageNum].japaneseSkinName;
                break;
            case Language.English:
                skinNameText.text = possSkinDatabase.possSkins[skinPageNum].englishSkinName;
                break;
        }
        
        // 現在のページのスキンアイコンを設定
        skinImage.sprite = possSkinDatabase.possSkins[skinPageNum].icon;
        // 現在のページ数を表示
        skinPageConutText.text = (skinPageNum + 1) + " / " + possSkinDatabase.possSkins.Count;
    }
}
