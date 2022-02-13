using Hex_Package;
using System;
public class Quest
{
    public string title;
  
    public const int RWARD_NONE = 0;
    public const int REWARD_GOLD = 1;
    public const int REWARD_FEME = 2;
    public const int RWARD_UNIT = 3;

    public Select select1;
    public Select select2;

    public virtual void WriteQuset()
    {
        Test();
    }


    public virtual void Test()
    {
        Util.Log("기본을 출력합니다");
    }

    public virtual void WinCallback()
    {
        //승리;
    }

    public virtual void LoseCallback()
    {

    }

    public virtual void FixedCallback()
    {

    }
}
/// <summary>
/// 퀘스트가 가지고있는 선택지의 1. 텍스트 데이터, 2.이벤트 3.이벤트의 결과에 맞는 보상의 정보를 가지고있다.
/// 이벤트는 전투 퍼센트
/// </summary>
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
    //무슨선택을 했는지 문장으로 나타낸다
    public eSelectType selectType;

    public string title;

    //? CurReward
    public Reward winReward;
    public Reward loseReward;
    public Reward fiexedReward;

    public int percent;

    public void WriteBattleEvent(string title, Reward win ,Reward lose)
    {
        this.title = title;
        selectType = eSelectType.Battle;
        winReward = win;
        loseReward = lose;
    }

    public void WritePercentEvent(string title, int percent , Reward win, Reward lose)
    {
        this.title = title;
        this.percent = percent;
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

    public void OnClickedButton()
    {
        switch(selectType)
        {
            case eSelectType.Battle:
                BattleManager.Instance.RequsetBattle();
                break;
            case eSelectType.Percent:
                int rnd = UnityEngine.Random.Range(0, 100);
                var curReward = percent < rnd ? winReward : loseReward;
                QuestManager.Instance.cur_Reward = curReward;
                curReward.Use();
                break;
            case eSelectType.FixedResult:
                QuestManager.Instance.cur_Reward = fiexedReward;
                fiexedReward.Use();
                break;
        }
    }

}

//리워드는 존재할필요가 없을수도있다
//존재하지않는 객체라면 수치를 이미 Quest에서 가지고있어야한다.
public class Reward
{
    public string rewardTitle;
    /// <summary>
    /// eType이 유닛일 경우 result는 Id로 명시된다
    /// </summary>
    public int result;
    public Action callback;

    public Reward( string title , Action callback)
    {
        rewardTitle = title;
        callback.Invoke();
    }

    public void Use()
    {
        UiManager.Instance.ShowRewardPopup();
    }

    public void SetRewardPopupData()
    {
       
    }

}