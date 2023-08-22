using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�R�A�̊Ǘ�����N���X
/// </summary>
public class Camera : MonoBehaviour
{
    private Vector3 offset;
    [Header("Component")]
    private GameObject playerObj;
    [SerializeField] Player player;
    private GameObject gameControllerObj;
    [SerializeField] GameController gameController;
    

    void Start()
    {
        GetComponentObject();

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (!gameController.isGameOver)
        {
            ScrollCamera();
        }
    }

    // �^�O����Ή������I�u�W�F�N�g����Component���擾
    private void GetComponentObject()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
        gameControllerObj = GameObject.FindWithTag("GameController");
        gameController = gameControllerObj.GetComponent<GameController>();
    }

    private void ScrollCamera()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 5.0f * Time.deltaTime);
    }
}
