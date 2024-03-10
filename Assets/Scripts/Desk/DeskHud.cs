using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.UI.CanvasScaler;

public class DeskHud : MonoBehaviour
{
    [Header("Only Player")]
    [SerializeField] private PlayerHud playerHud;

    [Header("Tower Or Creture")]
    [SerializeField] private EnemyUnitHud enemyHud; 

    public void Writ()
    {
        playerHud.SetData(GameManager.Instance.player);
    }

    public void OnClickedUnit(Unit unit)
    {
        if (unit == null)
        {
            Debug.LogError("잘못된 접근");
            return;
        }

        if (unit.status == eUnitType.Player)
        {
            enemyHud.gameObject.SetActive(false);
            enemyHud.bottom.gameObject.SetActive(false);
            playerHud.gameObject.SetActive(true);
            playerHud.SetData(unit);
            
        }
        else
        {
            enemyHud.gameObject.SetActive(true);
            playerHud.bottom.gameObject.SetActive(false);
            playerHud.gameObject.SetActive(false); 
            enemyHud.SetData(unit);
        }

    }
}
