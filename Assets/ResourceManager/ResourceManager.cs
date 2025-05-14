using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 열거타입에 오해가 있으면안된다.
/// 모델의 웨폰 인덱스는 0요소값에 라이플로 시작이다.
/// </summary>


public class ResourceManager : MonoBehaviour
{    
    private static ResourceManager _instance;    
    /// <summary>
    /// 공급자 분기 타입에 따라 서비스가 달라진다.
    /// </summary>
    private static IResourceProvider _provider;

    [SerializeField]
    public List<AnimatorClipInfo> list = new List<AnimatorClipInfo>();  

    public ItemFactory ItemFactory;    


    public static ResourceManager Instance
    {
        get
        {
#if UNITY_EDITOR
            // 플레이 전에도 동작하게 만듦 (Editor에서만)
            if (!Application.isPlaying)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ResourceManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("[ResourceManager Editor Instance]");
                        _instance = obj.AddComponent<ResourceManager>();
                        obj.hideFlags = HideFlags.HideAndDontSave; // 씬 저장 X
                    }
                }
                return _instance;
            }
#endif
            // 일반 런타임용
            if (_instance == null)
            {
                GameObject obj = new GameObject("[ResourceManager]");
                _instance = obj.AddComponent<ResourceManager>();
                
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        _instance = this;

    }

    public ItemObject GetRandomItem(Transform parent)
    {
       return ItemFactory.CreateRandomItem(parent);
    }
}
