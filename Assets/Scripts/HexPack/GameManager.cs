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
        public Player player;
        public CameraMovement camMove;
        

        public List<IUiObserver> uiObservers = new List<IUiObserver>();
        public List<ITurnSystem> miniSliders = new List<ITurnSystem>();

        // Start is called before the first frame update
        void Start()
        {
            fade.SetPopupColor(Color.black);
            status = eTurnType.AWake;
            GridManager.Instance.Setup();
            PathFindingManager.Instance.Setup();
            UnitManager.Instance.Setup();
            player =  UnitManager.Instance.CreatePlayer();
            Subject();
            StartGame();
            
            if (player == null)
            {
                Debug.LogError("player not create yet at UnitMnanger");
                return;
            }
            player.iplayerhit.Add(camMove.GetSubject());
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
                        yield return null;
                    }
                }
                allDisable = true;
            }
            Debug.Log("Good");
            SetStatus(eTurnType.PlayerTurn);
        }

        private IEnumerator CoAnimBattle(Unit attackUnit,Unit hitUnit)
        {
        if (attackUnit.GetCurrentHp() > 0)
        {
            var old = attackUnit.transform.position;
            var distance = (hitUnit.transform.position - attackUnit.transform.position) / 3;

            var tween = attackUnit.transform.DOLocalMove(distance + attackUnit.transform.position, 0.3f).SetEase(Ease.InFlash);
            yield return tween.WaitForCompletion();
            hitUnit.Hit(attackUnit);
            var hit_tween = hitUnit.transform.DOShakePosition(0.3f, 1, 20, 30, false);
            yield return hit_tween.WaitForCompletion();
            Subject();
            var backTween = attackUnit.transform.DOLocalMove(old, 1).SetEase(Ease.InOutBack);
            yield return backTween.WaitForCompletion();
        }

        }
        public IEnumerator CoBattle(Unit hitUnit)
        {
            yield return StartCoroutine(CoAnimBattle(player,hitUnit));
            yield return StartCoroutine(CoAnimBattle(hitUnit, player));

            if(player.GetCurrentHp() > 0 && hitUnit.GetCurrentHp() >0)
            {
                yield return StartCoroutine(CoBattle(hitUnit));
            }
            else if(player.GetCurrentHp() == 0)
            {
                player.Dead();
            }
            else if(hitUnit.GetCurrentHp() == 0)
            {
               var deadUnitNode =  hitUnit.GetTileOffSetPos();
               hitUnit.Dead();
               yield return new WaitForSeconds(0.3f);
               PathFindingManager.Instance.BattleEndMove(deadUnitNode);
            }
        }

    public void Subject()
        {
            foreach(var ui in uiObservers)
            {
                ui.Notification();
            }
        }

    private void OnDestroy()
    {
     turnStart = null ;
     turnAwake = null;
     PlayerMoveEnd = null;
    }
}

