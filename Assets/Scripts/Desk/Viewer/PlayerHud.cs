using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHud : DeskUnitHud
{
    [Header("hungger")]
    [SerializeField] private Slider slider_hungger;
    [SerializeField] private TextMeshProUGUI tmp_hungger;
    [SerializeField] private TextMeshProUGUI tmp_move;
    public override void SetData(Unit model)
    {
        base.SetData(model);

        var player = model as Player;
        var playerStat = player.playerStat;
        tmp_atk.text = playerStat.atk.ToString("D3");
        tmp_move.text = playerStat.move.ToString("D3");
        slider_hp.value = (float)((float)playerStat.curHp / (float)playerStat.maxHp);
        tmp_hp.text = string.Format("{0}/{1}", playerStat.curHp.ToString(), playerStat.maxHp.ToString());
        slider_hungger.value = (playerStat.hunger / playerStat.maxHunger);
        tmp_hungger.text = string.Format("{0}/{1}", playerStat.hunger, playerStat.maxHunger);
    }
}
