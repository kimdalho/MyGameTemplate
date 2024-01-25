using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessageManager : Singleton<MessageManager>
{
    public MessageViewer view;
    public List<MessageModel> models = new List<MessageModel>();
    private void Awake()
    {
        //테스트 ;
        for(uint i = 0; i < 30; i++)
        {
           models.Add( new MessageModel(i, $"메세지 테스트 {i}"));
        }
        
    }


    private void Start()
    {
        StartCoroutine(CoShowMessages());
    }

    private IEnumerator CoShowMessages()
    {
        for (int i = 0; i < 20; i++)
        {
            view.ShowMessage(models[i]);
            yield return new WaitForSeconds(1f);
            view.ScrollToBottom();
        }

    }

}
