using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneManager���Ǘ�����N���X
/// </summary>
public class ScenesManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] FadeController fadeController;

    // �^�C�g����ʂɑJ�ڂ���R���[�`���Ăяo��
    public void TitleScene()
    {
        fadeController.isFadeOut = true;
        StartCoroutine(ChangeTitleScene());
    }

    // �^�C�g����ʂɑJ��
    private IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("TitleScene");
    }

    // �Q�[����ʂɑJ�ڂ���R���[�`���Ăяo��
    public void GameScene()
    {
        fadeController.isFadeOut = true;
        StartCoroutine(ChangeGameScene());
    }

    // �Q�[����ʂɑJ��
    private IEnumerator ChangeGameScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
}
