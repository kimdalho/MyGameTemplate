public class Quest_Lodge : Quest
{
    public override void CreateInstanceData()
    {
        select1 = new Select();
        select2 = new Select();

        title = "평범하지만 수상한 집을 발견했습니다 어떻게 하시겠습니까?";

        string select1_Text = "문을 발로 차고 기습한다!";
        Reward select1_win = new Reward("수상한집의 정체는 도적단의 비밀 아지트였습니다. \n 당신은 골드를 획득했습니다.", WinCallback);
        Reward select1_lose = new Reward("전투에 패배하여 당신은 부상을 입었습니다.", LoseCallback);
        select1.WriteBattleEvent(select1_Text, select1_win, select1_lose);


        string select2_Text = "도망친다";
        Reward select2_fixed = new Reward("무사히 막사를 빠져나왔다.", FixedCallback);
        select2.WriteFixedEvent(select2_Text, select2_fixed);
    }

    public override void Test()
    {
        Util.Log("외딴 별장을 출력합니다");
    }
}
