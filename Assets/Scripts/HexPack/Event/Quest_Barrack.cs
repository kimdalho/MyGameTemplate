public class Quest_Barrack : Quest
{
    public override void WriteQuset()
    {
        select1 = new Select();
        select2 = new Select();

        title = "전투민족의 막사가 보입니다 어떻게 하시겠습니까?";

        string select1_Text = "싸운다";
        Reward select1_win = new Reward(RWARD_UNIT, "가장강했던 전사가 동료가되었다.", 0);
        Reward select1_lose = new Reward(REWARD_FEME, "도망가는모습에 모두에게 비웃음을 샀다.", -1);

        select1.WriteBattleEvent(select1_Text, select1_win, select1_lose);


        string select2_Text = "도망친다";
        Reward select2_fixed = new Reward(RWARD_NONE, "무사히 막사를 빠져나왔다.", 0);
        select2.WriteFixedEvent(select2_Text, select2_fixed);
    }

}
