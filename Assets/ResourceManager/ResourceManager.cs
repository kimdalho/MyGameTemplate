using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����Ÿ�Կ� ���ذ� ������ȵȴ�.
/// ���� ���� �ε����� 0��Ұ��� �����÷� �����̴�.
/// </summary>


public class ResourceManager : MonoBehaviour
{    
    private static ResourceManager _instance;    
    /// <summary>
    /// ������ �б� Ÿ�Կ� ���� ���񽺰� �޶�����.
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
            // �÷��� ������ �����ϰ� ���� (Editor������)
            if (!Application.isPlaying)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ResourceManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("[ResourceManager Editor Instance]");
                        _instance = obj.AddComponent<ResourceManager>();
                        obj.hideFlags = HideFlags.HideAndDontSave; // �� ���� X
                    }
                }
                return _instance;
            }
#endif
            // �Ϲ� ��Ÿ�ӿ�
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
