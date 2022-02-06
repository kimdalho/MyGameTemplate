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

    private void AgentDrag()
    {
        //플러그 필요
        foreach (var hit in Physics2D.RaycastAll(Util.MousePos, Vector3.forward))
        {
            Node node = hit.collider?.GetComponent<Node>();
            if (node == null)
                continue;
            Util.Log(string.Format("{0}", node.gameObject.name));

            GameManager.Instance.Pick.transform.localPosition = node.transform.localPosition;
            GameManager.Instance.Pick.transform.localPosition += Vector3.up * 20;
        }
    }

    private void OnMouseDrag()
    {
        AgentDrag();
    }

    private void OnMouseUp()
    {
        /*
         1. SelectTile이 존재하면
         PathFindingManager에서 Movement를 호출
         */
    }

}
