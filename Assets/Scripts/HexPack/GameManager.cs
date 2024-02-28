using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private Fade fade;

        public event Action turnStart;
        public event Action turnAwake;
        public event Action PlayerMoveEnd;

        public eTurnType status;

        // Start is called before the first frame update
        void Start()
        {
            fade.SetPopupColor(Color.black);
            status = eTurnType.AWake;
            GridManager.Instance.Setup();
            PathFindingManager.Instance.CreateNodeList();
            UnitManager.Instance.Setup();
            
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

    }

