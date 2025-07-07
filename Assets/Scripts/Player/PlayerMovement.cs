using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    // Referencias Componentes
    [SerializeField] private CharacterController characterController;
    private PlayerInput playerInput;


    // Variables manejo de Movimiento
    private Vector2 moveInput;
    [SerializeField] private float speed = 5f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtener el componente PlayerInput
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener el input del jugador
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveInput == Vector2.zero)
        {
            return; // Si no hay input, no hacer nada
        }
        else
        {          
            // Mover al jugador
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

            characterController.Move(move * speed * Time.deltaTime);
        }

        





    }
}
