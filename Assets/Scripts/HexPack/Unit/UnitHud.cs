using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnitHud : MonoBehaviour
{
    public MiniSlider gauge;
    public TextMeshPro tmp_atk;
    public TextMeshPro tmp_hp;
    [SerializeField]
    private UnitViewer viewer;

    private void SetTMPText(int atk, int curhp)
    {
        tmp_atk.text = atk.ToString();
        tmp_hp.text = curhp.ToString();
    }

    public void Draw(UnitItem model,UnitStat stat)
    {
        viewer.Setup();
        viewer.SetRender(model.render);
        SetTMPText(stat.atk, stat.curHp);
    }
}
