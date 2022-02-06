using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hex_Package
{
    public class UiManager : Singleton<UiManager>
    {
        PathFinderButton pathFinderBtn;
        MoveScoreTMP moveScore;
        private void Start()
        {
            PathFinderButtonSetup();
            moveScore = GameObject.Find("MoveScoreTMP").GetComponent<MoveScoreTMP>();
            moveScore.Setup();

        }

        public void PathFinderButtonSetup()
        {
            pathFinderBtn = GameObject.Find("PathFinderButton").GetComponent<PathFinderButton>();
            pathFinderBtn.GetButton().onClick.AddListener(OnClickedPathFinderButton);
        }

        public void OnClickedPathFinderButton()
        {

            StartCoroutine(PathFindingManager.Instance.Movement());
        }

        public void RefreshMoveScoreText()
        {
            moveScore.GetTMP().text = GameManager.Instance.MoveScore.ToString();
        }
    }



}