using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���֌W�̃I�u�W�F�N�g���֌W����N���X
/// </summary>
public class RoadObjectController : MonoBehaviour
{
    private const float loopPos = 40.0f;        // �I�u�W�F�N�g�̃��[�v����ʒu

    [Header("CarLight")]
    [SerializeField] GameObject rightCarLight;
    [SerializeField] GameObject leftCarLight;
    [Header("GetComponent")]
    private GameObject lightObj;
    [SerializeField] SkyController skyController;
    [SerializeField] RoadController roadController;
    
    void Start()
    {
        lightObj = GameObject.FindWithTag("Light");
        skyController = lightObj.GetComponent<SkyController>();
    }

    void FixedUpdate()
    {
        Loop();
        CarLight();
        CarForwardDirection();
    }

    // ��ʒu�ɂȂ�Έʒu�����������
    private void Loop()
    {
        if (transform.position.x >= loopPos)
        {
            transform.position = new Vector3(-loopPos, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -loopPos)
        {
            transform.position = new Vector3(loopPos, transform.position.y, transform.position.z);
        }
    }

    // �Ԃ̃��C�g��_��
    private void CarLight()
    {
        if (rightCarLight == null)
            return;

        if (skyController.isTrunOnLight)
        {
            rightCarLight.SetActive(true);
            leftCarLight.SetActive(true);
        }
        else
        {
            rightCarLight.SetActive(false);
            leftCarLight.SetActive(false);
        }
    }

    // �������ɐi�ޏꍇ�Ȃ�Ԃ̑O�������������ɂ���
    private void CarForwardDirection()
    {
        if (roadController.carSpeed >= 0)
        {
            transform.position += transform.right * roadController.carSpeed;
        }
        else
        {
            transform.position -= transform.right * roadController.carSpeed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
