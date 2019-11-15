using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour
{
    public string MainMenuSceneName;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
