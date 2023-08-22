using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̌����Ɋւ���N���X
/// </summary>
public class StageObjectController : MonoBehaviour
{
    [SerializeField] GameObject objectLight;

    [Header("Component")]
    private GameObject lightObj;
    [SerializeField] SkyController skyController;

    void Start()
    {
        lightObj = GameObject.FindWithTag("Light");
        skyController = lightObj.GetComponent<SkyController>();
    }

    void FixedUpdate()
    {
        ObjectLight();
    }

    // �I�u�W�F�N�g�̖������_��
    private void ObjectLight()
    {
        if (objectLight == null)
            return;

        if (skyController.isTrunOnLight)
        {
            objectLight.SetActive(true);
        }
        else
        {
            objectLight.SetActive(false);
        }
    }
}
