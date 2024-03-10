using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Creature : EnemyUnit
{
    public eCretureType crtureType;
    public Agent agent;
    private PathFindingManager pathMgr;
    [SerializeField] private List<Node> FinalList = new List<Node>();
    public int moveScore = 3;
    private readonly float MOVING_DURATION = 0.3f;
    public override void SetData(UnitItem item, bool isFront, Node parent)
    {
        base.SetData(item, isFront, parent);
        crtureType = item.cretureType;
        agent =  this.gameObject.AddComponent<Agent>();

        UnitManager.Instance.units.Add(this);
        agent.nowNode = parent;
        pathMgr = PathFindingManager.Instance;


    }

    public void AnimPlay()
    {
        if (moveScore <= 0 || FinalList.Count <= 0)
            return;

     moveScore--;
     agent.transform.DOMove(FinalList[0].offsetPos, MOVING_DURATION, true)
    .SetEase(Ease.OutCubic)
    .OnComplete(() =>
    {
        agent.nowNode.isWall = false;
        agent.nowNode.unit = null;
        agent.nowNode = FinalList[0];
        agent.nowNode.unit = this;
        agent.nowNode.isWall = true;
        parent = agent.nowNode;
        FinalList.RemoveAt(0);
        AnimPlay();
    });
    }

    public void Move()
    {
        moveScore = 3;
        pathMgr.SetEnemyAgent(unitName, agent, agent.nowNode);
        pathMgr.targetNode = GameManager.Instance.player.GetNowNode();
        Tuple<bool , List<Node>> isGood = pathMgr.EnemyPathFinding(moveScore);

        if (isGood == null)
            return;

        if (isGood.Item1)
        {
            
            FinalList.Clear();
            FinalList = isGood.Item2.ToList();

            if (FinalList.Count <= 0)
                return;

            if (FinalList.Count > moveScore)
            {
                FinalList.RemoveRange(moveScore, FinalList.Count - moveScore);
            }

            

            FinalList[FinalList.Count - 1].isWall = true;
            Debug.Log("찾았다!");

        }

        Debug.Log($"이름: {unitName} 시작좌표 {parent.gameObject.name}");

        for (int i = 0; i < FinalList.Count; i++ )
        {
            Debug.Log($"이름: {unitName} 인덱스 {i} {FinalList[i].gameObject.name}");
        }


    }

    public override Node GetTileOffSetPos()
    {
        return parent;
    }

    public override void Dead()
    {
        base.Dead();
        UnitManager.Instance.units.Remove(this);
        GameManager.Instance.buffer.Remove(hud.slider);
        parent.isWall = false;
    }
}
