using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyUnitHud : DeskUnitHud
{

    [SerializeField]
    private List<GameObject> icons;
    private int grad;
    [SerializeField]
    private List<GameMaterialView> materialViews;


    public override void SetData(Unit model)
    {
        base.SetData(model);
        EnemyUnit enemyModel = model as EnemyUnit;
        grad = enemyModel.grad;

        thumbnail.sprite = model.image;
        nameString.text = model.unitName;
        tmp_atk.text = model.GetAttack().ToString("D3");
        tmp_hp.text = string.Format("{0}/{1}", model.GetCurrentHp().ToString(), model.GetMaxHp().ToString());
        slider_hp.value = (float)((float)model.GetCurrentHp() / (float)model.GetMaxHp());
        

        icons.ForEach(obj => obj.SetActive(false));
        materialViews.ForEach(obj => obj.gameObject.SetActive(false));

        for (int i = 0; i < grad; i++)
        {
            icons[i].SetActive(true);
        }

        for (int i =0; i < enemyModel.materials.Count; i++)
        {
            materialViews[i].gameObject.SetActive(true);
            materialViews[i].SetData(enemyModel.materials[i]);
        }    
    }



}
