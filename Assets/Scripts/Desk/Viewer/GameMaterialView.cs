using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameMaterialView : MonoBehaviour
{
    [SerializeField]
    private Image imgIcon;
    [SerializeField]
    private TextMeshProUGUI tmpCount;

    public void SetData(List<GameMaterial> models)
    {
        models.ForEach(model =>
        {
            imgIcon.sprite = model.icon;
            tmpCount.text = model.count.ToString();
        });
    }
}
