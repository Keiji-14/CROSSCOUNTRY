using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���H���Ǘ�����N���X
/// </summary>
public class RoadController : MonoBehaviour
{
    public float carSpeed;

    [Header("SpeedRange")]
    [SerializeField] float minCarSpeed;
    [SerializeField] float maxCarSpeed;
    [Header("CarType")]
    [SerializeField] GameObject[] carTypeObj;

    void Start()
    {
        var randNum = Random.Range(0, carTypeObj.Length);
        carTypeObj[randNum].SetActive(true);

        SetCarSpeed();
    }

    // �Ԃ̑��鑬�x��ݒ肷�鏈��
    private void SetCarSpeed()
    {
        // �����Ă��鑬�x��0.04f����-0.04f�͈̔͂Ɋ܂܂Ȃ������_���l�Őݒ�
        // �i�����Ă��鑬�x���ɒ[�ɒx���Ȃ�Ȃ��l�ɒ����j
        do
        {
            carSpeed = Random.Range(minCarSpeed, maxCarSpeed);
        }
        while (carSpeed <= 0.04f && carSpeed >= -0.04f);
    }
}
