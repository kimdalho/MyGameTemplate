using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hex_Package;
public class Tower : Unit
{
    public override void Evnet()
    {
        UiManager.Instance.ShowEventPopup();   
    }
}
