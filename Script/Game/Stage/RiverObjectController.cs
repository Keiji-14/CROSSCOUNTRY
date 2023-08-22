using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ۑ��̎��
enum TreeLogType
{
    TwoSquare,
    ThreeSquare
}

/// <summary>
/// ��ɗ����I�u�W�F�N�g���Ǘ�����N���X
/// </summary>
public class RiverObjectController : MonoBehaviour
{
    private const float loopPos = 40.0f;        // �I�u�W�F�N�g�̃��[�v����ʒu

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
        // �v���C���[����������ɕ������݂���ׂɌ���Y���W�ɒ��ޕ��̐��l����
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

        /// ��ʒu�܂Œ��񂾂�isPlayerRaid��false�ɂ��ĕ����オ�点��
        if (transform.position.y == setBasePosY - sinkY)
        {
            isPlayerRide = false;
        }

        Loop();
    }

    // ��ʒu�ɂȂ�Έʒu�����������
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
