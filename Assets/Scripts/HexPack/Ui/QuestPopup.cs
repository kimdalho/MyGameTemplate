using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine;
/// <summary>
/// 1. 타워 유닛한정에서 등장한다.
/// 2. 퀘스트는 특정 이벤트를 통해서 플레이어에게 보상을 지급 또는 패널티를 부여한다.
/// </summary>
public class QuestPopup : EventPopup
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

    public Image GetRenderImage()
    {
        return Get<Image>(0);
    }

    public TextMeshProUGUI GetTitleTMP()
    {
        return Get<TextMeshProUGUI>(0);
    }

    public TextMeshProUGUI GetTMP1()
    {
        return Get<TextMeshProUGUI>((int)eTMPs.Select1_TMP);
    }

    public TextMeshProUGUI GetTMP2()
    {
        return Get<TextMeshProUGUI>((int)eTMPs.Select2_TMP);
    }

    public Button GetButton(int number)
    {
        if (number < 0 || number > 1)
            return null;

        return Get<Button>(number);
    }

    protected override IEnumerator CoAppearTitle(string title)
    {
        var titleText = GetTitleTMP();
        char[] charList = title.ToCharArray();
        for (int i = 0; i < charList.Length; i++)
        {
            titleText.text += charList[i];
            yield return new WaitForSeconds(0.1f);
        }
        titleText.text = title;
        GetButton(0).enabled = true;
        GetButton(1).enabled = true;
    }

    public void StartAppearTitle(string title)
    {
        ShowQuestPopup();
        StartCoroutine(CoAppearTitle(title));
    }
    public void ShowQuestPopup()
    {
        GetTitleTMP().text = "";
        GetButton(0).enabled = false;
        GetButton(1).enabled = false;
        gameObject.SetActive(true);
    }

    public void EventRemove()
    {
        GetButton(0).onClick.RemoveAllListeners();
        GetButton(1).onClick.RemoveAllListeners();
    }

}
