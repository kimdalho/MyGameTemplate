using UnityEngine.UI;
using TMPro;
/// <summary>
/// 1. 타워 유닛한정에서 등장한다.
/// 2. 퀘스트는 특정 이벤트를 통해서 플레이어에게 보상을 지급 또는 패널티를 부여한다.
/// </summary>
public class QuestPopup : BasePopup
{

     enum eButtons
    {
        Select1_Button = 0,
        Select2_Button = 1,
    }

    enum eTMPs
    {
        TitleTMP = 0,
        Select1_TMP = 1,
        Select2_TMP = 2,
    }

    public override void Setup()
    {
        Bind<Image>(typeof(eImages));
        Bind<Button>(typeof(eButtons));
        Bind<TextMeshProUGUI>(typeof(eTMPs));
    }

    public override void SetPopupData()
    {
        var item = UnitManager.Instance.targetUnit.item;
        var quest = QuestManager.Instance.cur_quest;
        Get<Image>(0).sprite = item.render;
        
        Get<TextMeshProUGUI>(0).text = quest.title;
        Get<TextMeshProUGUI>(1).text = quest.select1.title;
        Get<TextMeshProUGUI>(2).text = quest.select2.title;

        Get<Button>(0).onClick.AddListener(quest.select1.OnClickedButton);
        Get<Button>(1).onClick.AddListener(quest.select2.OnClickedButton);
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
