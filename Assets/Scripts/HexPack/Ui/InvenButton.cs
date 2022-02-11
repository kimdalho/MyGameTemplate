using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hex_Package;
public class InvenButton : BasePopup
{
    public override void Setup()
    {
        GetComponent<Button>().onClick.AddListener(UiManager.Instance.OnClickInvenButton);
    }
}
