using UnityEngine;
/// <summary>
/// 인스턴싱되는 대상
/// </summary>
namespace UnityFactory
{
    public class BaseObject : MonoBehaviour, IObject
    {
        public virtual void SetData(IObjectConfigSO data)
        {

        }
    }
}
