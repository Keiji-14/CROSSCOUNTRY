using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ǘ�����N���X
/// </summary>
public class RiverController : MonoBehaviour
{
    public float logSpeed;

    [Header("SpeedRange")]
    [SerializeField] float minLogSpeed;
    [SerializeField] float maxLogSpeed;
    [Header("LogType")]
    [SerializeField] GameObject[] logTypeObj;

    void Start()
    {
        var randNum = Random.Range(0, logTypeObj.Length);
        logTypeObj[randNum].SetActive(true);

        SetLogSpeed();
    }

    // �ۑ��̗���鑬�x��ݒ肷�鏈��
    private void SetLogSpeed()
    {
        // �ۑ��̗���鑬�x��0.04f����-0.04f�͈̔͂Ɋ܂܂Ȃ������_���l�Őݒ�
        // �i����鑬�x���ɒ[�ɒx���Ȃ�Ȃ��l�ɒ����j
        do
        {
            logSpeed = Random.Range(minLogSpeed, maxLogSpeed);
        }
        while (logSpeed <= 0.04f && logSpeed >= -0.04f);
    }
}
