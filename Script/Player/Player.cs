using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���Ǘ�����N���X
/// </summary>
public class Player : MonoBehaviour
{
    // �ړ��E���B�_
    [HideInInspector] public bool isMoving;
    [HideInInspector] public float arriveCount;
    [HideInInspector] public float moveCount;
    // �Ǌ֌W
    public bool isFrontWall;
    public bool isBackWall;
    public bool isRightWall;
    public bool isLeftWall;
    // �ۑ��֌W
    private bool isRide;
    private bool isRiding;
    private bool isRideMovingRight;
    private bool isRideMovingLeft;
    // �v���C���[�̈ړ�XYZ���W
    private float playerSetPosY;
    private float playerSetPosZ;
    private float playerSetPosX;
    // ���C�̋���
    private float detectionDistance = 2.0f;
    // �v���C���[�̐ݒ���W
    private Vector3 playerMovePos;
    // �v���C���[���ۑ��ɏ���Ă��鎞�̍��W
    private Vector3 setRideMovePos;
    // ���C�̕���
    private Vector3 forwardDirection;
    private Vector3 backwardDirection;
    private Vector3 rightDirection;
    private Vector3 leftDirection;

    [Header("Move�EJump")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [Header("ParticleSystem")]
    [SerializeField] GameObject splash;
    [Header("AudioClip")]
    [SerializeField] AudioClip logSE;
    [SerializeField] AudioClip hitSE;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip coinSE;
    [SerializeField] AudioClip splashSE;
    [Header("Component")]
    private GameObject scoreObj;
    [SerializeField] ScoreController scoreController;
    private GameObject audioObj;
    [SerializeField] AudioManager audioManager;
    private GameObject gameControllerObj;
    [SerializeField] GameController gameController;
    Rigidbody rb;
    RiverObjectController riverObjectController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GetComponentObject();
        SetRayDirection();
    }

    void FixedUpdate()
    {
        // �Q�[���I�[�o�[�ɂȂ�܂ł͓��삷��
        if (!gameController.isGameOver && gameController.isGameStart)
        {
            PlayerMoveBase();
            MoveCount();
            CheckHitStageObject();
        }
    }

    // �^�O����Ή������I�u�W�F�N�g����Component���擾
    private void GetComponentObject()
    {
        scoreObj = GameObject.FindWithTag("Score");
        scoreController = scoreObj.GetComponent<ScoreController>();
        audioObj = GameObject.FindWithTag("Audio");
        audioManager = audioObj.GetComponent<AudioManager>();
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
    }

    // ���C�̕�����ݒ�
    private void SetRayDirection()
    {
        forwardDirection = transform.forward;
        forwardDirection.y = 0.0f;
        backwardDirection = -transform.forward;
        backwardDirection.y = 0.0f;
        rightDirection = transform.right;
        rightDirection.y = 0.0f;
        leftDirection = -transform.right;
        leftDirection.y = 0.0f;
    }

    // �v���C���[�̓���֌W
    private void PlayerMoveBase()
    {
        // �v���C���[�������Ƃ��ɓ���
        if (isMoving)
        {
            SetMovePos();
        }
        // �ۑ��ɏ�鎞�ɓ���
        if (isRide)
        {
            SetRidePos();
        }
        // �ۑ��ɏ���Ă��鎞�ɓ���
        if (isRiding)
        {
            RidingMovePos();
        }
    }

    // �W�����v�̐��i���݁j
    public void PlayerMoveSandby()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.1f, transform.localScale.z);
    }

    // �W�����v���Ĉړ��i���̃T�C�Y�ɖ߂��j
    public void PlayerMoving()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
        audioManager.DefaultPicht();
    }

    private void CheckHitStageObject()
    {
        // ���ʕ�����Ray���΂�
        Ray forwardRay = new Ray(transform.position, forwardDirection);
        RaycastHit forwardHit;
        Debug.DrawRay(transform.position, forwardDirection, Color.red);

        if (Physics.Raycast(forwardRay, out forwardHit, detectionDistance))
        {
            GameObject hitObject = forwardHit.collider.gameObject;

            // ���������I�u�W�F�N�g���w�肵���^�O�������ꍇ�A���m�������s��
            if (hitObject.CompareTag("StageObject"))
            {
                isFrontWall = true;
            }
            else
            {
                isFrontWall = false;
            }
        }
        else
        {
            isFrontWall = false;
        }

        // �w�ʕ�����Ray���΂�
        Ray backwardRay = new Ray(transform.position, backwardDirection);
        RaycastHit backwardHit;
        Debug.DrawRay(transform.position, backwardDirection, Color.red);

        // Ray���I�u�W�F�N�g�ɓ����������𔻒�
        if (Physics.Raycast(backwardRay, out backwardHit, detectionDistance))
        {
            GameObject hitObject = backwardHit.collider.gameObject;

            // ���������I�u�W�F�N�g���w�肵���^�O�������ꍇ�A���m�������s��
            if (hitObject.CompareTag("StageObject"))
            {
                isBackWall = true;
            }
            else
            {
                isBackWall = false;
            }
        }
        else
        {
            isBackWall = false;
        }

        // �E������Ray���΂�
        Ray rightRay = new Ray(transform.position, rightDirection);
        RaycastHit rightHit;

        Debug.DrawRay(transform.position, rightDirection, Color.blue);
        if (Physics.Raycast(rightRay, out rightHit, detectionDistance))
        {
            GameObject hitObject = rightHit.collider.gameObject;

            // ���������I�u�W�F�N�g���w�肵���^�O�������ꍇ�A���m�������s��
            if (hitObject.CompareTag("StageObject"))
            {
                isRightWall = true;
            }
            else
            {
                isRightWall = false;
            }
        }
        else
        {
            isRightWall = false;
        }

        // ��������Ray���΂�
        Ray leftRay = new Ray(transform.position, leftDirection);
        RaycastHit leftHit;

        // Ray���I�u�W�F�N�g�ɓ����������𔻒�
        if (Physics.Raycast(leftRay, out leftHit, detectionDistance))
        {
            GameObject hitObject = leftHit.collider.gameObject;

            // ���������I�u�W�F�N�g���w�肵���^�O�������ꍇ�A���m�������s��
            if (hitObject.CompareTag("StageObject"))
            {
                isLeftWall = true;
            }
            else
            {
                isLeftWall = false;
            }
        }
        else
        {
            isLeftWall = false;
        }
    }


    // transform.position���ePos�ɐݒ�
    public void SetMovePos()
    {
        playerMovePos = new Vector3(playerSetPosX, transform.position.y, playerSetPosZ);
        transform.position = Vector3.MoveTowards(transform.position, playerMovePos, moveSpeed);

        if (transform.position == playerMovePos && rb.velocity.y == 0.0f)
        {
            isMoving = false;
        }
    }

    public void PlayerMoveUp()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (!isFrontWall)
        {
            isMoving = true;
            playerSetPosZ += 2;
            rb.AddForce(transform.up * 7.5f, ForceMode.VelocityChange);
            audioManager.PlaySE3D(jumpSE);
        }
    }

    public void PlayerMoveDown()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        if (!isBackWall)
        {
            isMoving = true;
            playerSetPosZ -= 2;
            rb.AddForce(transform.up * 7.5f, ForceMode.VelocityChange);
            audioManager.PlaySE3D(jumpSE);
        }
    }

    //�v���C���[���E�����ɓ�������
    public void PlayerMoveRight()
    {
        if (transform.position.x < 8 && !isRightWall)
        {
            // �ۑ��ɏ���Ă��邩�ǂ���
            if (isRiding)
            {
                isRideMovingRight = true;
                setRideMovePos = new Vector3(transform.localPosition.x + 2.0f, transform.localPosition.y, transform.localPosition.z);
                audioManager.PlaySE3D(jumpSE);
            }
            else
            {
                isMoving = true;
                playerSetPosX += 2;
                audioManager.PlaySE3D(jumpSE);
            }
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // �v���C���[���ۑ��ɏ���Ă��鎞�ɉE�����ɓ�������
    public void PlayerRidingMoveRight()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, setRideMovePos, moveSpeed);
        CheckMoved(setRideMovePos);
    }

    public void PlayerMoveLeft()
    {
        if (transform.position.x > -8 && !isLeftWall)
        {
            // �ۑ��ɏ���Ă��邩�ǂ���
            if (isRiding)
            {
                isRideMovingLeft = true;
                setRideMovePos = new Vector3(transform.localPosition.x - 2.0f, transform.localPosition.y, transform.localPosition.z);
                audioManager.PlaySE3D(jumpSE);
            }
            else
            {
                isMoving = true;
                playerSetPosX -= 2;
                audioManager.PlaySE3D(jumpSE);
            }
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    // �v���C���[���ۑ��ɏ���Ă��鎞�ɍ������ɓ�������
    public void PlayerRidingMoveLeft()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, setRideMovePos, moveSpeed);
        CheckMoved(setRideMovePos);
    }

    public void RidingMovePos()
    {
        if (isRideMovingRight)
        {
            PlayerRidingMoveRight();
        }
        if (isRideMovingLeft)
        {
            PlayerRidingMoveLeft();
        }

        // (�ۑ��ɏ���Ă���ԁA�v���C���[���ړ����Ă��܂����ߗ��ɏオ�������̍��W���w�肵�Ă���)
        // �ۑ��ɏ���Ă���v���C���[��X���W����}�X�A���ɂ��ꂽ���}�X���ɂ��炷
        if (transform.position.x <= playerSetPosX - 1.0f)
        {
            playerSetPosX -= 2;
        }
        // �ۑ��ɏ���Ă���v���C���[��X���W����}�X�A�E�ɂ��ꂽ���}�X�E�ɂ��炷
        if (transform.position.x >= playerSetPosX + 1.0f)
        {
            playerSetPosX += 2;
        }
    }

    // �ۑ��ɏ�鎞�Ɉʒu��ݒ�
    public void SetRidePos()
    {
        switch (riverObjectController.logTypeNum)
        {
            case (int)TreeLogType.TwoSquare:
                if (transform.localPosition.x >= 0.0f)
                {
                    setRideMovePos = new Vector3(1.0f, transform.localPosition.y, 0);
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, setRideMovePos, moveSpeed);

                    CheckMoved(setRideMovePos);
                }
                else if (transform.localPosition.x < 0.0f)
                {
                    setRideMovePos = new Vector3(-1.0f, transform.localPosition.y, 0);
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, setRideMovePos, moveSpeed);

                    CheckMoved(setRideMovePos);
                }
                break;
            case (int)TreeLogType.ThreeSquare:
                break;
        }
    }

    // �����I��������̊m�F
    private void CheckMoved(Vector3 setTargetPos)
    {
        if (transform.localPosition.x == setTargetPos.x)
        {
            isRide = false;
            isRideMovingRight = false;
            isRideMovingLeft = false;
        }
    }

    private void MoveCount()
    {
        // �i�񂾃}�X����
        moveCount = transform.position.z / 2;
        // �i�񂾃}�X����ԑO�Ȃ�J�E���g����
        if (moveCount > arriveCount)
        {
            arriveCount = moveCount;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            scoreController.SaveScore();
            gameController.isGameOver = true;

            // �Ԃɓ����������ɐ�����΂�
            Vector3 direction = transform.position - collision.transform.position;
            direction.Normalize();
            audioManager.PlaySE3D(hitSE);
            var blowingDirection = new Vector3(direction.x, direction.y + 2.0f, direction.z) * 25.0f;
            rb.AddForce(blowingDirection, ForceMode.Impulse);
            gameObject.layer = LayerMask.NameToLayer("DeadPlayer");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            isRide = true;
            transform.parent = other.transform;
            riverObjectController = other.gameObject.GetComponent<RiverObjectController>();
            audioManager.LowPicht();
            audioManager.PlaySE3D(logSE);
        }

        if (other.gameObject.CompareTag("RiverOutArea"))
        {
            if (isRiding)
            {
                scoreController.SaveScore();
                gameController.isGameOver = true;
            }
        }
        if (other.gameObject.CompareTag("River"))
        {
            scoreController.SaveScore();
            audioManager.PlaySE3D(splashSE);
            gameController.isGameOver = true;
            Instantiate(splash, new Vector3(transform.position.x, 0.0f, transform.position.z), Quaternion.identity);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            CoinController.possCoin++;
            Debug.Log("GetCoin:" + CoinController.possCoin);
            audioManager.LowPicht();
            Destroy(other.gameObject);
            audioManager.PlaySE3D(coinSE);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            isRiding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            isRiding = false;
            transform.parent = null;
        }
    }
}