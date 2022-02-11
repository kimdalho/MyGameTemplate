using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Hex_Package;
public class CardShowArea : BaseShowArea
{

    int showValue = 0;
    int hideValue = -26;


    private void Start()
    {
        TileCardManager.OnEndLoading += Hide;
    }

    private void OnDestroy()
    {
        TileCardManager.OnEndLoading -= Hide;
    }
    protected override void DetectShowArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        int layer = LayerMask.NameToLayer(targetLayer_Name);
        //hits를 x라는 컨테이너 매치
        onShowArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }


    protected override void Show()
    {
        targetTranform.transform.DOMoveY(showValue, 0.5f);
    }

    protected override void Hide()
    {
        targetTranform.transform.DOMoveY(hideValue, 0.5f);
    }
}
