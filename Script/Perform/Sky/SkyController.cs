using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ãÛÇÃéÌóﬁ
public enum SkyTime
{
    Midday,
    Evening,
    Night
}

/// <summary>
/// ãÛÇä«óùÇ∑ÇÈÉNÉâÉX
/// </summary>
public class SkyController : MonoBehaviour
{
    public bool isTrunOnLight;
    private bool isLerp;
    private float lerpTimer;
    private float lerpDuration = 1.5f;
    private float rotationRepeatValue;

    [SerializeField] float rotateSpeed;
    [SerializeField] float timeCount;
    [SerializeField] float changeTime;
    [SerializeField] float changeSpeed;
    [Header("Enum")]
    [HideInInspector] public SkyTime skyTime;
    [Header("SkyBox")]
    [SerializeField] Material sky;
    [Header("MiddaySky")]
    [SerializeField] Color middaySkyColor;
    [SerializeField] Color middayLightColor;
    [Header("EveningSky")]
    [SerializeField] Color eveningSkyColor;
    [SerializeField] Color eveningLightColor;
    [Header("NightSky")]
    [SerializeField] Color nightSkyColor;
    [SerializeField] Color nightLightColor;
    [Header("Component")]
    [SerializeField] Light light;

    private void Start()
    {
        sky.SetColor("_Tint", middaySkyColor);
    }

    void FixedUpdate()
    {
        timeCount += Time.deltaTime;

        ChangeSky();
        ChangeLight();
        
        switch (skyTime)
        {
            case SkyTime.Midday:
                Midday();
                break;
            case SkyTime.Evening:
                Evening();
                break;
            case SkyTime.Night:
                Night();
                break;
        }

        rotationRepeatValue = Mathf.Repeat(sky.GetFloat("_Rotation") + rotateSpeed, 360f);
        sky.SetFloat("_Rotation", rotationRepeatValue);
    }

    private void ChangeSky()
    {
        if (!isLerp)
            return;

        lerpTimer += Time.deltaTime;
        float t = Mathf.Clamp01(lerpTimer / lerpDuration);

        Color currentColor;

        switch (skyTime)
        {
            case SkyTime.Midday:
                currentColor = Color.Lerp(nightSkyColor, middaySkyColor, t);
                sky.SetColor("_Tint", currentColor);
                break;
            case SkyTime.Evening:
                currentColor = Color.Lerp(middaySkyColor, eveningSkyColor, t);
                sky.SetColor("_Tint", currentColor);
                break;
            case SkyTime.Night:
                currentColor = Color.Lerp(eveningSkyColor, nightSkyColor, t);
                sky.SetColor("_Tint", currentColor);
                break;
        }

        if (t >= 1f)
        {
            lerpTimer = 0f;
            isLerp = false;
        }
    }

    private void ChangeLight()
    {
        // ê›íËÇµÇΩéûä‘ñàÇ…êÿÇËë÷Ç¶ÇÈ
        if (timeCount >= changeTime)
        {
            timeCount = 0.0f;

            switch (skyTime)
            {
                case SkyTime.Midday:
                    isLerp = true;
                    skyTime = SkyTime.Evening;
                    changeTime = 10;
                    break;
                case SkyTime.Evening:
                    isLerp = true;
                    skyTime = SkyTime.Night;
                    changeTime = 30;
                    break;
                case SkyTime.Night:
                    isLerp = true;
                    skyTime = SkyTime.Midday;
                    break;
            }
        }
    }

    private void Midday()
    {
        light.color = Color.Lerp(light.color, middayLightColor, changeSpeed * Time.deltaTime);
        isTrunOnLight = false;
    }

    private void Evening()
    {
        light.color = Color.Lerp(light.color, eveningLightColor, changeSpeed * Time.deltaTime);
        isTrunOnLight = true;
    }

    private void Night()
    {
        light.color = Color.Lerp(light.color, nightLightColor, changeSpeed * Time.deltaTime);
    }
}
