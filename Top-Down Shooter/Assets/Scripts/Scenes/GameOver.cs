using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName;

    private void OnEnable()
    {
        UnitHealth.PlayerDied += LoadMainMenu;
    }

    private void OnDisable()
    {
        UnitHealth.PlayerDied -= LoadMainMenu;
    }

    private void LoadMainMenu()
    {
        if (_mainMenuSceneName != "") SceneManager.LoadScene(_mainMenuSceneName);
    }
}
