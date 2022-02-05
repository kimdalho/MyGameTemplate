using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hex_Package
{
    public class TileCardManager : Singleton<TileCardManager>
    {
        [Header("EnalargeVlaues")]
        //선택된 카드의 Enalage시 올라가는 offY축 크기이다
        public  float ENALARGE_OffsetY = 10f;
        //선택된 카드의 Enalage시 증가하는 사이즈값이다
        public  float ENALARGE_SCALE = 2.5f;


        public float AlignmentPosY; // 3f
        public float AlignmentScale; // 1.9f



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
        public static Action OnEndLoading;


        TileCard selectCard;

        public bool isMyCardDrag;
        bool onMyCardArea;
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
        }

        private void Update()
        {
            if (isMyCardDrag)
                CardDrag();
        }


        private void OnDestroy()
        {
            TileTurnManager.OnAddCard -= AddCard;
        }
        

        private void CardDrag()
        {
            if(!onMyCardArea)
            {
                selectCard.MoveTransform(new PRS(Util.MousePos, Util.QI, selectCard.originPRS.scale), false);
            }
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

        public void CardMouseOver(TileCard tileCard)
        {
            if (eCardState == ECardState.Nothing)
                return;

            selectCard = tileCard;
            EnlargeCard(true, tileCard);
        }

        void OnTurnStarted(bool isMyTurn)
        {

        }
        private void CardAlignment()
        {
            //매 AddCard를하며 호출된다
            //카드의 리스트의 정렬이 카드의 추가에 따라 라운드가 달라지기 때문
            List<PRS> originCardPRSs = new List<PRS>();
            //카드의 위치를 초기화하는 값을 생성하기위해 originCardPRS를 사용한다
            
            originCardPRSs = RoundAligenment(myCardLeft, myCardRight, myCards.Count, AlignmentPosY, Vector3.one * AlignmentScale);
           
            for (int i = 0; i < myCards.Count; i++)
            {
                var targetCard = myCards[i];

                targetCard.originPRS = originCardPRSs[i];
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
            }
        }

        List<PRS> RoundAligenment(Transform leftTr, Transform rightTr, int objectCount, float height, Vector3 scale)
        {
            //height는 반지름을 의미
            //실수 배열 objLerps를 카드의 갯수만큼 확보한다
            float[] objLerps = new float[objectCount];
            //반환 결과물을 좌표 각도,크기를 담고있는 PRS 클래스로 정의한다
            List<PRS> result = new List<PRS>();
            //3장 까지는 그대로 정렬되며 
            //4장부터 원형으로 카드가 정렬된다
            switch (objectCount)
            {
                case 1: objLerps = new float[] { 0.5f }; break;
                case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
                case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
                default:
                    //시간적 간격
                    float interval = 1f / (objectCount - 1);
                    for (int i = 0; i < objectCount; i++)
                    {
                        objLerps[i] = interval * i;
                    }
                        
                    break;
            }
            for (int i = 0; i < objectCount; i++)
            {
                //목적좌표는 = 선형보관으로 다음 카드의 targetPosX를 지정한다
                var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
                var targetRot = Util.QI;
                if (objectCount >= 4)
                {
                    //y 
                    float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));

                    Util.Log(string.Format("{0} = Mathf.Sqrt(Mathf.Pow({1}, 2) - Mathf.Pow({2} - 0.5f, 2))",
                        curve,height,objLerps[i]));

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


        #region MouseEvent

        public void CardMosueExit(TileCard tileCard)
        {
            EnlargeCard(false, tileCard);
        }

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
            {
                //EntityManager.Instance.RemoveMyEmptyEntity();
            }
            //Area밖에 존재
            else
            {
                TryPutCard();
            }
                
        }

        private void TryPutCard()
        {
            //임시

            myCards.Remove(selectCard);
            DestroyImmediate(selectCard.gameObject);
            //selectCard.item.skill.Use();
            GameManager.Instance.CheatGetMoveScore();
            selectCard = null;
            CardAlignment();


/*            switch (selectCard.item.type)
            {
                case TileCardItem.eType.DecType:
                    break;

                case TileCardItem.eType.ResultType:
                    break;
            }*/
        }


        private void EnlargeCard(bool isEnalarge, TileCard tileCard)
        {
            if(isEnalarge)
            {
                Vector3 enlargePos = new Vector3(tileCard.originPRS.pos.x, ENALARGE_OffsetY, -100);
                tileCard.MoveTransform(new PRS(enlargePos, Util.QI, Vector3.one * ENALARGE_SCALE), false);
            }
            else
            tileCard.MoveTransform(tileCard.originPRS, false);

            tileCard.GetComponent<Order>().SetMostFrontOrder(isEnalarge);
        }


        #endregion
    }

}
