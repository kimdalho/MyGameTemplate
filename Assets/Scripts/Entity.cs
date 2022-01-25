using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer entity;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] GameObject sleepParticle;

    public int attack;
    public int health;
    public bool isMine;
    public bool isBossOrEmpty;
    public bool attackable;
    //정렬을 위한 좌표 변수
    public Vector3 originPos;
    /// <summary>
    /// liveCount은 0일시 슬립, 1이상일시 공격가능
    /// </summary>
    int liveCount;

    public bool isDie;
    

    private void Start()
    {
        TurnManager.OnTurnStarted += OnTurnStarted;
    }

    private void OnDestroy()
    {
        TurnManager.OnTurnStarted -= OnTurnStarted;
    }

    //소환한 즉시 공격할수없다 Entity가 Spawn이후 한턴이 지났는지를 체크한다
    void OnTurnStarted(bool myTurn)
    {
        if (isBossOrEmpty)
            return;
        if (isMine == myTurn)
            liveCount++;

        sleepParticle?.SetActive(liveCount < 1);
    }


    public void Setup(Item item)
    {
        attack = item.attack;
        health = item.health;

        this.item = item;
        character.sprite = this.item.sprite;
        nameTMP.text = this.item.name;
        attackTMP.text = attack.ToString();
        healthTMP.text = health.ToString();
    }

    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime);
        else
            //원래 자리로 이동
            transform.position = pos;
    }

    public bool Damaged(int damage)
    {
        health -= damage;
        healthTMP.text = health.ToString();

        if(health <= 0)
        {
            isDie = true;
            return true;
        }
        return false;
    }

    void OnMouseDown()
    {
        if (isMine)
            EntityManager.Instance.EntityMouseDown(this);
    }

    private void OnMouseUp()
    {
        if (isMine)
            EntityManager.Instance.EntityMouseUp();
    }

    private void OnMouseDrag()
    {
        if (isMine)
            EntityManager.Instance.EntityMosueDrag();
    }
}
