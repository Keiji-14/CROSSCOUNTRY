using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 川を管理するクラス
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

    // 丸太の流れる速度を設定する処理
    private void SetLogSpeed()
    {
        // 丸太の流れる速度を0.04fから-0.04fの範囲に含まないランダム値で設定
        // （流れる速度が極端に遅くならない様に調整）
        do
        {
            logSpeed = Random.Range(minLogSpeed, maxLogSpeed);
        }
        while (logSpeed <= 0.04f && logSpeed >= -0.04f);
    }
}
