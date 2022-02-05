using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Object/Skill/CardSkillItemSO")]
public class SkillSO : ScriptableObject
{
    public enum eCardSkillStat
    {
        CardSkill_Unit = 0,
        CardSkill_Score = 1,
        CardSkill_Fame = 2,
        CardSkill_Pillage = 3,
    }

    public SkillItem[] skillItem;
}

[System.Serializable]
public class SkillItem
{
    public string name;
    public int value;
    public SkillSO.eCardSkillStat Stat;
}
