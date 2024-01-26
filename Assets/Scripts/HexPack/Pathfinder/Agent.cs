using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// a*를 적용할 대상에게 addComponet한다
/// </summary>
public class Agent : MonoBehaviour
{
    //위치
    public PRS originPRS;
    public Node nowNode;
    public static bool onDrag;

    private void OnMouseDrag()
    {
       onDrag = true;
       PathFindingManager.Instance.AgentDrag();
    }

    private void OnMouseUp()
    {
        if(onDrag == true)
        {
            bool onFindPath =  PathFindingManager.Instance.PathFinding();
            
            PathFindingManager.Instance.Move(onFindPath);
        }
    }

}
