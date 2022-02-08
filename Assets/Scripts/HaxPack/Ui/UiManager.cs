using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hex_Package
{
    public class UiManager : Singleton<UiManager>
    {
        MoveScoreTMP moveScore;
        public EventPopup eventPopup;
        private void Start()
        {
            moveScore = GameObject.Find("MoveScoreTMP").GetComponent<MoveScoreTMP>();
            moveScore.Setup();
            eventPopup.Setup();
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
    }



}