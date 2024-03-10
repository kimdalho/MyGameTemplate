using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackEventObserver
{
    public void Subject(int damage);
}

public class BloodSucking : Skill , IAttackEventObserver
{
    protected override void Use()
    {
        //계산 입힌 데미지가 10이면 흡혈량 0.5  이경우는 반올림으로 처리 => 1
        // value-1 / value-2
        int sucking = Mathf.RoundToInt((float)value_1 / (float)value_2);
        GameManager.Instance.player.ChangeCurrentHp(sucking);
        GameManager.Instance.UiRefresh();
    }

    public override void Show()
    {
        skillName = "흡혈";
        value_1 = 5;
        description = string.Format("적공격 시 입힌 피해 {0}%에 해당하는 수치를 흡혈합니다",value_1);
    }

    public void Subject(int damage)
    {
        value_2 = damage;
        Use();
    }
}
