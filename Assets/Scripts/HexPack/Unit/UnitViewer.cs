using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
using TMPro;
using System;
using System.Threading.Tasks;

enum eSprites
{
    iconRender = 0,
    CircleMask = 1,
    CircleLine = 2,
    CircleAtk = 3,
    CircleHealth = 4,
    MarkRender = 5,
}


public class UnitViewer : UiBase
{
    [SerializeField]
    SpriteRenderer spRenderer;

    [SerializeField]
    private Sprite unitSprtie;

    enum eTMPs
    {
        AtkTMP = 0,
        HealthTMP = 0,
    }

    public override void Setup()
    {
        Bind<SpriteRenderer>(typeof(eSprites));
        Bind<TMP_Text>(typeof(eTMPs));
        spRenderer = Get<SpriteRenderer>((int)eSprites.iconRender);
        Get<SpriteRenderer>(5).gameObject.SetActive(false);
    }

    public void SetRender(Sprite render)
    {
        unitSprtie = render;
        spRenderer.sprite = unitSprtie;
    }

    public void End()
    {
        Get<SpriteRenderer>((int)eSprites.CircleLine).enabled = false;
        Get<SpriteRenderer>((int)eSprites.CircleAtk).gameObject.SetActive(false);
        Get<SpriteRenderer>((int)eSprites.CircleHealth).gameObject.SetActive(false);
        Get<SpriteRenderer>((int)eSprites.MarkRender).gameObject.SetActive(true);
    }

}