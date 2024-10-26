using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : NetworkBehaviour
{
    [Header("Script's References")]
    public Behaviour[] scriptToDisable;

    [Header("UI")]
    [SerializeField] private static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    [Header("Input")]
    [SerializeField] private InputActionProperty pauseInputSource;

    [Header("Others")]
    [SerializeField] private string MainMenuSceneName = "MainMenu";

    private void Start()
    {
        // Confines the cursor and makes it invisible.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        if (pauseInputSource.action.WasPressedThisFrame())
        {
            if (gameIsPaused)
                Resume();
            else
                Paused();
        }
    }

    // Paused the game.
    void Paused()
    {
        foreach (Behaviour script in scriptToDisable)
        {
            script.enabled = false;
        }
        // Unlock the cursor and make it visible.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Activate the menu.
        pauseMenuUI.SetActive(true);

        gameIsPaused = true;
    }

    // Resume the Game.
    public void Resume()
    {
        // Enable player scripts.
        foreach (Behaviour script in scriptToDisable)
        {
            script.enabled = true;
        }

        // Confines the cursor and makes it invisible.
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        // Disable the menu.
        pauseMenuUI.SetActive(false);

        gameIsPaused = false;
    }


    [Client]
    public void Quit()
    {
        if (isServer)
            NetworkManager.singleton.StopHost();
        else
            NetworkManager.singleton.StopClient();

        SceneManager.LoadScene(MainMenuSceneName);
    }
}