using UnityEngine;

// Este script requiere que el objeto tenga un componente CharacterController
[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float walkSpeed = 4.0f;
    public float runSpeed = 7.0f;
    public float jumpHeight = 1.0f; // Opcional, quizás no quieras salto en un juego de terror
    public float gravity = -9.81f;

    [Header("Configuración de Cámara")]
    public Camera playerCamera;
    public float lookSensitivity = 2.0f;
    public float lookXLimit = 80.0f; // Evita que el jugador se rompa el cuello mirando muy arriba/abajo

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Bloquear el cursor en el centro de la pantalla y ocultarlo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 1. Calcular direcciones (Adelante/Atrás y Izquierda/Derecha)
        // El movimiento es relativo a donde mira el jugador (transform.forward)
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // 2. Detectar si estamos corriendo (Shift Izquierdo)
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calcular velocidad en ejes X y Z
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        // Guardar el valor de movimiento vertical (gravedad/salto) actual para no perderlo
        float movementDirectionY = moveDirection.y;

        // Asignar movimiento calculado
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // 3. Salto (Opcional - Para terror a veces es mejor quitarlo)
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY; // Mantener inercia vertical anterior
        }

        // 4. Aplicar Gravedad
        if (!characterController.isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // 5. MOVER EL CONTROLADOR
        characterController.Move(moveDirection * Time.deltaTime);

        // 6. ROTACIÓN DE CÁMARA (Mirar con el ratón)
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); // Limitar ángulo vertical

            // Mover la cámara (solo rotación vertical local)
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Rotar el cuerpo del personaje (rotación horizontal global)
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
        }
    }
}
