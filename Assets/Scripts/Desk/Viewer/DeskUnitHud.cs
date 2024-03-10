using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class DeskUnitHud : MonoBehaviour
{
    [SerializeField]
    protected Image thumbnail;
    [SerializeField]
    protected TextMeshProUGUI nameString;
    [Header("HP")]
    [SerializeField] protected Slider slider_hp;
    [SerializeField] protected TextMeshProUGUI tmp_hp;
    [Header("Stat")]
    [SerializeField] protected TextMeshProUGUI tmp_atk;

    [Header("하위 정보창")]
    [SerializeField]
    public GameObject bottom;


    public virtual void SetData(Unit model)
    {
        bottom.SetActive(true);

    }

}
