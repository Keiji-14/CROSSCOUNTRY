using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面タップで操作するクラス
/// </summary>
public class Touch : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    [Header("Component")]
    private GameObject playerObj;
    [SerializeField] Player player;
    private GameObject gameControllerObj;
    [SerializeField] GameController gameController;

    void Start()
    {
        GetComponentObject();
    }

    void Update()
    {
        Flick();
    }

    // タグから対応したオブジェクトからComponentを取得
    private void GetComponentObject()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
    }

    void Flick()
    {
        if (!player.isMoving && !gameController.isGameOver && gameController.isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                player.PlayerMoveSandby();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                GetDirection();
                player.PlayerMoving();
            }
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (50 < directionX)
            {
                //右向きにフリック
                player.PlayerMoveRight();
            }
            else if (-50 > directionX)
            {
                //左向きにフリック
                player.PlayerMoveLeft();
            }
        }

        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (50 < directionY)
            {
                //上向きにフリック 
                player.PlayerMoveUp();
            }
            else if (-50 > directionY)
            {
                //下向きのフリック
                player.PlayerMoveDown();
            }
        }
        else
        {
            //タッチを検出
            player.PlayerMoveUp();
        }
    }
}
