using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Card_GamePackage
{
    public class TurnManager : Singleton<TurnManager>
    {
        [Header("Develop")]
        [SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTrunMode;
        [SerializeField] [Tooltip("시작 배분이 매우 빨라집니다")] bool fastMode;
        [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

        [Header("Properties")]
        public bool isLoading; // 게임이 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지
        public bool myTurn;

        enum ETurnMode { Random, My, Other }
        //매 코루틴 호출에서 동적으로 생성하는것은 막는다
        WaitForSeconds delay05 = new WaitForSeconds(0.5f);
        WaitForSeconds delay07 = new WaitForSeconds(0.7f);

        //CardManager에서 notification
        public static Action<bool> OnAddCard;
        public static event Action<bool> OnTurnStarted;

        void GameSetup()
        {
            if (fastMode)
                delay05 = new WaitForSeconds(0.05f);

            switch (eTrunMode)
            {
                case ETurnMode.Random:
                    myTurn = Random.Range(0, 2) == 0;
                    break;
                case ETurnMode.My:
                    myTurn = true;
                    break;
                case ETurnMode.Other:
                    myTurn = false;
                    break;
            }
        }

        public IEnumerator StartGameCo()
        {
            GameSetup();
            isLoading = true;

            for (int i = 0; i < startCardCount; i++)
            {
                yield return delay05;
                OnAddCard?.Invoke(false);
                yield return delay05;
                OnAddCard?.Invoke(true);
            }

            StartCoroutine(StartTurnCo());
        }

        IEnumerator StartTurnCo()
        {
            isLoading = true;
            if (myTurn)
                UiManager.Instance.Notification("나의 턴");
            else
                UiManager.Instance.Notification("적의 턴");
            yield return delay07;
            OnAddCard?.Invoke(myTurn);
            yield return delay07;
            isLoading = false;
            OnTurnStarted?.Invoke(myTurn);

        }

        public void EndTurn()
        {
            myTurn = !myTurn;
            StartCoroutine(StartTurnCo());
        }
    }
}