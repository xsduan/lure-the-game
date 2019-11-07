using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject pauseMenu;
    private int pauseMenuActive = 0;
    public GameObject MainMenu;
    public GameObject HUD;

    public Camera Camera;
    public Camera MenuCamera;

    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        Camera.enabled = false;
        MenuCamera.enabled = true;
        HUD.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuActive == 1)
            {
                pauseMenu.SetActive(false);
                pauseMenuActive = 0;
                Time.timeScale = 1;
            }

            else if (pauseMenuActive == 0)
            {
                pauseMenu.SetActive(true);
                pauseMenuActive = 1;
                Time.timeScale = 0;
            }
        }

    }

    public void GetBack()
    {
        pauseMenu.SetActive(false);
        pauseMenuActive = 0;
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        HUD.SetActive(true);
        Camera.enabled = true;
        MenuCamera.enabled = false;
        MainMenu.SetActive(false);
    }

    public void mainMenuActive()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        Camera.enabled = false;
        MenuCamera.enabled = true;
        HUD.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
