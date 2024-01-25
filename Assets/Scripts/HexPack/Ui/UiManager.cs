using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UiManager : Singleton<UiManager>
    {

        public Queue<BasePopup> escQueue = new Queue<BasePopup>();

        [Header("TopUis")]
        public UiTopState moveScore;
        public UiTopState goldScore;
        public UiTopState fameScore;


        [Header("Button")]
        public InvenButton invenButton;

        [Header("Popup")]
        public InvenPopup invenPopup;
        public QuestPopup questPopup;
        public RewardPopup rewardPopup;

        public void OnClickInvenButton()
        {
            invenPopup.gameObject.SetActive(true);
            AddPopupESCList(invenPopup);

        }

        private void Start()
        {
            moveScore.Setup();
            goldScore.Setup();
            fameScore.Setup();
            questPopup.Setup();
            rewardPopup.Setup();
            invenButton.Setup();
            invenPopup.Setup();
        }

        

        

        public void ShowRewardPopup()
        {
            rewardPopup.gameObject.SetActive(true);
            rewardPopup.SetPopupData();
        }
        public void HideQuestPopup()
        {
            questPopup.gameObject.SetActive(false);
        }

        public void HideRewardPopup()
        {
            
            UnitManager.Instance.TargetUnitRelese();

            rewardPopup.EventRemove();
            rewardPopup.gameObject.SetActive(false);
        }

        public void OnClickBackButton()
        {
            escQueue.Dequeue().OnEscape();
        }

        public void AddPopupESCList(BasePopup popup)
        {
            escQueue.Enqueue(popup);
        }

        public void RefreshTopUi()
        {
            moveScore.GetTMP().text = UserData.Instance.move.ToString();
            goldScore.GetTMP().text = UserData.Instance.gold.ToString();
            fameScore.GetTMP().text = UserData.Instance.fame.ToString();
        }

        public void ShowQuestPopup(Quest newQuest)
        {
            var item = UnitManager.Instance.targetUnit.item;
            questPopup.GetRenderImage().sprite = item.render;
            questPopup.GetTMP1().text = newQuest.select1.title;
            questPopup.GetTMP2().text = newQuest.select2.title;
            questPopup.GetButton(0).onClick.AddListener(newQuest.select1.OnClickedButton);
            questPopup.GetButton(1).onClick.AddListener(newQuest.select2.OnClickedButton);
            questPopup.StartAppearTitle(newQuest.title);
        }
    }

interface iEscape
{
    void OnEscape();
}

