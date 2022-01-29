using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Hex_Package
{
    public class TileCard : UiBase
    {

        //인스펙터에서 초기화 진행 춫후 자동으로 초기화 코드 제작 필요
        [SerializeField] SpriteRenderer card;
        [SerializeField] SpriteRenderer character;
        [SerializeField] Sprite cardFront;
        //위치
        public PRS originPRS;

        public enum eTMP_Texts
        {
            NameTMP,
            AttackTMP,
            HealthTMP,
            SkillTMP,
        }

        public override void Setup()
        {
            Bind<TMP_Text>(typeof(eTMP_Texts));
        }

        public TileCardItem item;

        public void CardSetup(TileCardItem item)
        {
            this.item = item;

            character.sprite = item.sprite;
            card.sprite = cardFront;
            GetNameTMP().text = item.name;
            GetAttackTMP().text = item.attack.ToString();
            GetHealthTMP().text = item.health.ToString();
            GetSkillTMP().text = item.skill_description.ToString();

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

        public TMP_Text GetSkillTMP()
        {
            return Get<TMP_Text>((int)eTMP_Texts.SkillTMP);
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


        private void OnMouseOver()
        {
            TileCardManager.Instance.CardMouseOver(this);
        }


        private void OnMouseExit()
        {
            TileCardManager.Instance.CardMosueExit(this);
        }

        private void OnMouseDown()
        {
            TileCardManager.Instance.CardMouseDown();
        }

        private void OnMouseUp()
        {
            TileCardManager.Instance.CardMouseUp();
        }

    }
}
