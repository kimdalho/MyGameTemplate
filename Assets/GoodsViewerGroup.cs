using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsViewerGroup : MonoBehaviour
{
    public List<GoodsView> goods = new List<GoodsView>();
    public void SetData()
    {
        goods.ForEach(goodsView => { goodsView.SetData(); }) ;
    }
}
