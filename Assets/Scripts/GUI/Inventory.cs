using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Inventory : MonoBehaviour
{


    private bool isInventoryOpen = false;

    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject slotHolder;
    [SerializeField] LayerMask itemLayer;
    private int maxSlots;
    private int enabledSlots;
    private GameObject[] slots;

    // Referencias a la mano
    [SerializeField] private PlayerGrab playerGrabRef;


    void Start()
    {
        maxSlots = slotHolder.transform.childCount;
        slots = new GameObject[maxSlots];
        enabledSlots = maxSlots;

        for (int i = 0; i < maxSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slots[i].GetComponent<Slot>().item == null)
            {
                slots[i].GetComponent<Slot>().empty = true;
            }
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

    public void TryAddItem(InputAction.CallbackContext context)
    {
        if (context.performed && enabledSlots > 0 && playerGrabRef.grabbedObject.layer == 7)
        {
            Item item = playerGrabRef.grabbedObject.GetComponent<Item>();
            Debug.Log("Intentando agregar al inventario: " + playerGrabRef.grabbedObject.name);

            AddItem(playerGrabRef.grabbedObject, item.id, item.type, item.description, item.icon);

        }
    }
    
    public void AddItem(GameObject item, int id, string type, string description, Sprite icon)
    {
        if (enabledSlots > 0)
        {
            // Buscar un slot vacío
            for (int i = 0; i < maxSlots; i++)
            {
                if (slots[i].GetComponent<Slot>().empty)
                {
                    // Asignar el item al slot
                    slots[i].GetComponent<Slot>().item = item;
                    slots[i].GetComponent<Slot>().id = id;
                    slots[i].GetComponent<Slot>().type = type;
                    slots[i].GetComponent<Slot>().description = description;
                    slots[i].GetComponent<Slot>().icon = icon;
                    slots[i].GetComponent<Slot>().empty = false;

                    // Desactivar el objeto agarrado
                    item.transform.SetParent(slots[1].transform);
                    item.SetActive(false);

                    enabledSlots--;
                    Debug.Log("Item agregado al inventario: " + item.name);
                    return;
                }
            }
        }
    }

}
