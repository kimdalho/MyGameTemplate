using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;
using System;
public class MiniSlider : MonoBehaviour
{

    public Fade fade;

    public Image fill;

    public float maxGage = 3;
    public float curGage = 0;
    public event Action spawn;
    private float _value;




    private void Update()
    {
        Normalized();
        fill.GetComponent<RectTransform>().localScale = new Vector3(_value, 1, 1);
    }

    private void Normalized()
    {
        if(curGage <= 0)
        {
            _value = 0;
            return;
        }

        _value =(float)(curGage / maxGage);
        
        if(maxGage <= curGage)
        {
            curGage = 0;
            spawn?.Invoke();

        }


    }


}
