using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] middleRenderers;
    [SerializeField] string sortingLayerName;
    int originOrder;
    //코드 설명
    //몸체가 되는 카드의 위 스프라이트의 Layer를 자동으로
    //관리하여 가려지지않게 초기화한다
    //renderer의 LayerName은 sortingLayerName으로 모두 통일된다
    //몸체의 밑으로 간 모든 Sprite는 mulOrder(카드)보다 Layer가 상위로 올라간다

    /// <param name="isMostFron"></param>
    public void SetMostFrontOrder(bool isMostFron)
    {
        Util.Log("Check");
        SetOrder(isMostFron ? 100 : originOrder);
    }

    public void SetOriginOrder(int originOrder)
    {
        Util.Log("Check");
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetOrder(int order)
    {
        int mulOrder = order * 10;

        foreach(var render in backRenderers)
        {
            render.sortingLayerName = sortingLayerName;
            render.sortingOrder = mulOrder;
        }

        foreach (var render in middleRenderers)
        {
            render.sortingLayerName = sortingLayerName;
            render.sortingOrder = mulOrder  + 1;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SetOrder(originOrder);
    }
}
