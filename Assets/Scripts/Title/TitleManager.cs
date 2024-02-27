using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    [Header("TitleButtons")]
    [SerializeField] private Button btn_NewGameStart;
    [SerializeField] private Button btn_LoadGame;
    [SerializeField] private Button btn_ExitGame;

    [Header("Resources")]
    [SerializeField] private Fade popup_fade;
    [SerializeField] private Say say_loadPopup;

    private void Awake()
    {
        btn_NewGameStart.onClick.AddListener(OnClickedNewGameStart);
        btn_LoadGame.onClick.AddListener(OnClickedLoadGame);
        btn_ExitGame.onClick.AddListener(OnClickedExitGame);
    }

    public void OnClickedNewGameStart()
    {
        Debug.Log("시작 ");

        popup_fade.FadeIn(()=>
        {
            SceneContainer.instance.LoadScene(eSceneType.GameScene);
        });
    }

    public void OnClickedLoadGame()
    {
        if (say_loadPopup.gameObject.activeSelf == true)
            return;

        say_loadPopup.gameObject.SetActive(true);
        say_loadPopup.Do();

        Debug.Log("불 러 오 기  ");
    }

    public void OnClickedExitGame()
    {
        Application.Quit();
        Debug.Log("나 가 기 ");
    }

}
