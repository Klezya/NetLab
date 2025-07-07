using UnityEngine;


public class Slot : MonoBehaviour
{


    [SerializeField] private GameObject item;
    [SerializeField] private Transform handPoint;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject warningText;
    


    public void OnClick()
    {
        if (hand.GetComponent<PlayerGrab>().grabbedObject != null && hand.GetComponent<PlayerGrab>().grabbedObject.GetComponent<Item>().type != "Conector")
        {
            Debug.Log("You already have an item in your hand");
            warningText.GetComponent<WarningText>().InitFade();
            return;
        }
        if (item.GetComponent<Item>().type != "Conector")
        {
            GameObject newItem = Instantiate(item, handPoint.position, handPoint.rotation);
            newItem.transform.SetParent(handPoint);
            newItem.GetComponent<Rigidbody>().useGravity = false;
            newItem.GetComponent<Rigidbody>().isKinematic = true;
            hand.GetComponent<PlayerGrab>().grabbedObject = newItem;
        }
        else
        {
            GetComponent<CableManager>().Use();
        }
    }
}
