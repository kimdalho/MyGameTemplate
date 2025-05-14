using UnityEngine;

namespace UnityFactory
{
    public class BaseFactory<TInfo, TObject> : MonoBehaviour, IFactory<TInfo, TObject>
        where TInfo : IObjectInfo
        where TObject : MonoBehaviour, IObject
    {
        public virtual TObject Create(TInfo info, Transform parent)
        {
            GameObject instance = Instantiate(info.prefab, parent);
            TObject obj = instance.GetComponent<TObject>();
            obj.SetData(info);
            return obj;
        }
    }
}
