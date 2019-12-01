using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiManager : MonoBehaviour {
    public enum CameraState {
        /// <summary>
        ///     Main camera, e.g. the camera that the main game is played on.
        /// </summary>
        MAIN,

        /// <summary>
        ///     Scenic camera, e.g. the camera that will be used for main menus.
        /// </summary>
        ALTERNATE
    }

    /// <summary>
    ///     Mutually exclusive GUI components.
    /// </summary>
    public enum GuiState {
        PAUSE,
        MAIN,
        HUD
    }

    /// <summary>
    ///     Camera states. Copied from Cameras because Unity can't handle dictionaries.
    /// </summary>
    private Swapper<CameraState, Camera> cameras;

    /// <summary>
    ///     GUI states. Copied from GUIs because Unity can't handle dictionaries.
    /// </summary>
    private Swapper<GuiState, GameObject> guis;

    private void Start() {
        guis = new Swapper<GuiState, GameObject>(
            GUIs.ToDictionary(kp => kp.Key, kp => {
                kp.Value.SetActive(false);
                return kp.Value;
            }),
            (gui, active) => gui.SetActive(active)
        );
        cameras = new Swapper<CameraState, Camera>(
            Cameras.ToDictionary(kp => kp.Key, kp => kp.Value),
            (camera, active) => camera.enabled = active
        );

        GoMainMenu();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            switch (guis.CurrentKey) {
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

    private void UpdateState(CameraState cameraState, GuiState guiState, Pauser.PauseState pauseState, bool background) {
        cameras.CurrentKey = cameraState;
        guis.CurrentKey = guiState;
        Pauser.PauseStatus = pauseState;
        Background.SetActive(background);
    }

    private void GoMainMenu() => UpdateState(CameraState.ALTERNATE, GuiState.MAIN, Pauser.PauseState.PAUSED, true);

    public void GoActive() => UpdateState(CameraState.MAIN, GuiState.HUD, Pauser.PauseState.ACTIVE, false);

    public void GoPauseGame() => UpdateState(CameraState.MAIN, GuiState.PAUSE, Pauser.PauseState.PAUSED, true);

    public void Restart() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    public void ExitGame() => Application.Quit();

    /// <summary>
    ///     Essentially KeyValuePair of CameraState and Camera but able to be read by the Unity editor.
    /// </summary>
    [Serializable]
    public struct CameraPair {
        public CameraState Key;
        public Camera Value;
    }

    /// <summary>
    ///     Essentially KeyValuePair of GuiState and GameObject but able to be read by the Unity editor.
    /// </summary>
    [Serializable]
    public struct GuiPair {
        public GuiState Key;
        public GameObject Value;
    }

    #region Inspector Variables

    public List<CameraPair> Cameras;
    public List<GuiPair> GUIs;
    public GameObject Background;

    #endregion
}