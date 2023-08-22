using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道関係のオブジェクトを関係するクラス
/// </summary>
public class RoadObjectController : MonoBehaviour
{
    private const float loopPos = 40.0f;        // オブジェクトのループする位置

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

    // 車のライトを点灯
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

    // 左方向に進む場合なら車の前方向を左方向にする
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
