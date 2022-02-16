public class Quest_Mushroom : Quest
{
    public override void CreateInstanceData()
    {
        select1 = new Select();
        select2 = new Select();

        title = "당신은 버섯으로 지어진것처럼 보이는 마을을 조우했습니다 모든 인종이 평화로워 보입니다. 어떻게 하시겠습니까?";
        string select1_Text = "버섯마을을 약탈한다.";
        Reward select1_fixed = new Reward("마을이 모두 재건이 불가능해보일정도로 망가졌습니다.", FixedCallback);
        select1.WriteFixedEvent(select1_Text,select1_fixed);


        string select2_Text = "마을에서 휴식을 취한다.";
        Reward select2_fixed = new Reward("무사히 막사를 빠져나왔다.", FixedCallback);
        select2.WriteFixedEvent(select2_Text, select2_fixed);
    }


    public override void Test()
    {
        Util.Log("버섯을 출력합니다");
    }
}
