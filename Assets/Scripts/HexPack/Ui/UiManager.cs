using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hex_Package
{
    public class UiManager : Singleton<UiManager>
    {

        public Queue<BasePopup> escQueue = new Queue<BasePopup>();


        MoveScoreTMP moveScore;
        public InvenButton invenButton;

        public InvenPopup invenPopup;
        public EventPopup eventPopup;

        public void OnClickInvenButton()
        {
            invenPopup.gameObject.SetActive(true);
            AddPopupESCList(invenPopup);

        }

        private void Start()
        {
            moveScore = GameObject.Find("MoveScoreTMP").GetComponent<MoveScoreTMP>();
            moveScore.Setup();
            eventPopup.Setup();
            invenButton.Setup();
            invenPopup.Setup();
            eventPopup.gameObject.SetActive(false);
        }

        public void RefreshMoveScoreText()
        {
            moveScore.GetTMP().text = GameManager.Instance.MoveScore.ToString();
        }

        public void ShowEventPopup()
        {
            eventPopup.SetEventData();
            eventPopup.gameObject.SetActive(true);
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

