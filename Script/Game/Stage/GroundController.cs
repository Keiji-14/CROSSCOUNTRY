using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������n�ʂɃI�u�W�F�N�g��ݒ肷��N���X
/// </summary>
public class GroundController : MonoBehaviour
{
    [SerializeField] GameObject[] typeObj;

    void Start()
    {
        var randNum = Random.Range(0, typeObj.Length);
        typeObj[randNum].SetActive(true);
    }
}
