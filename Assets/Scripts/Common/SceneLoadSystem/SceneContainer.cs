using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum eSceneType
{
    None = -1,
    TitleScene = 0,
    LoadScene = 1,
    GameScene =2,
}

public class SceneContainer : MonoBehaviour
{
    public static SceneContainer instance;

    [SerializeField]
    private string[] sceneNames;

    [HideInInspector]
    public string nextScene;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
            

        instance = this;

        DontDestroyOnLoad(this);
    }

    public void LoadScene(eSceneType nextSceneType)
    {
        this.nextScene = sceneNames[(int)nextSceneType];
        SceneManager.LoadScene(sceneNames[(int)eSceneType.LoadScene]);
    }


}
