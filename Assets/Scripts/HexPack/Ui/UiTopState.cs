using TMPro;
public class UiTopState : UiBase
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
