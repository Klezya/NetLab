using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private GameObject handPoint;
    private GameObject grabbedObject = null;


    // Colision del objeto que se va a agarrar
    private bool isColliding = false;
    private GameObject collidingObject = null;

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (grabbedObject == null && other.gameObject.CompareTag("Graspable"))
        {
            isColliding = true;
            collidingObject = other.gameObject;
            Debug.Log("Objeto colisionado: " + collidingObject.name);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Graspable"))
        {
            isColliding = false;
            collidingObject = null;
            Debug.Log("Objeto salido de colision: " + other.gameObject.name);
        }
    }

    public void GrabAndRelease(InputAction.CallbackContext context)
    {
        if (context.performed && grabbedObject == null && isColliding)
        { //Agarrar el Objeto

            grabbedObject = collidingObject;

            grabbedObject.GetComponent<Rigidbody>().useGravity = false;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = handPoint.transform.position;
            grabbedObject.transform.SetParent(handPoint.transform);


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
