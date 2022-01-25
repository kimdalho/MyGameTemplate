using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Card : UiBase
{

    //인스펙터에서 초기화 진행 춫후 자동으로 초기화 코드 제작 필요
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;
    //위치
    public PRS originPRS;

    public enum eTMP_Texts
    {
        NameTMP,
        AttackTMP,
        HealthTMP,
    }

    public override void Setup()
    {
        Bind<TMP_Text>(typeof(eTMP_Texts));
    }

    public Item item;
    bool isFront;

    public void CardSetup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if (this.isFront)
        {
            //2022_01_25
            //character.sprite = item.sprite;

            card.sprite = cardFront;
            GetNameTMP().text = item.name;
            GetAttackTMP().text = item.attack.ToString();
            GetHealthTMP().text = item.health.ToString();
        }
        else
        {
            //character.sprite = null;

            card.sprite = cardBack;
            GetNameTMP().text = "";
            GetAttackTMP().text = "";
            GetHealthTMP().text = "";

        }

    }

    public TMP_Text GetNameTMP()
    {
        return Get<TMP_Text>((int)eTMP_Texts.NameTMP);
    }

    public TMP_Text GetAttackTMP()
    {
        return Get<TMP_Text>((int)eTMP_Texts.AttackTMP);
    }

    public TMP_Text GetHealthTMP()
    {
        return Get<TMP_Text>((int)eTMP_Texts.HealthTMP);
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }


    // 콜라이더 필요
    private void OnMouseOver()
    {
        if (isFront)
            CardManager.Instance.CardMouseOver(this);
        ;
    }


    private void OnMouseExit()
    {
        if (isFront)
            CardManager.Instance.CardMouseExit(this);
    }

    private void OnMouseDown()
    {
        if (isFront)
            CardManager.Instance.CardMouseDown();
    }

    private void OnMouseUp()
    {
        if (isFront)
            CardManager.Instance.CardMouseUp();
    }
}
