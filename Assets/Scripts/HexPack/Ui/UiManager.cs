using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hex_Package
{
    public class UiManager : Singleton<UiManager>
    {

        public Queue<BasePopup> escQueue = new Queue<BasePopup>();


        public MoveScoreTMP moveScore;
        public InvenButton invenButton;

        [Header("Popup")]
        public InvenPopup invenPopup;
        public QuestPopup questPopup;
        public RewardPopup rewardPopup;



        public static event Action showPopup;


        public void OnClickInvenButton()
        {
            invenPopup.gameObject.SetActive(true);
            AddPopupESCList(invenPopup);

        }

        private void Start()
        {
            moveScore.Setup();
            questPopup.Setup();
            rewardPopup.Setup();
            invenButton.Setup();
            invenPopup.Setup();
        }

        public void RefreshMoveScoreText()
        {
            moveScore.GetTMP().text = GameManager.Instance.MoveScore.ToString();
        }

        public void ShowEventPopup()
        {
            showPopup += questPopup.SetPopupData;
            showPopup?.Invoke();
            questPopup.gameObject.SetActive(true);
        }

        public void ShowRewardPopup()
        {
            showPopup += HideEventPopup;
            showPopup += rewardPopup.SetPopupData;
            showPopup?.Invoke();
            rewardPopup.gameObject.SetActive(true);
        }
        public void HideEventPopup()
        {
            showPopup = null;
            questPopup.gameObject.SetActive(false);
        }

        public void HideRewardPopup()
        {
            showPopup = null;
            rewardPopup.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            showPopup -= questPopup.SetPopupData;
            showPopup -= rewardPopup.SetPopupData;
        }


        public void OnClickBackButton()
        {
            escQueue.Dequeue().OnEscape();
        }

        public void AddPopupESCList(BasePopup popup)
        {
            escQueue.Enqueue(popup);
        }
    }



}

interface iEscape
{
    void OnEscape();
}

