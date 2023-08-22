using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトル画面のプレイヤーを管理するクラス
/// </summary>
public class TitlePlayer : MonoBehaviour
{
    private static TitlePlayer instance;
    private float countTime = 1.0f;

    Rigidbody rb;

    // 生成された時に同じものがあった場合古いを削除する
    // (スキン変更時にタイトル画面のプレイヤーの見た目を変更するため)
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        countTime -= Time.deltaTime;

        // 一定間隔で跳ねる
        if (countTime <= 0)
        {
            TitlePlayerMove();
            countTime = 1.0f;
        }
    }

    private void TitlePlayerMove()
    {
        rb.AddForce(transform.up * 15.0f, ForceMode.VelocityChange);
    }
}
