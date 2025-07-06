using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlace : MonoBehaviour
{
    public GameObject placePoint = null;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material transparentMaterial;
    [SerializeField] private PlayerGrab playerGrab;


    

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlacePoint")) return;
        if (GetComponent<PlayerGrab>().grabbedObject == null) return;
        
        if (placePoint == null && other.gameObject.GetComponent<PlacePoint>().placedObject == null)
        {
            other.GetComponent<Renderer>().material = highlightMaterial;
            placePoint = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("PlacePoint")) return;
        if (other.gameObject.GetComponent<PlacePoint>().placedObject != null)
        {
            return;
        }
        if (placePoint != null && placePoint == other.gameObject)
        {
            other.GetComponent<Renderer>().material = transparentMaterial;
            placePoint = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("PlacePoint")) return;
        if (GetComponent<PlayerGrab>().grabbedObject == null) return;

        if (other.gameObject.GetComponent<PlacePoint>().placedObject != null)
        {
            return;
        }
        if (placePoint == null)
        {
            other.GetComponent<Renderer>().material = highlightMaterial;
            placePoint = other.gameObject;
        }
    }
    
    public void PlaceItem(InputAction.CallbackContext context)
    {
        if (placePoint == null) return;
        if (GetComponent<PlayerGrab>().grabbedObject == null) return;
    
        if (context.performed)
        {
            // Place the item at the place point
            GameObject itemToPlace = GetComponent<PlayerGrab>().grabbedObject;
            itemToPlace.transform.position = placePoint.transform.position + itemToPlace.GetComponent<Item>().idealRelativePosition;
            itemToPlace.transform.rotation = itemToPlace.GetComponent<Item>().idealRotation;

            // Disable the Rigidbody to prevent physics interactions
            itemToPlace.GetComponent<Rigidbody>().isKinematic = true;
            itemToPlace.GetComponent<Rigidbody>().useGravity = false;

            itemToPlace.transform.SetParent(null); // Unparent the item from the hand

            // Reset the grabbed object
            itemToPlace.GetComponent<Item>().placed = true;
            itemToPlace.GetComponent<Item>().placedInto = placePoint;
            GetComponent<PlayerGrab>().grabbedObject = null;

            // Update placedObject in PlacePoint
            placePoint.GetComponent<PlacePoint>().placedObject = itemToPlace;

            // Reset the highlight
            placePoint.GetComponent<Renderer>().material = transparentMaterial;
            placePoint = null;

        }
    }

}
