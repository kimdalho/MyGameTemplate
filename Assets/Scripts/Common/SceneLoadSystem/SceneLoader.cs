using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string nextScene => SceneContainer.instance.nextScene;

    private void Awake()
    {

        StartCoroutine(CoLoadScene());
    }

    IEnumerator CoLoadScene()
    {
        yield return null;
        float deltime = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if(deltime < 1)
            {
                deltime += Time.deltaTime;
            }
            else
            {
                op.allowSceneActivation = true;
            }
                
            yield return null;
        }
        Debug.Log("call");
       
        yield break;
    }

}
