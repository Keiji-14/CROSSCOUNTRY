using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class Player : MonoBehaviour
{
    // 移動・到達点
    [HideInInspector] public bool isMoving;
    [HideInInspector] public float arriveCount;
    [HideInInspector] public float moveCount;
    // 壁関係
    public bool isFrontWall;
    public bool isBackWall;
    public bool isRightWall;
    public bool isLeftWall;
    // 丸太関係
    private bool isRide;
    private bool isRiding;
    private bool isRideMovingRight;
    private bool isRideMovingLeft;
    // プレイヤーの移動XYZ座標
    private float playerSetPosY;
    private float playerSetPosZ;
    private float playerSetPosX;
    // レイの距離
    private float detectionDistance = 2.0f;
    // プレイヤーの設定座標
    private Vector3 playerMovePos;
    // プレイヤーが丸太に乗っている時の座標
    private Vector3 setRideMovePos;
    // レイの方向
    private Vector3 forwardDirection;
    private Vector3 backwardDirection;
    private Vector3 rightDirection;
    private Vector3 leftDirection;

    [Header("Move・Jump")]
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
        // ゲームオーバーになるまでは動作する
        if (!gameController.isGameOver && gameController.isGameStart)
        {
            PlayerMoveBase();
            MoveCount();
            CheckHitStageObject();
        }
    }

    // タグから対応したオブジェクトからComponentを取得
    private void GetComponentObject()
    {
        scoreObj = GameObject.FindWithTag("Score");
        scoreController = scoreObj.GetComponent<ScoreController>();
        audioObj = GameObject.FindWithTag("Audio");
        audioManager = audioObj.GetComponent<AudioManager>();
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
    }

    // レイの方向を設定
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

    // プレイヤーの動作関係
    private void PlayerMoveBase()
    {
        // プレイヤーが動くときに動作
        if (isMoving)
        {
            SetMovePos();
        }
        // 丸太に乗る時に動作
        if (isRide)
        {
            SetRidePos();
        }
        // 丸太に乗っている時に動作
        if (isRiding)
        {
            RidingMovePos();
        }
    }

    // ジャンプ体制（屈み）
    public void PlayerMoveSandby()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.1f, transform.localScale.z);
    }

    // ジャンプして移動（元のサイズに戻す）
    public void PlayerMoving()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
        audioManager.DefaultPicht();
    }

    private void CheckHitStageObject()
    {
        // 正面方向にRayを飛ばす
        Ray forwardRay = new Ray(transform.position, forwardDirection);
        RaycastHit forwardHit;
        Debug.DrawRay(transform.position, forwardDirection, Color.red);

        if (Physics.Raycast(forwardRay, out forwardHit, detectionDistance))
        {
            GameObject hitObject = forwardHit.collider.gameObject;

            // 当たったオブジェクトが指定したタグだった場合、検知処理を行う
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

        // 背面方向にRayを飛ばす
        Ray backwardRay = new Ray(transform.position, backwardDirection);
        RaycastHit backwardHit;
        Debug.DrawRay(transform.position, backwardDirection, Color.red);

        // Rayがオブジェクトに当たったかを判定
        if (Physics.Raycast(backwardRay, out backwardHit, detectionDistance))
        {
            GameObject hitObject = backwardHit.collider.gameObject;

            // 当たったオブジェクトが指定したタグだった場合、検知処理を行う
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

        // 右方向にRayを飛ばす
        Ray rightRay = new Ray(transform.position, rightDirection);
        RaycastHit rightHit;

        Debug.DrawRay(transform.position, rightDirection, Color.blue);
        if (Physics.Raycast(rightRay, out rightHit, detectionDistance))
        {
            GameObject hitObject = rightHit.collider.gameObject;

            // 当たったオブジェクトが指定したタグだった場合、検知処理を行う
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

        // 左方向にRayを飛ばす
        Ray leftRay = new Ray(transform.position, leftDirection);
        RaycastHit leftHit;

        // Rayがオブジェクトに当たったかを判定
        if (Physics.Raycast(leftRay, out leftHit, detectionDistance))
        {
            GameObject hitObject = leftHit.collider.gameObject;

            // 当たったオブジェクトが指定したタグだった場合、検知処理を行う
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


    // transform.positionを各Posに設定
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

    //プレイヤーが右方向に動く処理
    public void PlayerMoveRight()
    {
        if (transform.position.x < 8 && !isRightWall)
        {
            // 丸太に乗っているかどうか
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

    // プレイヤーが丸太に乗っている時に右方向に動く処理
    public void PlayerRidingMoveRight()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, setRideMovePos, moveSpeed);
        CheckMoved(setRideMovePos);
    }

    public void PlayerMoveLeft()
    {
        if (transform.position.x > -8 && !isLeftWall)
        {
            // 丸太に乗っているかどうか
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

    // プレイヤーが丸太に乗っている時に左方向に動く処理
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

        // (丸太に乗っている間、プレイヤーが移動してしまうため陸に上がった時の座標を指定しておく)
        // 丸太に乗っているプレイヤーのX座標が一マス、左にずれたら一マス左にずらす
        if (transform.position.x <= playerSetPosX - 1.0f)
        {
            playerSetPosX -= 2;
        }
        // 丸太に乗っているプレイヤーのX座標が一マス、右にずれたら一マス右にずらす
        if (transform.position.x >= playerSetPosX + 1.0f)
        {
            playerSetPosX += 2;
        }
    }

    // 丸太に乗る時に位置を設定
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

    // 動き終わったかの確認
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
        // 進んだマスを代入
        moveCount = transform.position.z / 2;
        // 進んだマスが一番前ならカウントする
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

            // 車に当たった時に吹き飛ばす
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