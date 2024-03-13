using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoodsView : MonoBehaviour
{
    TextMeshProUGUI countText;
    Image icon;

    [SerializeField]
    private eGoodsType type;

    private void Awake()
    {
        //icon.sprite = UnitManager.Instance.GetGameMaterialSprite(type);
    }

    public void SetData()
    {
        countText.text =  string.Format("x{0}",UserData.Instance.userGoods[type].ToString());
    }


}
