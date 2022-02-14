public class Quest_Clocktower : Quest
{
    public override void WriteQuset()
    {
        select1 = new Select();
        select2 = new Select();

        title = "신비로운 시계탑을 발견했습니다. 어떻게 하시겠습니까?";

        string select1_Text = "시계를 만진다";
        Reward select1_win = new Reward("시계의 신비로운 힘이 미래를 보여주었습니다.", WinCallback);
        Reward select1_lose = new Reward("시계의 신비로운 힘이 당신의 힘을 빼았아갔습니다.",LoseCallback);
        select1.WritePercentEvent(select1_Text,50, select1_win, select1_lose);
        string select2_Text = "시계를 파괴하고 부품을 챙긴다.";
        Reward select2_fixed = new Reward("시계를 파괴하고 조금의 돈이될만한것을 챙겼습니다.", FixedCallback);
        select2.WriteFixedEvent(select2_Text, select2_fixed);
    }

    public override void WinCallback()
    {
        base.WinCallback();
    }


    public override void Test()
    {
        Util.Log("시계탑을 출력합니다");
    }
}
