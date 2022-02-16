using UnityEngine;
using System.Collections.Generic;




/// <summary>
///1.Position 
///2.Rotation 
///3.LocalScale
/// </summary>
[System.Serializable]
public class PRS
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;

    public PRS(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
    }
}


/**
 * 규칙 
 * 1. Util에서 선언된 변수는 상수와 같이 고정적으로 변화하지않는 값을 선언한다
 * 
 * 
 * 
 */
public class Util
{
    public static Quaternion QI = Quaternion.identity;

    public enum eTopUiType
    {
        None = 0,
        Move = 1,
        Gold = 2,
        Fame = 3,
    }

    public static Vector3 MousePos
    {
        get
        {
            Vector3 resut = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            resut.z = -10;
            return resut;
        }
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }


    //########Log################################################
    public static void Log(object _object )
    {
        Debug.Log(_object);
    }

    public static void LogWarning(object _object)
    {
        Debug.LogWarning(_object);
    }

    public static void LogError(object _object)
    {
        Debug.LogError(_object);
    }
    //########Log################################################

/*    public static  List<T> Shuffle<T>(this List<T> list)
    {
        for (int i =  list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;

        }

        return list;
    }*/

}