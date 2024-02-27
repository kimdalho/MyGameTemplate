using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Say : MonoBehaviour
{
    [SerializeField] Fade fade;
    [Header("텍스트")]
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    [SerializeField]
    private float delay;

    [SerializeField]
    private string description;

    private void Awake()
    {
        if(textMeshPro == null)
        {
            Debug.LogWarning("Say를 사용하지만 Tmp가 정의되지않았습니다.");
        }
    }


    public void Do()
    {
        textMeshPro.text = "";
        fade.FadeOut(()=>{ StartCoroutine(CoDoSay()); });
    }

    private IEnumerator CoDoSay()
    {
        char[] tochars = description.ToCharArray();
        string temp = "";
        for(int i = 0; i < tochars.Length; i++)
        {
            temp += tochars[i];
            textMeshPro.text = temp;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1);
        fade.FadeIn();


    }

}
