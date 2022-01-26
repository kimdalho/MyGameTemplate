using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpriteManager : Singleton<SpriteManager>
{
    [Header("Sprite")]
    [SerializeField] Sprite backGround;
    [SerializeField] Sprite button;
    [Header("Rnderer")]
    [SerializeField] SpriteRenderer backgrdoundRender;


    public Action SetRenderSprite;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        backgrdoundRender.sprite = backGround;
    }

    public Sprite GetButtonSprite()
    {
        return button;
    }
}
