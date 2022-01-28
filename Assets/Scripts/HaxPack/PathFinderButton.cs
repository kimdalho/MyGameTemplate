using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PathFinderButton : UiBase
{

    enum eButtons
    {
        PathFinderButton = 0,
    }
    public override void Setup()
    {

    }

    public Button GetButton()
    {
        return GetComponent<Button>();
    }
}
