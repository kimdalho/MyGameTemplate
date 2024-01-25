using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


    public class TileTurnManager : Singleton<TileTurnManager>
    {
        [Header("Develop")]
        public TurnType type;
        [SerializeField] [Tooltip("시작 배분이 매우 빨라집니다")] bool fastMode;
        [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

        [Header("Properties")]
        public bool isLoading; // 게임이 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지
        public bool myTurn;

        public static Action OnAddCard;
        //public static event Action<bool> OnTurnStarted;

        //매 코루틴 호출에서 동적으로 생성하는것은 막는다
        WaitForSeconds delay05 = new WaitForSeconds(0.5f);
        WaitForSeconds delay07 = new WaitForSeconds(0.7f);

        public enum TurnType
        {
            None = 0,
            PutCard = 1,
            Move = 2,
            Questevent = 3,
            Battle = 4,
        }

        void GameSetup()
        {
            if (fastMode)
                delay05 = new WaitForSeconds(0.05f);

        }

        public IEnumerator StartGameCo()
        {
            GameSetup();
            isLoading = true;

            for (int i = 0; i < startCardCount; i++)
            {
                yield return delay05;
                OnAddCard?.Invoke();
            }
            yield return new WaitForSeconds(1);
            SkillCardManager.OnEndLoading?.Invoke();

            isLoading = false;
        }
    }