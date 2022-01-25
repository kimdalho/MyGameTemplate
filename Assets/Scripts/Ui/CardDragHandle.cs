using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardDragHandle : UiBase  ,IBeginDragHandler , IDragHandler , IEndDragHandler
{
    public CardInfo cardInfo;
    public Vector3 pos;
    public enum eTexts
    {
        CardText = 0,
    }

    private void Awake()
    {
        pos = gameObject.transform.localPosition;
        Setup();
    }
    public override void Setup()
    {
        Bind<Text>(typeof(eTexts));
        Get<Text>((int)eTexts.CardText).text = cardInfo.GetCardCount().ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void ReturnPos()
    {
        transform.localPosition = Vector3.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;

        ReturnPos();
    }
}

[System.Serializable]
public class CardInfo
{
    
    public int cardCount;

    public int GetCardCount()
    {
        return cardCount;
    }

    public void SetCardCount(int valuse)
    {
        cardCount = valuse;
    }
}
