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
    [SerializeField] LayerMask itemLayer;
    private int maxSlots;
    private GameObject[] slots;

    // Referencias a la mano
    [SerializeField] private GameObject playerGrabRef;


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


    void Update()
    {

    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isInventoryOpen = !isInventoryOpen;

            if (isInventoryOpen)
            {
                inventoryPanel.SetActive(true);
                Time.timeScale = 0f; // Pause the game
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                inventoryPanel.SetActive(false);
                Time.timeScale = 1f; // Resume the game
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
