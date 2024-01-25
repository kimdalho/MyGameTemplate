using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MessageViewer : MonoBehaviour
{
    [SerializeField] ScrollRect scrollrect;
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform content;

    List<GameObject> texts = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < 40; i++)
        {
            //var newtext = Instantiate(textPrefab);
            //texts.Add(newtext);
            //newtext.transform.parent = content;
            //newtext.SetActive(false);
        }
    }


    public void ShowMessage(MessageModel model)
    {
        var newtext = Instantiate(textPrefab);
        newtext.transform.SetParent(content);
        newtext.GetComponent<TextMeshProUGUI>().text = model.description;
        newtext.SetActive(true);
        //ScrollToBottom();
    }

    private void Update()
    {
        Debug.Log($"{scrollrect.normalizedPosition}");
    }


    public void ScrollToBottom()
    {
        scrollrect.normalizedPosition = new Vector2(1, 0f);
    }

    public ScrollRect GetScrollRect()
    {
        return scrollrect;
    }

}
