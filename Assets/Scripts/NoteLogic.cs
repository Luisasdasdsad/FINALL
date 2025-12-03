using UnityEngine;

public class NoteLogic : MonoBehaviour
{
    // Esto se activa automáticamente cuando el Panel aparece (SetActive True)
    void OnEnable()
    {
        // 1. Liberar el cursor para que puedas moverlo y dar clic
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Esta función la llamaremos desde el Botón "X"
    public void CerrarNota()
    {
        // 1. Ocultar el panel
        gameObject.SetActive(false);

        // 2. Volver a atrapar el cursor para seguir jugando
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}