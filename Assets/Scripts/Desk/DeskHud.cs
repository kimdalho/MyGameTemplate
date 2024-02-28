using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeskHud : MonoBehaviour, IUiObserver
{
    [SerializeField] private Image player;
    [SerializeField] private TextMeshProUGUI nameString;

    [SerializeField] private TextMeshProUGUI tmp_atk;
    [SerializeField] private TextMeshProUGUI tmp_gold;
    [SerializeField] private TextMeshProUGUI tmp_move;

    [SerializeField] private Slider slider_hp;
    [SerializeField] private Slider slider_hungger;

    private void Awake()
    {
        GameManager.Instance.uiObservers.Add(this);
    }

    public void Writ()
    {
        var playerStat = GameManager.Instance.playerStat;
        tmp_atk.text =  playerStat.atk.ToString();
        tmp_gold.text = playerStat.move.ToString();
        tmp_move.text = playerStat.move.ToString();

        slider_hp.value = (float)((float)playerStat.curHp / (float)playerStat.maxHp);
        slider_hungger.value = (playerStat.hunger / playerStat.maxHunger);
    }
}
