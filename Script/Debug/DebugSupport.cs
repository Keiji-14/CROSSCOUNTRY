using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �f�o�b�O�p��TimeScale�̉����E�ᑬ�̐ݒ�
/// </summary>
public class DebugSupport : MonoBehaviour
{
	private float timeSpeed = 1.0f;

	// TimeScale�̐ݒ�i����̓���܂ő�����Ȃǁj
	void Update()
    {
		// h�L�[�������� 0.1f ����������
		if (Input.GetKeyDown("h"))
		{
			timeSpeed += 0.1f;
			Debug.Log(timeSpeed);
		}
		// l�L�[�������� 0.1f ����������
		if (Input.GetKeyDown("l"))
		{
			timeSpeed -= 0.1f;
			Debug.Log(timeSpeed);
		}

		Time.timeScale = timeSpeed;
	}
}
