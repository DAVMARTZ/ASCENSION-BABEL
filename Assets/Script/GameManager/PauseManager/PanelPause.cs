using UnityEngine;
using UnityEngine.InputSystem;

public class PanelPause : MonoBehaviour
{
    public GameObject pausePanel;

    private NewActions controls;

    private void Awake()
    {
        controls = new NewActions();
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.pause.performed += PauseGame;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.pause.performed -= PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else 
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
