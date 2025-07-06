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
        }
        else
        {
            isActive = false;
            mainCamera.fieldOfView = defaultFOV;
            cameraLook.mouseSensitivity = cameraLook.defaultMouseSensitivity;
            inspectorHand.SetActive(false);

            colidingPort.GetComponent<Renderer>().material = colidingPort.GetComponent<Port>().portTransparentMaterial;
            colidingPort = null;
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
