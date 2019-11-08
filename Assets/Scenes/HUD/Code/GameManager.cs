using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private int pauseMenuActive = 0;
    public GameObject MainMenu;
    public GameObject HUD;
    public GameObject ShopCanvas;
    public GameObject InventoryCanvas;
    public GameObject MapCanvas;
    private bool IsMenuActive;

    public Camera Camera;
    public Camera MenuCamera;
    
    void Start()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        Camera.enabled = false;
        MenuCamera.enabled = true;
        HUD.SetActive(false);

        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
        IsMenuActive = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsMenuActive == false)
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

    }

    public void GetBack()
    {
        pauseMenu.SetActive(false); 
        pauseMenu.SetActive(false); 
        pauseMenuActive = 0;
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Camera.enabled = true;
        MenuCamera.enabled = false;
        MainMenu.SetActive(false);
        HUD.SetActive(true);
        IsMenuActive = false;
    }

    public void mainMenuActive()
    {
        Time.timeScale = 0;
        Camera.enabled = false;
        MenuCamera.enabled = true;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        MainMenu.SetActive(true);
        IsMenuActive = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ActivateShop()
    {
        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(true);
        InventoryCanvas.SetActive(false);
    }

    public void ActivateMap()
    {
        MapCanvas.SetActive(true);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
    }

    public void ActivateInventory()
    {
        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(true);
    }
}