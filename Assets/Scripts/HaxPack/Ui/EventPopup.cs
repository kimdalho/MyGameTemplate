using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EventPopup : UiBase
{
    enum eImages
    {
        Frame = 0,
    }

    enum eButtons
    {
        Choice1_Button = 0,
        Choice2_Button = 1,
    }

    enum eTMPs
    {
        TitleTMP = 0,
        Choice1_TMP = 1,
        Choice2_TMP = 2,
    }

    public override void Setup()
    {
        Bind<Image>(typeof(eImages));
        Bind<Button>(typeof(eButtons));
        Bind<TextMeshProUGUI>(typeof(eTMPs));

        
    }

    public void SetEventData()
    {
        var item = PathFindingManager.Instance.targetNode.unit.item;
        Get<Image>(0).sprite = item.render;
        Get<TextMeshProUGUI>(0).text = item.envet.title_str;
        Get<TextMeshProUGUI>(1).text = item.envet.choice1_str;
        Get<TextMeshProUGUI>(2).text = item.envet.choice2_str;
        Get<Button>(0).onClick.AddListener(ClosePopup);
        Get<Button>(1).onClick.AddListener(ClosePopup);
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
