using UnityEngine;
/// <summary>
/// 반드시 인스턴싱할 대상의 프리펩 정보가있어야한다.
/// </summary>
/// 
namespace UnityFactory
{
    public class BaseObejctInfo : ScriptableObject, IObjectInfo
    {
        public GameObject prefab => _prefab;
        private GameObject _prefab;
    }
}
