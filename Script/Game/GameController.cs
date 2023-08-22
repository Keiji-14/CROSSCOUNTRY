using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �Q�[�����Ǘ�����N���X
/// </summary>
public class GameController : MonoBehaviour
{
    [HideInInspector] public bool isGameStart;
    [HideInInspector] public bool isGameOver;

    private bool isCountFinish;
    private float countTime = 3.5f;
    private float highScoreNum;

    [SerializeField] GameObject countDownObj;
    [SerializeField] GameObject gameOverWindow;
    
    [Header("TextMeshPro")]
    [SerializeField] TextMeshProUGUI possCoinText;    
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [Header("Component")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] FadeController fadeController;
    private GameObject skinObj;
    [SerializeField] SkinController skinController;
    [SerializeField] PossessionSkinDatabase possSkinDataBase;

    void Start()
    {
        // �Q�[���J�n���Ƀt�F�[�h�C��������
        fadeController.isFadeIn = true;

        skinObj = GameObject.FindWithTag("Skin");
        skinController = skinObj.GetComponent<SkinController>();
        Instantiate(possSkinDataBase.possSkins[skinController.setSkinNum].gamePrefab, Vector3.zero, Quaternion.identity);
    }

    void FixedUpdate()
    {
        // �t�F�[�h�C��������������J�E���g�_�E���J�n
        if (fadeController.isFinishFadeIn && !isCountFinish)
        {
            CountDown();
        }

        // �Q�[���I�[�o�[�ɂȂ������ǂ���
        if (isGameOver)
        {
            bgmSource.Stop();
            CoinController.SavePossCoin();
            Invoke("GameOver", 2.0f);
        }

        possCoinText.text = CoinController.possCoin.ToString("0");
    }

    // �Q�[���J�n�̃J�E���g�_�E���̏���
    private void CountDown()
    {
        countTime -= Time.deltaTime;
        countDownText.text = countTime.ToString("0");
        if (countTime <= 0.5f)
        {
            isGameStart = true;
            isCountFinish = true;
            bgmSource.Play();
            countDownObj.SetActive(false);
        }
    }

    // �Q�[���I�[�o�[�̎��ɌĂ΂�鏈��
    private void GameOver()
    {
        highScoreNum = PlayerPrefs.GetFloat("HIGHSCORE", 0);
        highScoreText.text = highScoreNum.ToString("0");

        float halfScreenWidth = Screen.width * 0.5f;
        float halfScreenHeight = Screen.height * 0.5f;

        Vector3 targetPos = new Vector3(halfScreenWidth, halfScreenHeight, 0.0f);
        //Vector3 targetPos = new Vector3(305.0f, 540.0f, 0.0f);
        gameOverWindow.transform.position = Vector3.MoveTowards(gameOverWindow.transform.position, targetPos, 20.0f);
    }
}
