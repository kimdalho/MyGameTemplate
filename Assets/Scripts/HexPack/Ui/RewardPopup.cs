using UnityEngine.UI;
using TMPro;
using Hex_Package;
using System;

public class RewardPopup : EventPopup
{
    enum eButtons
    {
        Select1_Button = 0,
    }

    enum eTMPs
    {
        TitleTMP = 0,
        Select1_TMP = 1,
    }
    public override void Setup()
    {
        Bind<Image>(typeof(eImages));
        Bind<Button>(typeof(eButtons));
        Bind<TextMeshProUGUI>(typeof(eTMPs));
    }

    public void SetPopupData()
    {
        var item = UnitManager.Instance.targetUnit.item;
        var quest = QuestManager.Instance.cur_Reward;
        Get<Image>(0).sprite = item.render;

        Get<TextMeshProUGUI>(0).text = quest.rewardTitle;
        Get<TextMeshProUGUI>(1).text = "닫기";
        //계속 추가됨
        Get<Button>(0).onClick.AddListener(InvokeCallback);
    }

    private void InvokeCallback()
    {
        QuestManager.Instance.cur_Reward.callback?.Invoke();
        UiManager.Instance.HideRewardPopup();
    }

    public void EventRemove()
    {
        Get<Button>(0).onClick.RemoveAllListeners();
    }
}
