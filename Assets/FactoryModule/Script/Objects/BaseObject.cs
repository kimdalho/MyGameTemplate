using UnityEngine;
/// <summary>
/// �ν��Ͻ̵Ǵ� ���
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
