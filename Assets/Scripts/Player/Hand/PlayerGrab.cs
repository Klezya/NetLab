using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private GameObject handPoint;
    public GameObject grabbedObject = null;


    // Colision del objeto que se va a agarrar
    private bool isColliding = false;
    private GameObject collidingObject = null;



    void OnTriggerEnter(Collider other)
    {
        if (isColliding)
        {
            return;
        }
        if (grabbedObject == null && other.gameObject.CompareTag("Graspable"))
        {
            isColliding = true;
            collidingObject = other.gameObject;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Graspable"))
        {
            isColliding = false;
            collidingObject = null;
        }
    }

    public void GrabAndRelease(InputAction.CallbackContext context)
    {
        if (GetComponent<PlayerPlace>().placePoint != null) return;
        if (grabbedObject != null && grabbedObject.GetComponent<Item>().type == "Conector") return;

        if (context.performed && grabbedObject == null && isColliding)
        { //Agarrar el Objeto

            grabbedObject = collidingObject;

            grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = handPoint.transform.position;
            grabbedObject.transform.SetParent(handPoint.transform);

            if (grabbedObject.GetComponent<Item>().placed)
            {
                grabbedObject.GetComponent<Item>().placed = false;
                grabbedObject.GetComponent<Item>().placedInto.GetComponent<PlacePoint>().placedObject = null;
                grabbedObject.GetComponent<Item>().placedInto = null;
            }


        }
        else if (context.performed && grabbedObject != null)
        { //Soltar el Objeto
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.transform.SetParent(null);

            grabbedObject = null;
        }
    }
}
