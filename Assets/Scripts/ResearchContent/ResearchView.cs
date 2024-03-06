using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResearchView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp_name;
    [SerializeField]
    private Image icon;

    [Header("소모비용")]
    [SerializeField]
    private TextMeshProUGUI tmp_hungerCost;
    [SerializeField]
    private TextMeshProUGUI tmp_goldCost;
    [SerializeField]
    private TextMeshProUGUI tmp_castlelevel;
    [Header("슬라이더")]
    [SerializeField]
    private Slider slider;
    [Header("버튼")]
    [SerializeField]
    private Button button;


    public void SetData(ResearchModel model)
    {
        tmp_name.text = model.name;
        icon.sprite = model.thumbnail;

        tmp_hungerCost.text = model.hungerCost.ToString();
        tmp_goldCost.text = model.goldCost.ToString();
        tmp_castlelevel.text = model.castleLevel.ToString();

        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        Debug.Log("눌림");
    }

    private void Lock()
    {

    }

    private void Unlock()
    {

    }

    



}
