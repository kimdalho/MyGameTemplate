using UnityEngine;


public enum eSkillAtiveType
{
    None = 0,
    When_I_Get_Only_One = 1, //임시이름 획득 즉시 바로 단한번 시전
    Attack = 2, //획득이후 전투시 지속 시전

}
public abstract class Skill
{
    [SerializeField]
    protected string skillName;
    [SerializeField]
    protected string description;

    [SerializeField]
    protected int value_1;
    protected int value_2;

    protected virtual void Use()
    {

    }

    public virtual void Show()
    {

    }
}
