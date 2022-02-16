using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Hex_Package;
/// <summary>
/// a*를 적용할 대상에게 addComponet한다
/// </summary>
public class Agent : MonoBehaviour
{
    //위치
    public PRS originPRS;
    public Node nowNode;

    public bool onMouseOver;

    public bool onDrag;

    public void MoveTranform(PRS prs, bool useDotween, float dotweenTime = 0)
    {

    }

    private void OnMouseOver()
    {
        onMouseOver = true;
    }

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
        /*
         1. SelectTile이 존재하면
         PathFindingManager에서 Movement를 호출
         */
    }

}
