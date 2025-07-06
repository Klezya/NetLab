using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


    private bool isInventoryOpen = false;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject slotHolder;
    private int maxSlots;
    private GameObject[] slots;

    // Referencias a la mano
    [SerializeField] private GameObject playerGrabRef;

    // Referencia al input
    [SerializeField] private PlayerInput playerInput;

    void Awake()
    {
        inventoryPanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }

    void Start()
    {
        maxSlots = slotHolder.transform.childCount;
        slots = new GameObject[maxSlots];

        for (int i = 0; i < maxSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInventoryOpen = !isInventoryOpen;

            if (isInventoryOpen)
            {
                inventoryPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                playerInput.actions.FindAction("Grab").Disable(); // Disable the grab/release action when inventory is open
                playerInput.actions.FindAction("Look").Disable(); // Disable the look action when inventory is open
            }
            else
            {
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                playerInput.actions.FindAction("Grab").Enable(); // Enable the grab/release action when inventory is closed
                playerInput.actions.FindAction("Look").Enable(); // Enable the look action when inventory is closed
            }
        }
    }
}
