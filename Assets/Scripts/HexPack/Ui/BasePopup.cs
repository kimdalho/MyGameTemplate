using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopup : UiBase , iEscape
{
    public enum ePopupTMPs
    {
        TitleTMP = 0,
    }
    
    public enum ePopupButtons
    {
        BackButton = 0,
    }

    public override void Setup()
    {
        Bind<Button>(typeof(ePopupButtons));
    }

    public Button GetBackButton ()
    {
        return Get<Button>((int)ePopupButtons.BackButton);
    }

    public void OnEscape()
    {
        gameObject.SetActive(false);
    }
}
