using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InspectorMode : MonoBehaviour
{
    // Inspector Mode Settings
    [SerializeField] private float defaultFOV = 60f;
    [SerializeField] private float inspectorFOV = 30f;
    [SerializeField] private float inspectorSensitivity = 30f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CameraLook cameraLook;
    private bool isActive = false;

    // Inspector Hand
    [SerializeField] private GameObject inspectorHand;

    // Port Management
    private GameObject colidingPort;
    public bool isEditingPorts = false;
    public bool isConnecting = false;
    [SerializeField]private GameObject cablePrefab;
    private GameObject cableRef = null;
    private GameObject cableStart = null;
    private GameObject cableEnd = null;
    [SerializeField] private PlayerInput playerInput;

    void Start()
    {
        inspectorHand.SetActive(false);
    }

    public void ToggleInspectorMode(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!isActive)
        {
            isActive = true;
            mainCamera.fieldOfView = inspectorFOV;
            cameraLook.mouseSensitivity = inspectorSensitivity;
            inspectorHand.SetActive(true);

            playerInput.actions.FindAction("Grab").Disable();
            playerInput.actions.FindAction("PlaceDevice").Disable();
        }
        else
        {
            isActive = false;
            mainCamera.fieldOfView = defaultFOV;
            cameraLook.mouseSensitivity = cameraLook.defaultMouseSensitivity;
            inspectorHand.SetActive(false);
            playerInput.actions.FindAction("Grab").Enable();
            playerInput.actions.FindAction("PlaceDevice").Enable();

            if (colidingPort != null)
            {
                colidingPort.GetComponent<Renderer>().material = colidingPort.GetComponent<Port>().portTransparentMaterial;
                colidingPort = null;
            }
        }
    }

    public void ConectPort(InputAction.CallbackContext context)
    {
        if (!isActive || !context.performed || colidingPort == null) return;
        if (!isEditingPorts) return;
        if (colidingPort.GetComponent<Port>().isBusy)
        {
            GameObject conectorTemp = colidingPort.GetComponent<Port>().conector.gameObject;
            GameObject cableTemp = conectorTemp.GetComponent<CableRJ45>().cableParent;
            cableTemp.GetComponent<CableHolder>().DestroyCable();

            return;
        }
        if (!isConnecting)
        {
            // Iniciar Cable
            isConnecting = true;
            cableRef = Instantiate(cablePrefab, inspectorHand.transform.position, inspectorHand.transform.rotation);
            cableStart = cableRef.transform.GetChild(0).gameObject;
            cableEnd = cableRef.transform.GetChild(1).gameObject;

            // Configurar Cable
            cableStart.GetComponent<CableRJ45>().port = colidingPort;

            // Posicionar inicio
            cableStart.transform.position = colidingPort.transform.position;
            cableStart.transform.rotation = cableStart.GetComponent<Item>().idealRotation;
            cableStart.transform.SetParent(colidingPort.transform);

            // Update Port
            colidingPort.GetComponent<Port>().isBusy = true;
            colidingPort.GetComponent<Port>().conector = cableStart;
        }
        else
        {
            // Finalizar Cable
            isConnecting = false;

            // Configurar Cable
            cableEnd.GetComponent<CableRJ45>().port = colidingPort;

            // Posicionar final
            cableEnd.transform.position = colidingPort.transform.position;
            cableEnd.transform.rotation = cableEnd.GetComponent<Item>().idealRotation;
            cableEnd.transform.SetParent(colidingPort.transform);

            // Update Port
            colidingPort.GetComponent<Port>().isBusy = true;
            colidingPort.GetComponent<Port>().conector = cableEnd;

            // Eliminar Referencias
            cableRef = null;
            cableStart = null;
            cableEnd = null;
        }
        


    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Port")) return;
        if (colidingPort != null) return;

        if (!other.GetComponent<Port>().isBusy)
        {
            colidingPort = other.gameObject;
            colidingPort.GetComponent<Renderer>().material = other.GetComponent<Port>().portHighlightMaterial;
        }
        else
        {
            colidingPort = other.gameObject;
            colidingPort.GetComponent<Renderer>().material = other.GetComponent<Port>().portDeleteMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Port")) return;

        if (colidingPort == other.gameObject)
        {
            colidingPort.GetComponent<Renderer>().material = other.GetComponent<Port>().portTransparentMaterial;
            colidingPort = null;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Port")) return;
        if (colidingPort != null) return;
        
        if (!other.GetComponent<Port>().isBusy)
        {
            colidingPort = other.gameObject;
            colidingPort.GetComponent<Renderer>().material = other.GetComponent<Port>().portHighlightMaterial;
        }
        else
        {
            colidingPort = other.gameObject;
            colidingPort.GetComponent<Renderer>().material = other.GetComponent<Port>().portDeleteMaterial;
        }
    }
}
