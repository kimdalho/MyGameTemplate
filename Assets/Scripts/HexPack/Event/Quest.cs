using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    /// <summary>
    /// 무슨 퀘스트인가 문장형식으로 나타낸다.
    /// </summary>
    public string title;
  
    public const int RWARD_NONE = 0;
    public const int REWARD_GOLD = 1;
    public const int REWARD_FEME = 2;
    public const int RWARD_UNIT = 3;

    /// <summary>
    /// 1. 버튼을 선택했을시 일어날일
    /// 2. 버튼에 적히게될 문장
    /// </summary>
    public Select select1;
    public Select select2;

    public virtual void WriteQuset()
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

public class Select
{
    //타겟 선택지가 무슨일을 일으킬지 타입으로 나타낸다.
    public enum eSelectType
    {
        None = 0,
        Battle = 1,
        Percent = 2,
        FixedResult = 3,
    }
    public eSelectType selectType;
    //무슨선택을 했는지 문장으로 나타낸다
    public string title;
    public Reward winReward;
    public Reward loseReward;
    public Reward fiexedReward;


    public void WriteBattleEvent(string title, Reward win ,Reward lose)
    {
        this.title = title;
        selectType = eSelectType.Battle;
        winReward = win;
        loseReward = lose;
    }

    public void WritePercentEvent(int percent , Reward win, Reward lose)
    {
        selectType = eSelectType.Percent;
        winReward = win;
        loseReward = lose;
    }

    public void WriteFixedEvent(string title, Reward fiexed)
    {
        this.title = title;
        selectType = eSelectType.FixedResult;
        fiexedReward = fiexed;
    }


}

//리워드는 존재할필요가 없을수도있다
//존재하지않는 객체라면 수치를 이미 Quest에서 가지고있어야한다.
public class Reward
{
    public string rewardTitle;


    public enum eRewardType
    {
        GOLD,
        FEME,
        UNIT,
    }
    public eRewardType eType;
    /// <summary>
    /// eType이 유닛일 경우 result는 Id로 명시된다
    /// </summary>
    public int result;

    public Reward(int rewardType, string title , int resultValue)
    {
        rewardTitle = title;
        eType = (eRewardType)rewardType;
        result = resultValue;
    }


}
