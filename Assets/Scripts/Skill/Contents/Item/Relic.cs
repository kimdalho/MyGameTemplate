using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eRelicType
{
    GoldenHeartStatue = 1,
    MysticalMoonlightFist,
    EnchantedFormScroll,
    BurningStarTalisman,
    TimeGuardiansTalisman,
    ThunderousHolySword,
    FrozenSoulCrystal,
    MysteriousRuneGlyph,
    SwiftFootstepsTalisman,
    InfiniteWisdomCrystal
}


[System.Serializable]
public class Relic 
{
    public int id;
    public eRelicType type;
    public Sprite icon;
    public string name;
    public int grade;
    public UnitStat stat;
    public eSkillType skillType;
}

[System.Serializable]
public class UnitStat
{
    public int maxHp;
    public int curHp;
    public int atk;
    public int move;
    public int fullness;
}
