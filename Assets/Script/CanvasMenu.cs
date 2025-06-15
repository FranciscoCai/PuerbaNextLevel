using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasMenu : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject menuPanel; 

    private void Start()
    {
    }

    public void RespawnPlayer()
    {
        EventCenter.Instance.EventTrigger<int>("Respawn", 0);
        ObjectManager.instance.ActivateAllObjects();
        EventCenter.Instance.EventTrigger<int>("Reset", 0);
        ObjectManager.instance.ClearObjects();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        playerInput.SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player"); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
