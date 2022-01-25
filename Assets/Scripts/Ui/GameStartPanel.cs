using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameStartPanel : UiBase
{
    enum eButtons
    {
        GameStartButton = 0,
    }

    enum eTMP_texts
    {
        GameStartButton = 0,
    }



    public override void Setup()
    {
       Bind<Button>(typeof(eButtons));
    }

    //##########Get#############

    public Button GetGameStartButton()
    {
        return Get<Button>((int)eButtons.GameStartButton);
    }

    //##########Get#############

}
