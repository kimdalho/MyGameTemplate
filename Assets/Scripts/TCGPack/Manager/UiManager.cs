using UnityEngine;

namespace Card_GamePackage
{
    public class UiManager : Singleton<UiManager>
    {
        public NotificationPannel notificationPannel;
        public ResultPanel resultPanel;
        public EndTurnButton endTurnButton;
        public GameStartPanel gameStartPanel;
        //############# PlayButton ######################
        public PlayButton playButton;
        //############# PlayButton ######################


        private void Start()
        {
            UiSetup();
        }

        private void UiSetup()
        {
            NotificationPannelSetup();
            EndTurnButtonSetup();
            ResultPanelSetup();
            //모든게 초기화가 이루어진다음 마지막으로 게임버튼을 나타낸다
            GameStartPanelSetup();
        }

        public void OnClickedTurnOverButton()
        {
            TurnManager.Instance.EndTurn();
            endTurnButton.SetButtonInteractable(false);
        }
        public void Notification(string message)
        {
            notificationPannel.Show(message);
        }


        #region Setups
        private void ResultPanelSetup()
        {
            resultPanel = GameObject.Find("ResultPanel").GetComponent<ResultPanel>();
            resultPanel.Setup();
        }


        private void EndTurnButtonSetup()
        {
            endTurnButton = GameObject.Find("EndTurnButton").GetComponent<EndTurnButton>();
            endTurnButton.Setup();
            endTurnButton.GetEndTurnButton().onClick.AddListener(OnClickedTurnOverButton);

        }


        private void NotificationPannelSetup()
        {
            notificationPannel = GameObject.Find("NotificationPanel").GetComponent<NotificationPannel>();
            notificationPannel.Setup();
        }

        private void GameStartPanelSetup()
        {
            gameStartPanel = GameObject.Find("GameTitlePanel").GetComponent<GameStartPanel>();
            gameStartPanel.Setup();
            gameStartPanel.GetGameStartButton().onClick.AddListener(OnClickedGameStartButton);
        }
        #endregion

        private void OnClickedGameStartButton()
        {
            gameStartPanel.gameObject.SetActive(false);
            GameManager.Instance.StartGame();
        }
    }
}