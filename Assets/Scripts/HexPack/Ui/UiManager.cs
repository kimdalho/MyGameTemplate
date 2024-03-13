using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UiManager : Singleton<UiManager>
    {

        [Header("Desk")]
        public DeskHud deskHud;

        [Header("GoodsViewerGroup")]
        public GoodsViewerGroup goodsGroup;

        public GameObject warring;
        
        
        public void ShowCantMove()
        {
        StartCoroutine(CoShowCantMove());
        }


        private IEnumerator CoShowCantMove()
        {
            warring.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            warring.gameObject.SetActive(false); 
        }

        public void RefreshDesk()
        {
            deskHud.Writ();
        }

    }

interface iEscape
{
    void OnEscape();
}

