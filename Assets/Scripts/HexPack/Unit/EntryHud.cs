using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EntryHud : MonoBehaviour
{
    public MiniSlider slider;
    public TextMeshProUGUI tmp_atk;
    public TextMeshProUGUI tmp_hp;
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
