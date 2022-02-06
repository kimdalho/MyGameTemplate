using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveScoreTMP : UiBase
{

    enum eTMPs
    {
        TMP = 0,
    }


    public override void Setup()
    {
        Bind<TextMeshProUGUI>(typeof(eTMPs));
    }

    public TextMeshProUGUI GetTMP()
    {
        return Get<TextMeshProUGUI>(0);
    }
}
