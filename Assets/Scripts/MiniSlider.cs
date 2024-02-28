using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;
using System;
public class MiniSlider : MonoBehaviour ,ITurnSystem
{
    public Fade fade;

    public Fade fild_Fade;

    public Image fill;
    public float maxGage = 1;
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
            maxGage += 3;
            curGage = 0;
            spawn?.Invoke();

        }
    }

    private void Awake()
    {
        TurnAwake();
    }

    public void TurnAwake()
    {
        GameManager.Instance.miniSliders.Add(this);
    }

    public void StartPlayerTurn()
    {
       
    }

    public void EndPlayerMove()
    {
        
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.miniSliders.Remove(this);

    }
}
