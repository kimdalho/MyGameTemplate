using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnitView : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHud;

    [Header("스텟")]
    [SerializeField]
    private Image imgIcon;
    [SerializeField]
    private TextMeshProUGUI tmpUnitName;
    [SerializeField]
    private TextMeshProUGUI tmpAttack;
    [SerializeField]
    private TextMeshProUGUI tmpMaxHp;
    [SerializeField]
    private List<GameObject> icons;
    private int grad;

    //[Header("드랍 아이템")]
    [SerializeField]
    private GameObject bottom;
    [SerializeField]
    private List<GameMaterialView> materialViews;


    public void SetData(Unit model)
    {
        if (model == null)
        {
            Debug.LogError("잘못된 접근");
            return;
        }

        gameObject.SetActive(true);
        playerHud.SetActive(false);
        bottom.SetActive(true);

        imgIcon.sprite = model.image;
        tmpUnitName.text = model.unitName;
        tmpMaxHp.text = string.Format("{0}/{1}", model.GetAttack().ToString(), model.GetMaxHp().ToString());
        grad =  model.grad;

        icons.ForEach(obj => obj.SetActive(false));

        for(int i = 0; i < grad; i++)
        {
            icons[i].SetActive(true);
        }

        materialViews.ForEach(data => data.SetData(model.materials));
    }

}
