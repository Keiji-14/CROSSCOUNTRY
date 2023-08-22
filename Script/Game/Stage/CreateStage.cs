using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�������s���N���X
/// </summary>
public class CreateStage : MonoBehaviour
{
    // �X�e�[�W�̌�����X�e�[�W���쐬
#if false

    private const float createPosX = 0;             // X���W�̐����ʒu
    private const float createPosY = -1;            // Y���W�̐����ʒu
    private const float createPosZ = 2;             // Z���W�̐����ʒu
    private const float startCorrectionPosZ = 4;    // �J�n����Z���W�̐����ʒu��␳�l
    private const float correctionPosZ = 52;        // �Q�[������Z���W�̐����ʒu��␳�l

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

    // �Z�߂��X�e�[�W����X�e�[�W�쐬
    //#if false
    private const float createPosX = 0;             // X���W�̐����ʒu
    private const float createPosY = -1;            // Y���W�̐����ʒu
    private const float createPosZ = 2;             // Z���W�̐����ʒu
    private const float startCorrectionPosZ = 4;    // �J�n����Z���W�̐����ʒu��␳�l
    private const float correctionPosZ = 44;        // �Q�[������Z���W�̐����ʒu��␳�l
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

    // �^�O����Ή������I�u�W�F�N�g����Component���擾
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

    // �Q�[���J�n���Ɉ��ʂ̃X�e�[�W���쐬
    private void CreateStartStage()
    {
        // �ݒ肵���X�L���̃X�e�[�W�^�C�v�ɍ��킹���X�e�[�W���쐬����
        switch (skinController.skinTypeNum)
        {
            case (int)SkinStageType.Forest:
                Instantiate(startForestStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6�X�e�[�W���𐶐�����
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, forestStageObj.Length);
                    Instantiate(forestStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
            case (int)SkinStageType.Snow:
                Instantiate(startSnowStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6�X�e�[�W���𐶐�����
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, snowStageObj.Length);
                    Instantiate(snowStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
            case (int)SkinStageType.Desert:
                Instantiate(startDesertStageObj, new Vector3(createPosX, createPosY, createPosZ - 2.0f), Quaternion.identity, stageParent);
                // 6�X�e�[�W���𐶐�����
                for (int i = 0; i < 6; i++)
                {
                    stageNum = Random.Range(0, desertStageObj.Length);
                    Instantiate(desertStageObj[stageNum], new Vector3(createPosX, createPosY, (i * 5.0f * createPosZ) + startCorrectionPosZ), Quaternion.identity, stageParent);
                }
                break;
        }
    }

    // �v���C���[������̈ʒu�𒴂�����ǉ��ō쐬����
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
