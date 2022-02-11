using System;
using System.Collections.Generic;
using System.Reflection;

public class QuestManager : Singleton<QuestManager>
{
    IDictionary<EQuset, Type> mTypes;


    public enum EQuset
    {
        Quest_Barrack = 0,
        Quest_Mushroom = 1,
    }

    public Quest cur_quest;

    private void Start()
    {
        NameLess("Quest_Barrack");
    }
    public void NameLess(string prefix)
    {
        
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes())
        {
            if (type.Name.Contains("Quest_"))
            {
                Util.Log(type.Name);
            }
        }
        BuildDictionary(prefix, out mTypes);


    }

    void BuildDictionary(string prefix, out IDictionary<EQuset, Type> types)
    {
        var enumType = typeof(EQuset);
        var values = Enum.GetValues(enumType);

        var namespacePath = enumType.Namespace;
        if (null == namespacePath)
        {
            //Log.Warning(string.Format("namespacePath is Null {0}", enumType.Namespace));
            namespacePath = string.Empty;
        }
        types = new Dictionary<EQuset, Type>(values.Length);
        //Enum을 순회한다
        foreach (object value in values)
        {
            //none ~ Max
            //Log.Warning(string.Format("값 {0}",value));

            var name = Enum.GetName(enumType, value);

            if (null == name)
            {
                // Log.Warning(string.Format("name is Null value {0} enumType", value , enumType));
                continue;
            }

        }
    }
}
