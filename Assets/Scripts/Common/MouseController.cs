using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private Agent agent;
    private PathFindingManager pathMgr;
    public static bool onDrag;
    private readonly int PICK_OFFSET = 20;
    [SerializeField]
    private GameObject pin;
    // Start is called before the first frame update
    void Start()
    {
        pathMgr = PathFindingManager.Instance;
    }
    private void OnMouseDrag()
    {
        onDrag = true;
        MouseDrag();
    }

    

    public void SetData(Agent agent , GameObject _pin)
    {
        this.agent = agent;
        this.pin = _pin;
    }



    private void MouseDrag()
    {
        foreach (var hits in Physics2D.RaycastAll(Util.MousePos, Vector3.forward))
        {
            Node node = hits.collider?.GetComponent<Node>();
            if (node == null)
            {
                Debug.Log("NotFound!");
                continue;

            }

            pin.transform.localPosition = node.transform.localPosition;
            pin.transform.localPosition += Vector3.up * PICK_OFFSET;
            pathMgr.targetNode = node;
        }
    }

    private void OnMouseUp()
    {
        if (onDrag == true)
        {
            bool ExistPath = pathMgr.PlayerPathFinding();

            if (ExistPath == false)
            {
                pin.transform.localPosition = agent.nowNode.transform.localPosition;
                pin.transform.localPosition += Vector3.up * PICK_OFFSET;
                Debug.LogWarning("there is nothing to Path");
                return;
            }
            pathMgr.Move(ExistPath);
        }
    }
}
