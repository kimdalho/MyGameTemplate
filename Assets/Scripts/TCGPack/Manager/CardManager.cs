using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/*
 * 역할
 * 1. 모든 카드 DB
 * 2. 플레리어의 카드 리스트 
 * 3. 적의 카드 리스트
 * 4. 카드 리소스
 * 5. 카드 연출 (카드 소환 좌표 등등)
 * 
 * 요약: 쉽게 '카드'라는  객체의 공통점을 가지고 '카드를 관리한다'
 */

namespace Card_GamePackage
{
    public class CardManager : Singleton<CardManager>
    {
        [SerializeField] CardItemSO itemso;
        [SerializeField] GameObject cardPrefab;
        //사용할 가상의 카드 임시저장공간 itemBuffer로 지정
        [SerializeField] List<CardItem> itembuffer;
        [SerializeField] List<Card> myCards = new List<Card>();
        [SerializeField] List<Card> otherCards = new List<Card>();
        [SerializeField] Transform cardspawnPoint;

        [SerializeField] Transform myCardLeft;
        [SerializeField] Transform myCardRight;
        [SerializeField] Transform otherCardLeft;
        [SerializeField] Transform otherCardRight;

        [SerializeField] Transform otherCardSpawnPoint;

        [SerializeField] ECardState eCardState;

        bool isMyCardDrag;
        bool onMyCardArea;
        enum ECardState
        {
            Nothing = 0,
            CanMouseOver = 1,
            CanMosueDrag = 2,
        }

        int myPutCount;

        const int MAX_PUT_COUNT = 1;

        Card selectCard;
        //큐와 역할이 같다
        private CardItem PopItem()
        {
            if (itembuffer.Count == 0)
                SetupItemBuffer();


            var result = itembuffer[0];
            itembuffer.RemoveAt(0);

            return result;
        }

        private void SetupItemBuffer()
        {
            itembuffer = new List<CardItem>();
            for (int i = 0; i < itemso.items.Length; i++)
            {
                itembuffer.Add(itemso.items[i]);
            }
        }

        private void Start()
        {
            SetupItemBuffer();
            SetupCardSpawnPoint();
            TurnManager.OnAddCard += AddCard;
            TurnManager.OnTurnStarted += OnTurnStarted;
        }



        private void OnDestroy()
        {
            TurnManager.OnAddCard -= AddCard;
            TurnManager.OnTurnStarted -= OnTurnStarted;
        }

        void OnTurnStarted(bool myTrun)
        {
            myPutCount = 0;
        }

        private void SetupCardSpawnPoint()
        {
            cardspawnPoint = GameObject.Find("CardSpawnPoint").transform;
        }

        private void Update()
        {
            if (isMyCardDrag)
                CardDrag();

            DetectCardArea();
            SetEcardState();
        }



        private void CardDrag()
        {
            if (!onMyCardArea)
            {
                selectCard.MoveTransform(new PRS(Util.MousePos, Util.QI, selectCard.originPRS.scale), false);
                EntityManager.Instance.InsertMyEmptyEntity(Util.MousePos.x);
            }
        }

        //Detect 감시하다
        void DetectCardArea()
        {
            //마우스에서 충돌한 모든 raycastHit를 가져온다
            RaycastHit2D[] hits = Physics2D.RaycastAll(Util.MousePos, Vector3.forward);
            //Util.Log(string.Format("MousePos {0} , forward {1} ", Util.MousePos, Vector3.forward));
            //Debug.DrawRay(Util.MousePos, Vector3.forward, Color.yellow, 0.5f);
            int layer = LayerMask.NameToLayer("MyCardArea");
            //람다식에서 hits를 x로 대입한다
            //마우스와 충돌한 객체 x의 layer가 MyCardArea가 맞다면 OnMyCardArea는 참이다
            onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
            //Util.Log(string.Format("{0}", onMyCardArea));



        }

        void AddCard(bool isMine)
        {
            var cardObject = Instantiate(cardPrefab, cardspawnPoint.position, Util.QI);
            Card card = cardObject.GetComponent<Card>();
            card.Setup();
            card.CardSetup(PopItem(), isMine);
            //3항식의 반환이 List<Card> 형태로 전환된다
            (isMine ? myCards : otherCards).Add(card);
            SetOriginOrder(isMine);
            CardAlignment(isMine);
        }



        void SetOriginOrder(bool isMine)
        {
            int count = isMine ? myCards.Count : otherCards.Count;
            for (int i = 0; i < count; i++)
            {
                var targetCard = isMine ? myCards[i] : otherCards[i];
                targetCard?.GetComponent<Order>().SetOriginOrder(i);
            }
        }

        void CardAlignment(bool isMine)
        {
            List<PRS> originCardPRSs = new List<PRS>();
            if (isMine)
                originCardPRSs = RoundAligenment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 1.9f);
            else
                originCardPRSs = RoundAligenment(otherCardLeft, otherCardRight, otherCards.Count, -0.5f, Vector3.one * 1.9f);

            var targetCards = isMine ? myCards : otherCards;
            for (int i = 0; i < targetCards.Count; i++)
            {
                var targetCard = targetCards[i];

                targetCard.originPRS = originCardPRSs[i];
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);

            }
        }

        #region DragEvnet
        public void CardMouseDown()
        {
            if (eCardState != ECardState.CanMosueDrag)
                return;
            isMyCardDrag = true;

        }

        public void CardMouseUp()
        {
            isMyCardDrag = false;

            if (eCardState != ECardState.CanMosueDrag)
                return;

            if (onMyCardArea)
                EntityManager.Instance.RemoveMyEmptyEntity();
            //Area밖에 존재
            else
                TryPutCard(true);
        }
        #endregion

        //Aligenment
        List<PRS> RoundAligenment(Transform leftTr, Transform rightTr, int objectCount, float height, Vector3 scale)
        {
            float[] objLerps = new float[objectCount];
            List<PRS> result = new List<PRS>();
            switch (objectCount)
            {
                case 1: objLerps = new float[] { 0.5f }; break;
                case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
                case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
                default:
                    float interval = 1f / (objectCount - 1);
                    for (int i = 0; i < objectCount; i++)
                        objLerps[i] = interval * i;
                    break;
            }

            for (int i = 0; i < objectCount; i++)
            {
                var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
                var targetRot = Util.QI;
                if (objectCount >= 4)
                {
                    float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                    curve = height >= 0 ? curve : -curve;
                    targetPos.y += curve;
                    targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);

                }
                result.Add(new PRS(targetPos, targetRot, scale));
            }

            return result;
        }

        #region MyCard

        public void CardMouseOver(Card card)
        {
            if (eCardState == ECardState.Nothing)
                return;

            selectCard = card;
            EnlargeCard(true, card);
        }

        public void CardMouseExit(Card card)
        {
            EnlargeCard(false, card);
        }
        #endregion

        void EnlargeCard(bool isEnalarge, Card card)
        {
            //확대
            if (isEnalarge)
            {
                //확대 포스 지정 x는 그대로 y만 올린다 z를 카메라에 가깝게 곂치는 카드에서 우선순위를 위함
                Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -4.8f, -100f);
                card.MoveTransform(new PRS(enlargePos, Util.QI, Vector3.one * 3.5f), false);
            }
            else
                card.MoveTransform(card.originPRS, false);

            card.GetComponent<Order>().SetMostFrontOrder(isEnalarge);

        }

        private void SetEcardState()
        {
            if (TurnManager.Instance.isLoading)
                eCardState = ECardState.Nothing;

            else if (!TurnManager.Instance.myTurn || myPutCount == 1 || EntityManager.Instance.IsFullMyEntities)
                eCardState = ECardState.CanMouseOver;

            else if (TurnManager.Instance.myTurn && myPutCount == 0)
                eCardState = ECardState.CanMosueDrag;
        }

        public bool TryPutCard(bool isMine)
        {
            if (isMine && myPutCount >= MAX_PUT_COUNT)
                return false;
            if (!isMine && otherCards.Count <= 0)
                return false;


            Card card = isMine ? selectCard : otherCards[Random.Range(0, otherCards.Count)];
            var spawnPos = isMine ? Util.MousePos : otherCardSpawnPoint.position;
            var targetCards = isMine ? myCards : otherCards;

            if (EntityManager.Instance.SpawnEntity(isMine, card?.item, spawnPos))
            {
                targetCards.Remove(card);
                card.transform.DOKill();
                //중요 Destroy는 한 프레임이 지난뒤 처리된다
                DestroyImmediate(card.gameObject);
                if (isMine)
                {
                    selectCard = null;
                    myPutCount++;
                }
                CardAlignment(isMine);
                return true;
            }
            else
            {
                //원래 오더로 돌려놓는다
                targetCards.ForEach(x => x.GetComponent<Order>().SetMostFrontOrder(false));
                //카드패를 돌려놓기에 재정렬한다
                CardAlignment(isMine);
                return false;
            }
        }
    }
}