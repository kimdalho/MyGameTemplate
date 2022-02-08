using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public string title_str;
    public string choice1_str;
    public string choice2_str;

    public Reward choic1_reward_win;
    public Reward choic1_reward_lose;

    public Reward choic2_reward;

}
[System.Serializable]
public class Reward
{
    public string rewardTitle;


    public enum eRewardType
    {
        부,
        명예,
        유닛,
    }
    public eRewardType eType;
    /// <summary>
    /// eType이 유닛일 경우 result는 Id로 명시된다
    /// </summary>
    public int result;

}
