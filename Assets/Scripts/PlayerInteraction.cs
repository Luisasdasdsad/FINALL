using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 3.0f;
    public LayerMask interactableLayer; // Para que solo detecte objetos importantes

    void Update()
    {
        // Lanzar un rayo invisible desde el centro de la cámara
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            // ¿Golpeamos algo interactuable?
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // AQUÍ PODRÍAS MOSTRAR UI (Ej: "Presiona E")
                Debug.Log("Mirando: " + interactable.promptMessage);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
