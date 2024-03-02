using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// a*를 적용할 대상에게 addComponet한다
/// </summary>
public class Agent : MonoBehaviour
{

    private readonly int PICK_OFFSET = 20;
    //위치
    public PRS originPRS;
    public Node nowNode;
    public static bool onDrag;
    [Header("Resource")]
    [SerializeField] private GameObject pickHandle;
    public void Setup(GameObject _pick)
    {
        pickHandle = _pick;
    }
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }


    private void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag1");

        //if (GameManager.Instance.status != eTurnType.PlayerTurn)
        //{
        //    Debug.Log($"{GameManager.Instance.gameObject.name}");
        //    return;
        //}
        Debug.Log("OnMouseDrag2");
        onDrag = true;
       AgentDrag();
    }

    private void AgentDrag()
    {

        foreach (var hit in Physics2D.RaycastAll(Util.MousePos, Vector3.forward))
        {
            Node node = hit.collider?.GetComponent<Node>();
            if (node == null)
            {
                Debug.Log("NotFound!");
                continue;

            }

            pickHandle.transform.localPosition = node.transform.localPosition;
            pickHandle.transform.localPosition += Vector3.up * PICK_OFFSET;
            PathFindingManager.Instance.targetNode = node;
        }
    }

    private void OnMouseUp()
    {
        if (onDrag == true)
        {
            bool onFindPath =  PathFindingManager.Instance.PathFinding();
            
            PathFindingManager.Instance.Move(onFindPath);
        }
    }

}
