using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchScroll : MonoBehaviour
{
    [SerializeField]
    private ResearchView[] researchViews;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 0; i <  researchViews.Length; i++)
            {
                researchViews[i].SetData(ResearchManager.Instance.models[i]);
            }
        }
    }
}
