public class Quest_Tower : Quest
{
    public override void WriteQuset()
    {
        select1 = new Select();
        select2 = new Select();

        title = "전투민족의 막사가 보입니다 어떻게 하시겠습니까?";

        string select1_Text = "싸운다";
        Reward select1_win = new Reward("가장강했던 전사가 동료가되었다.", WinCallback);
        Reward select1_lose = new Reward("도망가는모습에 모두에게 비웃음을 샀다.", WinCallback);

        select1.WriteBattleEvent(select1_Text, select1_win, select1_lose);


        string select2_Text = "도망친다";
        Reward select2_fixed = new Reward("무사히 막사를 빠져나왔다.", WinCallback);
        select2.WriteFixedEvent(select2_Text, select2_fixed);
    }

    public override void Test()
    {
        Util.Log("마을을 출력합니다");
    }

}
