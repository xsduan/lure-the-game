using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC_VARS
    public List<CameraPair> Cameras;
    public List<GuiPair> GUIs;
    public GameObject Background;
    #endregion

    /// <summary>
    /// Essentially KeyValuePair of CameraState and Camera but able to be read by the Unity editor.
    /// </summary>
    [System.Serializable]
    public struct CameraPair { public CameraState Key; public Camera Value; }

    /// <summary>
    /// Essentially KeyValuePair of GuiState and GameObject but able to be read by the Unity editor.
    /// </summary>
    [System.Serializable]
    public struct GuiPair { public GuiState Key; public GameObject Value; }

    public enum CameraState { MAIN, ALTERNATE };
    public enum GuiState { PAUSE, MAIN, HUD };

    private UniquePrefabSwitch subMenus;
    private Dictionary<GuiState, GameObject> guisDict;
    private Dictionary<CameraState, Camera> camerasDict;

    private static void SwapAction<T1, T2>(
        Dictionary<T1, T2> dict, T1 currentKey, T1 newKey,
        System.Action<T2> currentAction, System.Action<T2> newAction
    )
    {
        dict.TryGetValue(newKey, out T2 newValue);
        if (newValue == null)
        {
            Debug.LogWarning($"There isn't a value for {newKey}. Ignoring.");
            return;
        }

        dict.TryGetValue(currentKey, out T2 currentValue);

        currentAction(currentValue);
        newAction(newValue);
    }

    private CameraState _currentCameraState;
    private CameraState CurrentCameraState
    {
        get { return _currentCameraState; }
        set
        {
            SwapAction(camerasDict, _currentCameraState, value, c => c.enabled = false, n => n.enabled = true);
            _currentCameraState = value;
        }
    }

    private GuiState _currentGuiState;
    private GuiState CurrentGuiState
    {
        get { return _currentGuiState; }
        set
        {
            SwapAction(guisDict, _currentGuiState, value, c => c.SetActive(false), n => n.SetActive(true));
            _currentGuiState = value;
        }
    }

    void Start()
    {
        guisDict = GUIs.ToDictionary(kp => kp.Key, kp =>
        {
            kp.Value.SetActive(false);
            return kp.Value;
        });
        camerasDict = Cameras.ToDictionary(kp => kp.Key, kp => kp.Value);
        subMenus = new UniquePrefabSwitch(guisDict[GuiState.PAUSE].transform);

        GoMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (CurrentGuiState)
            {
                case GuiState.HUD:
                    GoPauseGame();
                    break;
                case GuiState.PAUSE:
                    GoActive();
                    break;
                case GuiState.MAIN:
                    break;
            }
            
        }
    }

    private void UpdateState(CameraState cameraState, GuiState guiState, Pauser.PauseState pauseState, bool background)
    {
        CurrentCameraState = cameraState;
        CurrentGuiState = guiState;
        Pauser.SetPauseState(pauseState);
        Background.SetActive(background);
    }

    public void GoActive() => UpdateState(CameraState.MAIN, GuiState.HUD, Pauser.PauseState.ACTIVE, false);
    public void GoMainMenu() => UpdateState(CameraState.ALTERNATE, GuiState.MAIN, Pauser.PauseState.PAUSED, true);
    public void GoPauseGame() => UpdateState(CameraState.MAIN, GuiState.PAUSE, Pauser.PauseState.PAUSED, true);

    public void ExitGame() => Application.Quit();

    public void ActivateMenu(GameObject prefabMenu) => subMenus.Activate(prefabMenu);
}
