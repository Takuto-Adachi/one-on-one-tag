using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //プレイ開始ボタン
    public void StartButton()
    {
        SceneManager.LoadScene("tag");
    }

    //ルール説明画面へ
    public void RuleButton()
    {
        SceneManager.LoadScene("rule");
    }

    //タイトル画面へ
    public void TitleButton()
    {
        SceneManager.LoadScene("title");
    }
}
