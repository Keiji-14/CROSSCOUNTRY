using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードイン・フェードアウトの管理するクラス
/// </summary>
public class FadeController : MonoBehaviour
{
    private const float fadeSpeed = 20.0f;       // フェードイン・アウトの速度
    private const float fadeInPosX = -1080.0f;
    private const float fadeOutPosX = 290.0f;

    public bool isFadeIn;
    public bool isFadeOut;
    public bool isFinishFadeIn;
    public bool isFinishFadeOut;

    [SerializeField] GameObject fadeInImage;
    [SerializeField] GameObject fadeOutImage;

    void FixedUpdate()
    {
        if (isFadeIn)
        {
            FadeIn();
        }
        if (isFadeOut)
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        Vector3 targetPos = new Vector3(fadeOutPosX, fadeOutImage.transform.position.y, 0.0f);
        fadeOutImage.transform.position = Vector3.MoveTowards(fadeOutImage.transform.position, targetPos, fadeSpeed);
        // 定位置に着いたならfalseに変える
        if (fadeOutImage.transform.position == targetPos)
        {
            isFadeOut = false;
            isFinishFadeOut = true;
            //fadeOutImage.SetActive(false);
        }
    }

    public void FadeIn()
    {
        Vector3 targetPos = new Vector3(fadeInPosX, fadeInImage.transform.position.y, 0.0f);
        fadeInImage.transform.position = Vector3.MoveTowards(fadeInImage.transform.position, targetPos, fadeSpeed);

        // 定位置に着いたならfalseに変える
        if (fadeInImage.transform.position == targetPos)
        {
            isFadeIn = false;
            isFinishFadeIn = true;
            //fadeInImage.SetActive(false);
        }
    }
}
