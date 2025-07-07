using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    // Referencias Componentes
    [SerializeField] private Transform playerBody;


    // Variables manejo de Camara
    public float mouseSensitivity = 60f;
    public float defaultMouseSensitivity = 60f;
    private float xRotation = 0f;


    // Variables de Input
    private PlayerInput playerInput;
    private Vector2 lookInput;


    void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        Cursor.visible = false; // Hacer invisible el cursor
    }

    // Update is called once per frame
    void Update()
    {
        // Leer el input de la accion Look
        lookInput = playerInput.actions["Look"].ReadValue<Vector2>();
        if (lookInput == Vector2.zero)
        {
            return;
        }
        else
        {
            float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
            float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

            // Rotacion de la camara
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotacion vertical
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotar el cuerpo del jugador
            playerBody.Rotate(Vector3.up * mouseX);
            // playerBody.Rotate(Vector3.right * -mouseY);
        }

        
    }
}
