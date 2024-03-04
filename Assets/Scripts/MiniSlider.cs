using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;
using System;
public class MiniSlider : MonoBehaviour ,ITurnSystem
{
    public Image fill;
    private float maxGage;
    public float curGage = 0;
    public event Action spawn;
    private float _value;

    [SerializeField]
    private List<Fade> fades;

    [SerializeField]
    private bool isDraw;

    private void Start()
    {
        SliderFadeIn();
    }

    private void Update()
    {
        if (isDraw is false)
            return;

        Normalized();
        fill.GetComponent<RectTransform>().localScale = new Vector3(_value, 1, 1);
    }

    public void SetDraw(bool draw)
    {
        this.isDraw = draw;
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
            maxGage = 0;
            curGage = 0;
            spawn?.Invoke();
            isDraw = false;

        }
    }

    private void Awake()
    {
        TurnAwake();
    }

    public void SetMax(int max)
    {
        maxGage = max;
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

    public void SliderFadeIn()
    {
        for(int i = 0; i < fades.Count; i++)
        {
           if(i == fades.Count -1)
           {
                fades[i].FadeIn(() => { this.gameObject.SetActive(false); });
           }
           else
            {
                fades[i].FadeIn();
            }
        }
    }

    public void SliderFadeOut()
    {
        if (isDraw == false)
            return;

        this.gameObject.SetActive(true);
        foreach (var fade in fades) {
            fade.gameObject.SetActive(true);
            fade.FadeOut(() => { });
        }
    }

}
