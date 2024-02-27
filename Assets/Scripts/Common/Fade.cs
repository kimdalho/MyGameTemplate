using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public enum eFadeType
{
    Stop = 0,
    Stop_Aad_Disable = 1,
}

public class Fade : MonoBehaviour
{
    [Header("팝업 페이드")]
    [SerializeField]
    private Image img_popup;
    [SerializeField]
    private Color startcolor;
    [SerializeField]
    private Color fadecolor;
    [SerializeField]
    private float fadedelay;

    public void FadeOut(Action endaction)
    {
        img_popup.DOColor(startcolor, fadedelay).OnComplete(()=> { endaction.Invoke(); });
    }

    public void FadeIn(eFadeType statuse = eFadeType.Stop_Aad_Disable)
    {
        switch(statuse)
        {
            case eFadeType.Stop:
                img_popup.DOColor(fadecolor, fadedelay);
                break;
            case eFadeType.Stop_Aad_Disable:
                img_popup.DOColor(fadecolor, fadedelay).OnComplete(() => { this.gameObject.SetActive(false); });
                break;
        }

    }

    public void FadeIn(Action endaction)
    {
        img_popup.DOColor(fadecolor, fadedelay).OnComplete(() => { endaction.Invoke(); });

    }
}
