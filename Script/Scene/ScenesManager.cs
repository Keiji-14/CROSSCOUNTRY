using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneManagerを管理するクラス
/// </summary>
public class ScenesManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] FadeController fadeController;

    // タイトル画面に遷移するコルーチン呼び出し
    public void TitleScene()
    {
        fadeController.isFadeOut = true;
        StartCoroutine(ChangeTitleScene());
    }

    // タイトル画面に遷移
    private IEnumerator ChangeTitleScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("TitleScene");
    }

    // ゲーム画面に遷移するコルーチン呼び出し
    public void GameScene()
    {
        fadeController.isFadeOut = true;
        StartCoroutine(ChangeGameScene());
    }

    // ゲーム画面に遷移
    private IEnumerator ChangeGameScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
}
