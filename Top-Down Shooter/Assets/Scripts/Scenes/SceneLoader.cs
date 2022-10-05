using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneOnName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
