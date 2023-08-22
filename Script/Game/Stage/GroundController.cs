using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成した地面にオブジェクトを設定するクラス
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
