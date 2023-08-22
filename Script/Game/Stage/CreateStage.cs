using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成を行うクラス
/// </summary>
public class CreateStage : MonoBehaviour
{
    // ステージの元からステージを作成
#if false

    private const float createPosX = 0;             // X座標の生成位置
    private const float createPosY = -1;            // Y座標の生成位置
    private const float createPosZ = 2;             // Z座標の生成位置
    private const float startCorrectionPosZ = 4;    // 開始時のZ座標の生成位置を補正値
    private const float correctionPosZ = 52;        // ゲーム中のZ座標の生成位置を補正値

    private int countNum = 10;
    private int stageNum;
    private StageType stageType;

    [SerializeField] Transform stageParent;
    [SerializeField] GameObject[] stageObjects;

    [SerializeField] Player player;

    enum StageType
    {
        Grasslands,
        Graund,
        Road,
        River
    }

    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            stageNum = Random.Range(0, stageObjects.Length);
            switch (stageNum)
            {
                case (int)StageType.Grasslands:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, (i * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.Graund:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, (i * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.Road:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, transform.position.y, (i * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.River:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, (i * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                    break;
            }
        }
    }

    void Update()
    {
        if (player.transform.position.z > countNum)
        {
            countNum += 2;
            stageNum = Random.Range(0, stageObjects.Length);
            switch (stageNum)
            {
                case (int)StageType.Grasslands:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.Graund:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.Road:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, transform.position.y, countNum + correctionPosZ), Quaternion.identity, stageParent);
                    break;
                case (int)StageType.River:
                    Instantiate(stageObjects[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                    break;
            }
        }
    }

#endif

    // 纏めたステージからステージ作成
    //#if false
    private const float createPosX = 0;             // X座標の生成位置
    private const float createPosY = -1;            // Y座標の生成位置
    private const float createPosZ = 2;             // Z座標の生成位置
    private const float startCorrectionPosZ = 4;    // 開始時のZ座標の生成位置を補正値
    private const float correctionPosZ = 44;        // ゲーム中のZ座標の生成位置を補正値
    private int countNum = 10;
    private int stageNum;

    [Header("Stage")]
    [SerializeField] Transform stageParent;
    [Header("ForestStage")]
    [SerializeField] GameObject startForestStageObj;
    [SerializeField] GameObject[] forestStageObj;
    [Header("SnowStage")]
    [SerializeField] GameObject startSnowStageObj;
    [SerializeField] GameObject[] snowStageObj;
    [Header("DesertStage")]
    [SerializeField] GameObject startDesertStageObj;
    [SerializeField] GameObject[] desertStageObj;
    [Header("Component")]
    private GameObject playerObj;
    [SerializeField] Player player;
    private GameObject skinObj;
    [SerializeField] SkinController skinController;

    void Start()
    {
        GetComponentObject();
        CreateStartStage();
    }

    void Update()
    {
        if (playerObj.transform.position.z > countNum)
        {
            CreateAddStage();
        }
    }

    // タグから対応したオブジェクトからComponentを取得
    private void GetComponentObject()
    {
        if (player == null)
        {
            playerObj = GameObject.FindWithTag("Player");
            player = playerObj.GetComponent<Player>();
        }

        if (skinController == null)
        {
            skinObj = GameObject.FindWithTag("Skin");
            skinController = skinObj.GetComponent<SkinController>();
        }
    }

    // ゲーム開始時に一定量のステージを作成
    private void CreateStartStage()
    {
        // 設定したスキンのステージタイプに合わせたステージを作成する
        switch (skinController.skinTypeNum)
        {
            case (int)SkinStageType.Forest:
                Instantiate(startForestStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6ステージ分を生成する
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, forestStageObj.Length);
                    Instantiate(forestStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
            case (int)SkinStageType.Snow:
                Instantiate(startSnowStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6ステージ分を生成する
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, snowStageObj.Length);
                    Instantiate(snowStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
            case (int)SkinStageType.Desert:
                Instantiate(startDesertStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6ステージ分を生成する
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, desertStageObj.Length);
                    Instantiate(desertStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
        }
    }

    // プレイヤーが特定の位置を超えたら追加で作成する
    private void CreateAddStage()
    {
        switch (skinController.skinTypeNum)
        {
            case (int)SkinStageType.Forest:
                countNum += 10;
                stageNum = Random.Range(0, forestStageObj.Length);
                Instantiate(forestStageObj[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                break;
            case (int)SkinStageType.Snow:
                countNum += 10;
                stageNum = Random.Range(0, snowStageObj.Length);
                Instantiate(snowStageObj[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                break;
            case (int)SkinStageType.Desert:
                countNum += 10;
                stageNum = Random.Range(0, desertStageObj.Length);
                Instantiate(desertStageObj[stageNum], new Vector3(createPosX, createPosY, countNum + correctionPosZ), Quaternion.identity, stageParent);
                break;
        }
    }
    //#endif
}
