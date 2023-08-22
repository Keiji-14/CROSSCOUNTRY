using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道路を管理するクラス
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

    // 車の走る速度を設定する処理
    private void SetCarSpeed()
    {
        // 走っている速度を0.04fから-0.04fの範囲に含まないランダム値で設定
        // （走っている速度が極端に遅くならない様に調整）
        do
        {
            carSpeed = Random.Range(minCarSpeed, maxCarSpeed);
        }
        while (carSpeed <= 0.04f && carSpeed >= -0.04f);
    }
}
