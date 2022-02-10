using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hex_Package;
public class InvenPopup : BasePopup 
{

    public override void Setup()
    {
        base.Setup();

        GetBackButton().onClick.AddListener(UiManager.Instance.OnClickBackButton);
    }
}
