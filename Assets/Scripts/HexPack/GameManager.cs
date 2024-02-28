using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private Fade fade;

        public event Action turnStart;
        public event Action turnAwake;
        public event Action PlayerMoveEnd;
        public eTurnType status;
        public PlayerState playerStat;

        public List<IUiObserver> uiObservers = new List<IUiObserver>();
        public List<ITurnSystem> miniSliders = new List<ITurnSystem>();

        // Start is called before the first frame update
        void Start()
        {
            fade.SetPopupColor(Color.black);
            status = eTurnType.AWake;
            GridManager.Instance.Setup();
            PathFindingManager.Instance.CreateNodeList();
            UnitManager.Instance.Setup();
            playerStat.Setup();
            Subject();
            StartGame();
        }

        public void SetStatus(eTurnType next)
        {
            status = next;
            EventTurnSystemUpdate();
        }
        
        
        
        private void EventTurnSystemUpdate()
        {
            switch (status)
            {
                case eTurnType.AWake:
                    break;
                case eTurnType.PlayerTurn:
                    //turnEnd.Invoke();
                    //상태에서는 오로지 플레이어만이 이동합니다
                    break;
                case eTurnType.PlayerMoveEnd:
                    PlayerMoveEnd.Invoke();
                    StartCoroutine(CoCheckAllFade());
                    //플레이어는 허기 상태가 감소합니다.
                    //크리처는 이동을 시작합니다.
                    //타워는 생상을 시작합니다.
                break;
                case eTurnType.GameOver:
                    break;

            }
            
        }
            

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                SetStatus(eTurnType.PlayerTurn);
            }
            else if(Input.GetKeyDown(KeyCode.F2))
            {
                SetStatus(eTurnType.PlayerMoveEnd);
            }
        }

        public void StartGame()
        {
            fade.FadeIn(() => { SetStatus(eTurnType.PlayerTurn); }); 
        }

        public void CheatGetMoveScore()
        {
            UserData.Instance.move += 2;
            UiManager.Instance.RefreshTopUi();
        }

        private IEnumerator CoCheckAllFade()
        {
            bool allDisable = false;
            while (!allDisable)
            {
                for (int i = 0; i < miniSliders.Count; i++)
                {
                    if ((miniSliders[i] as MonoBehaviour).isActiveAndEnabled)
                    {
                        Debug.Log("한번 호출이면 내실수 ");
                        yield return null;
                    }
                }
                allDisable = true;
            }
            Debug.Log("Good");
            SetStatus(eTurnType.PlayerTurn);
        }

        private IEnumerator CoAnimAttack(Unit hitUnit)
        {
            var player = PathFindingManager.Instance.agent;
            var old = hitUnit.transform.position;
            hitUnit.transform.DOLocalMove(player.transform.position,0.3f).SetEase(Ease.InOutBack);
            yield return new WaitForSeconds(1f);
            hitUnit.transform.DOLocalMove(old, 1).SetEase(Ease.InOutBack);
            playerStat.curHp = Math.Clamp(playerStat.curHp - hitUnit.item.atk, 0, playerStat.maxHp);
            hitUnit.curHp = Math.Clamp(hitUnit.curHp - playerStat.atk, 0, hitUnit.maxHp);
            Subject();
        }

        public bool Attack(Unit hitUnit)
        {
            StartCoroutine(CoAnimAttack(hitUnit));

            
            if (playerStat.curHp == 0)
            {
                Subject();
            
                return false;
            }
        //몬스터가 죽었으며 플레이어가 살아있을때만 True;
        if (hitUnit.curHp <= 0)
        {
            Subject();
            Debug.Log($"Attacked {playerStat.curHp}");
            hitUnit.Dead();
            return true;
        }

        Subject();
            return false;
        }

        public void Subject()
        {
            foreach(var ui in uiObservers)
            {
                ui.Notification();
            }
        }
}

