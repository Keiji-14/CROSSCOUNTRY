using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ʃ^�b�v�ő��삷��N���X
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

    // �^�O����Ή������I�u�W�F�N�g����Component���擾
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
                //�E�����Ƀt���b�N
                player.PlayerMoveRight();
            }
            else if (-50 > directionX)
            {
                //�������Ƀt���b�N
                player.PlayerMoveLeft();
            }
        }

        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (50 < directionY)
            {
                //������Ƀt���b�N 
                player.PlayerMoveUp();
            }
            else if (-50 > directionY)
            {
                //�������̃t���b�N
                player.PlayerMoveDown();
            }
        }
        else
        {
            //�^�b�`�����o
            player.PlayerMoveUp();
        }
    }
}
