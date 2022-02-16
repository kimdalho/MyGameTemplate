using System;
using System.Collections.Generic;
using System.Reflection;
using Hex_Package;

public class QuestManager : Singleton<QuestManager>
{
    IDictionary<EQusetType, Type> mTypes;


    public enum EQusetType
    {
        None = 0,
        Quest_Barrack = 1,
        Quest_Mushroom = 2,
        Quest_Tower = 3,
        Quest_Clocktower = 4,
        Quest_Lodge = 5,
    }

    public Reward cur_Reward;

    private void Start()
    {
        BuildDictionary(out mTypes);
    }


    public void PrintQuests()
    {
        //내 어셈블리를 가져온다.
        var assembly = Assembly.GetExecutingAssembly();
        //모든 타입스를 긁어온다
        foreach (var type in assembly.GetTypes())
        {
            //앞 클래스의 이름에 Quest_가 붙은 클래스를 추출한다.
            if (type.Name.Contains("Quest_"))
            {
                //출력한다.
               Util.Log(type.Name); 
            }
        }
        BuildDictionary(out mTypes);
    }

    private void BuildDictionary( out IDictionary<EQusetType, Type> mTypes)
    {
        //enum을 Type형식으로 바꾼다.
        var enumType = typeof(EQusetType);
        //enum의 갯수를 출력하기위해 Values로 변수를 만든다
        var values = Enum.GetValues(enumType);
        //딕셔너리의 데이터 초기화 최대값 Length로 지정한다.
        mTypes = new Dictionary<EQusetType, Type>(values.Length);

        //Enum을 하나씩 objectType으로 긁어온다.
        foreach (object value in values)
        {
            //none ~ Max
            //Log.Warning(string.Format("값 {0}",value));

            //무슨이넘 타입이며, 몇번째 값을불러올지 정한다.
            var name = Enum.GetName(enumType, value);

            if (null == name)
            {
                // Log.Warning(string.Format("name is Null value {0} enumType", value , enumType));
                continue;
            }
            var type = CreateType(name);
            if (null == type)
            {
                //  Log.Warning(string.Format("BuildDictionary type {0}", type));
                continue;
            }
            
            mTypes.Add((EQusetType)value, type);
        }
    }

    private Type CreateType(string name)
    {
        if (null == name)
        {
            return null;
        }

        //Log.Warning(string.Format("path {0}",path));
        var type = Type.GetType(name);
        //Log.Warning(string.Format("CreateType type {0}", type));
        return type;
    }

    public void CreateQuest(EQusetType questType)
    {
        if (questType == EQusetType.None)
            return;

        Type type = mTypes[questType];
        Quest NewQuest = (Quest)Activator.CreateInstance(type);
        if (NewQuest == null)
            return;

        SkillCardManager.Instance.OnOtherEventState();

        NewQuest.CreateInstanceData();
        UiManager.Instance.ShowQuestPopup(NewQuest);
    }
}
