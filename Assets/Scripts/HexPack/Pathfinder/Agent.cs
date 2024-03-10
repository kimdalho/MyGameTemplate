using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
/// <summary>
/// a*를 적용할 대상에게 addComponet한다
/// </summary>
public class Agent : MonoBehaviour
{
    //위치
    public Node nowNode;
    public static bool onDrag;
    [Header("Resource")]
    [SerializeField] private GameObject pickHandle;

    private PathFindingManager pathMgr;
    public void Setup(GameObject _pick)
    {
        pickHandle = _pick;
        pathMgr = PathFindingManager.Instance;
    }
}
