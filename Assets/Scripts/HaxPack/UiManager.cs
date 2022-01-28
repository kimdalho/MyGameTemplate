using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hex_Package
{
    public class UiManager : Singleton<UiManager>
    {
        PathFinderButton pathFinderBtn;

        private void Start()
        {
            PathFinderButtonSetup();
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
    }



}