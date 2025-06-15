using UnityEngine;
using UnityEngine.InputSystem;

public class MenuActivator : MonoBehaviour
{
    public CanvasMenu canvasMenu;
    public PlayerInput playerInput;

    private InputAction menuActionPlayer;
    private InputAction menuActionUI;

    private void Start()
    {
        // Asegura que el mapa Player est¨¦ activo al inicio
        playerInput.SwitchCurrentActionMap("Player");

        menuActionPlayer = playerInput.actions.FindActionMap("Player").FindAction("Menu");
        menuActionUI = playerInput.actions.FindActionMap("UI").FindAction("Menu");

        if (menuActionPlayer != null)
            menuActionPlayer.started += OnMenu;
        if (menuActionUI != null)
            menuActionUI.started += OnMenu;
    }

    private void OnDestroy()
    {
        if (menuActionPlayer != null)
            menuActionPlayer.started -= OnMenu;
        if (menuActionUI != null)
            menuActionUI.started -= OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext context)
    {
        if (canvasMenu.menuPanel.activeSelf)
            canvasMenu.HideMenu();
        else
            canvasMenu.ShowMenu();
    }
}