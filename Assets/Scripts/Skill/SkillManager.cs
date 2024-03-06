using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSkillType
{
    None = 0,
    BloodSucking = 1,
}

public class SkillManager : Singleton<SkillManager>
{
    public List<IAttackEventObserver> when_player_attack= new List<IAttackEventObserver>();

    public List<Skill> skilltype;


    public void WhenPlayerAttack(int damage)
    {
        foreach(var observer in when_player_attack )
        {
            observer?.Subject(damage);
        }
    }

}
