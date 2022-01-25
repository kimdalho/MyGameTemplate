using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임을 관리
public class GameManager : Singleton<GameManager>
{
    [SerializeField] NotificationPannel notificationPanel;
    WaitForSeconds delay2 = new WaitForSeconds(1);

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
/*    private void Start()
    {
        StartGame();
    }
*/
    private void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }

    void InputCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TurnManager.OnAddCard?.Invoke(true);

        if (Input.GetKeyDown(KeyCode.X))
            TurnManager.OnAddCard?.Invoke(false);

        if (Input.GetKeyDown(KeyCode.F3))
            TurnManager.Instance.EndTurn();

        if (Input.GetKeyDown(KeyCode.F4))
            EntityManager.Instance.DamageBoss(true, 19);

        if (Input.GetKeyDown(KeyCode.F5))
            EntityManager.Instance.DamageBoss(false, 19);

    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Instance.StartGameCo());
    }

    public IEnumerator GameOver(bool isMyWin)
    {
        //카드클릭의 방지
        TurnManager.Instance.isLoading = true;
        UiManager.Instance.endTurnButton.gameObject.SetActive(false);
        yield return delay2;
        UiManager.Instance.resultPanel.Show(isMyWin ? "승리" : "패배");
    }
}
