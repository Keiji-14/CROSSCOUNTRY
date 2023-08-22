using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの建物に関するクラス
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

    // オブジェクトの明かりを点灯
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
