using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : Singleton<ResearchManager>
{
    [SerializeField]
    private ResearchSO so;

    [SerializeField]
    public List<ResearchModel> models;

    private void Awake()
    {
        models = new List<ResearchModel>();
        foreach(var model in so.models)
        {
            this.models.Add(model);
        }
    }



}
