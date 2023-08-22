using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デバッグ用にTimeScaleの加速・低速の設定
/// </summary>
public class DebugSupport : MonoBehaviour
{
	private float timeSpeed = 1.0f;

	// TimeScaleの設定（特定の動作まで早送りなど）
	void Update()
    {
		// hキーを押すと 0.1f 加速させる
		if (Input.GetKeyDown("h"))
		{
			timeSpeed += 0.1f;
			Debug.Log(timeSpeed);
		}
		// lキーを押すと 0.1f 加速させる
		if (Input.GetKeyDown("l"))
		{
			timeSpeed -= 0.1f;
			Debug.Log(timeSpeed);
		}

		Time.timeScale = timeSpeed;
	}
}
