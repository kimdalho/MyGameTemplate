using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hex_Package
{
    public class TileCardManager : Singleton<TileCardManager>
    {
        [SerializeField] TileCardItemSO itemso;
        [SerializeField] GameObject cardPrefab;

        //내 덱의 순환을 위한 사용할 가상의 카드 임시저장공간 itemBuffer로 지정
        [SerializeField] List<TileCardItem> myCardbuffer;

        //보상카드의 순환을 위한 가상의 카드 임시저장공간 itemBuffer로 지정
        [SerializeField] List<TileCardItem> resultCardbuffer;
        [SerializeField] List<TileCard> myCards = new List<TileCard>();
        [SerializeField] Transform cardspawnPoint;

        [SerializeField] Transform myCardLeft;
        [SerializeField] Transform myCardRight;
        [SerializeField] Transform myTileCards;

        [SerializeField] ECardState eCardState;

        int showValue = 0;
        int hideValue = -26;

        bool isMyCardDrag;
        bool onMyCardArea;

        bool onMyCardShowArea;
        enum ECardState
        {
            Nothing = 0,
            CanMouseOver = 1,
            CanMosueDrag = 2,
        }

        int myPutCount;
        

        private void Start()
        {
            SetupItemBuffer();
            SetupCardSpawnPoint();
            TileTurnManager.OnAddCard += AddCard;
            //TileTurnManager.OnTurnStarted += OnTurnStarted;
        }

        private void Update()
        {
            DetectShowCardArea();

            if (TileTurnManager.Instance.isLoading == true)
                return;
                
            if (onMyCardShowArea)
                ShowMyCardShowArea();
            else
                HideMyTileCards();
        }


        private void OnDestroy()
        {
            TileTurnManager.OnAddCard -= AddCard;
            //TileTurnManager.OnTurnStarted -= OnTurnStarted;
        }
        

        private void DetectShowCardArea()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            int layer = LayerMask.NameToLayer("ShowMyCardArea");
            //hits를 x라는 컨테이너 매치
            onMyCardShowArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);

            Util.Log(string.Format("onMyCardShowArea is {0}", onMyCardShowArea));
        }

        private void ShowMyCardShowArea()
        {
            myTileCards.transform.DOMoveY(showValue, 0.5f);

        }

        private void SetupCardSpawnPoint()
        {
            cardspawnPoint = GameObject.Find("CardSpawnPoint").transform;
        }
        void AddCard()
        {
            var cardObject = Instantiate(cardPrefab, cardspawnPoint.position, Util.QI);
            TileCard card = cardObject.GetComponent<TileCard>();
            card.transform.parent = myTileCards;
            card.Setup();
            //이재 CardSetup의 isMine은 필요가없다
            card.CardSetup(PopItem());
            //3항식의 반환이 List<Card> 형태로 전환된다
            myCards.Add(card);
            SetOriginOrder();
            CardAlignment();
        }

        void OnTurnStarted(bool isMyTurn)
        {

        }
        private void CardAlignment()
        {
            List<PRS> originCardPRSs = new List<PRS>();

                originCardPRSs = RoundAligenment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 1.9f);
           
            for (int i = 0; i < myCards.Count; i++)
            {
                var targetCard = myCards[i];

                targetCard.originPRS = originCardPRSs[i];
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);

            }
        }

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

        private void SetOriginOrder()
        {
            for (int i = 0; i < myCards.Count; i++)
            {
                myCards[i]?.GetComponent<Order>().SetOriginOrder(i);
            }
        }

        private TileCardItem PopItem()
        {
            if (myCardbuffer.Count == 0)
                SetupItemBuffer();


            var result = myCardbuffer[0];
            myCardbuffer.RemoveAt(0);

            return result;
        }

        private void SetupItemBuffer()
        {
            myCardbuffer = new List<TileCardItem>();
            for (int i = 0; i < itemso.items.Length; i++)
            {
                myCardbuffer.Add(itemso.items[i]);
            }
        }

        public void HideMyTileCards()
        {
            myTileCards.transform.DOMoveY(hideValue,0.5f);

        }
    }
}
