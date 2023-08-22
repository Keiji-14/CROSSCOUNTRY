using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 丸太の種類
enum TreeLogType
{
    TwoSquare,
    ThreeSquare
}

/// <summary>
/// 川に流れるオブジェクトを管理するクラス
/// </summary>
public class RiverObjectController : MonoBehaviour
{
    private const float loopPos = 40.0f;        // オブジェクトのループする位置

    public int logTypeNum;
    private bool isPlayerRide;
    private bool isRightDirection;
    private bool isLeftDirection;
    private float sinkY = 0.5f;
    private float setBasePosY;

    [Header("Component")]
    [SerializeField] RiverController riverController;

    void Start()
    {
        // プレイヤーが乗った時に浮き沈みする為に元のY座標に沈む分の数値を代入
        setBasePosY = transform.position.y - sinkY;
    }

    void FixedUpdate()
    {
        transform.position += transform.right * riverController.logSpeed;

        if (isPlayerRide)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, setBasePosY - sinkY, transform.position.z), 0.1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, setBasePosY + sinkY, transform.position.z), 0.1f);
        }

        /// 定位置まで沈んだらisPlayerRaidをfalseにして浮き上がらせる
        if (transform.position.y == setBasePosY - sinkY)
        {
            isPlayerRide = false;
        }

        Loop();
    }

    // 定位置になれば位置を一周させる
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRide = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerRide = false;
        }
    }
}
