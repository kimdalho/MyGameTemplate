using UnityEngine;

[System.Serializable]


/**
 ##################################################################*
   카드의 타입 종류는 다음과 같다
 *1. 매 내턴이 될때마다 드로우 받는 '기본'카드
 *2. 성을 탈취하거나 전투 등 보상으로만 얻을수있는 '보상' 카드
 *
 *보상카드만이 들어있는 버퍼역학의 가상리스트가 존재해야한다
 *기본카드만이 들어있는 버퍼역학의 가상리스트가 존재해야한다
 *
 #####################################################################*/
public class TileCardItem
{
    public enum eType
    {
        DecType = 0,
        ResultType = 1,
    }

    public string name;
    public int attack;
    public int health;
    public Sprite sprite;
    public string skill_description;
    public Skill skill;
    public eType type;

}



[CreateAssetMenu(fileName = "TileCardItemSO", menuName = "Scriptable Object/Hex/TileCardItemSO")]
public class TileCardItemSO : ScriptableObject
{
    public TileCardItem[] items;
}
