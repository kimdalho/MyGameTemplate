using UnityEngine;
/// <summary>
/// �ݵ�� �ν��Ͻ��� ����� ������ �������־���Ѵ�.
/// </summary>
/// 
namespace UnityFactory
{
    public class BaseObejctInfo : ScriptableObject, IObjectConfigSO
    {
        public GameObject prefab => _prefab;
        private GameObject _prefab;
    }
}
