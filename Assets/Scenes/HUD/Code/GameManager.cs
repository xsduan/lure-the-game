using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the names are prety obvious if you ask me
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

    // Start is called before the first frame update
    void Start()
    {
        //you can tell what this does
        //   ||
        //   \/
        Time.timeScale = 0;
        pauseMenu.SetActive(false);
        Camera.enabled = false;
        MenuCamera.enabled = true;
        HUD.SetActive(false);

        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
        IsMenuActive = true;
        //   /\
        //   ||
        //you can tell what this does


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) //check for ESC press
        {
            if (IsMenuActive == false) //inafisieant way for not having the pause menu open in the main menu
            {
                if (pauseMenuActive == 1) //because i forgot bool exists and am too lazy to change it
                {
                    pauseMenu.SetActive(false);
                    pauseMenuActive = 0;
                    Time.timeScale = 1; //unfreeses the game
                }

                else if (pauseMenuActive == 0)
                {
                    pauseMenu.SetActive(true);
                    pauseMenuActive = 1;
                    Time.timeScale = 0;// freeses time     ZA WARLDO!
                }
            }
            
        }

    }

    public void GetBack()
    {
        pauseMenu.SetActive(false); 
        pauseMenu.SetActive(false); 
        pauseMenuActive = 0; //because i forgot bool exists and am too lazy to change it
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Time.timeScale = 1; //un freses the game time
        Camera.enabled = true;//turns off the normal camera
        MenuCamera.enabled = false;//turns off the menu camera
        MainMenu.SetActive(false);//you can tell what this does
        HUD.SetActive(true);//you can tell what this does
        IsMenuActive = false;
    }

    public void mainMenuActive()
    {
        Time.timeScale = 0; //freeses the game time    ZA WORLDO!
        Camera.enabled = false;//turns off the normal camera
        MenuCamera.enabled = true;//turns on the menu camera
        HUD.SetActive(false);         //you can tell what this does
        pauseMenu.SetActive(false);   //you can tell what this does
        MainMenu.SetActive(true);     //you can tell what this does
        IsMenuActive = true;
    }

    public void ExitGame() //you can tell what this does
    {
        Application.Quit();
    }

    public void ActivateShop() //activates the shop and deactivates the other 2
    {
        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(true);
        InventoryCanvas.SetActive(false);
    }

    public void ActivateMap()//activates the map and deactivates the other 2
    {
        MapCanvas.SetActive(true);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
    }

    public void ActivateInventory()//activates the inventory and deactivates the other 2
    {
        MapCanvas.SetActive(false);
        ShopCanvas.SetActive(false);
        InventoryCanvas.SetActive(true);
    }
}