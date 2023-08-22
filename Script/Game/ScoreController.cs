using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �X�R�A�̊Ǘ�����N���X
/// </summary>
public class ScoreController : MonoBehaviour
{
    [Header("TextMeshPro")]
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Component")]
    private GameObject playerObj;
    [SerializeField] Player player;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
    }

    void Update()
    {
        scoreText.text = player.arriveCount.ToString("0");
    }

    // �Q�[���I�[�o�[���ɍ���̃X�R�A���n�C�X�R�A�Ȃ�ۑ�������
    public void SaveScore()
    {
        var highScoreNum = PlayerPrefs.GetFloat("HIGHSCORE", 0);
        if (player.arriveCount > highScoreNum)
        {
            PlayerPrefs.SetFloat("HIGHSCORE", player.arriveCount);
        }
    }
}
