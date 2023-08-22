using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�C�g����ʂ̃v���C���[���Ǘ�����N���X
/// </summary>
public class TitlePlayer : MonoBehaviour
{
    private static TitlePlayer instance;
    private float countTime = 1.0f;

    Rigidbody rb;

    // �������ꂽ���ɓ������̂��������ꍇ�Â����폜����
    // (�X�L���ύX���Ƀ^�C�g����ʂ̃v���C���[�̌����ڂ�ύX���邽��)
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

        // ���Ԋu�Œ��˂�
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
