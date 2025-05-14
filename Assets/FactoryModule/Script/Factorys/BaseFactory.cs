using System.Collections.Generic;
using UnityEngine;

namespace UnityFactory
{
    public class BaseFactory<TInfo, TObject> : MonoBehaviour, IFactory<TInfo, TObject>
        where TInfo : IObjectConfigSO
        where TObject : MonoBehaviour, IObject
    {
        [SerializeField]
        protected List<TInfo> configSOs;

        protected void Awake()
        {
            if (configSOs == null)
            {
                Debug.LogError($"{this} => configSOs null");
            }
            else if (configSOs.Count <= 0)
            {
                Debug.LogError($"{this} => configSOs Count zero");
            }
        }

        public virtual TObject Create(TInfo info, Transform parent)
        {
            GameObject instance = Instantiate(info.prefab, parent);
            TObject obj = instance.GetComponent<TObject>();
            obj.SetData(info);
            return obj;
        }
    }
}
