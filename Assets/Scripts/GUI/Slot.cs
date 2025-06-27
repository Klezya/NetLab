using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{


    public GameObject item;
    public int id;
    public string type, description;
    public Sprite icon;
    public int cantidad;
    public bool empty;
    // Manejar contidad
    public Transform quantityText;
    public Transform slotIconPanel;


    void Awake()
    {
        slotIconPanel = transform.GetChild(0);
        quantityText = transform.GetChild(1);
    }

    public void UpdateSlot()
    {
        slotIconPanel.GetComponent<Image>().sprite = icon;
        if (item.GetComponent<Item>().cantidad > 0)
        {
            quantityText.gameObject.SetActive(true);
            // quantityText.GetComponent<Text>().text = item.GetComponent<Item>().cantidad.ToString();
        }
        else if (item.GetComponent<Item>().cantidad <= 0)
        {
            quantityText.gameObject.SetActive(false);
            // quantityText.GetComponent<Text>().text = item.GetComponent<Item>().cantidad.ToString();
        }

    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            Debug.Log("Slot clicked: " + item.name);
            UseItem();
        }
        else
        {
            Debug.Log("Slot is empty.");
        }
    }

    public void UseItem()
    {

    }


}
