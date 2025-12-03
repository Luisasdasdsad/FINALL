using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // 1. Agregamos 'Key' a la lista de tipos
    public enum InteractionType { Note, Battery, Door, Key }

    [Header("Configuración")]
    public string promptMessage = "Presiona E para interactuar";
    public InteractionType type;

    [Header("Solo para Pilas")]
    public float batteryAmount = 25f;

    [Header("Eventos (Lo que pasa al tocar)")]
    public UnityEvent onInteract; // Aquí arrastras funciones en el inspector

    public void BaseInteract()
    {
        // --- CASO 1: BATERÍA ---
        if (type == InteractionType.Battery)
        {
            // Busca la linterna en el jugador y recarga
            FlashlightSystem flashlight = FindObjectOfType<FlashlightSystem>();
            if (flashlight != null)
            {
                flashlight.AddBattery(batteryAmount);
            }
            Destroy(gameObject); // Desaparece la pila
        }

        // --- CASO 2: NOTA ---
        else if (type == InteractionType.Note)
        {
            Debug.Log("Nota leída: " + promptMessage);
            // IMPORTANTE: Para las notas, mejor no destruir el objeto aquí si usas el evento 
            // para mostrar el UI, o el UI podría fallar. Deja que el botón 'X' de la nota lo cierre.
            // Si quieres que la nota de papel desaparezca del suelo, usa:
            // gameObject.SetActive(false); 
        }

        // --- CASO 3: LLAVE (NUEVO) ---
        else if (type == InteractionType.Key)
        {
            // Buscamos el GameManager para decirle que ya la tenemos
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager != null)
            {
                manager.RecogerLlave();
            }
            // La llave desaparece del mundo físico
            Destroy(gameObject);
        }

        // --- CASO 4: PUERTA FINAL (NUEVO) ---
        else if (type == InteractionType.Door)
        {
            // Preguntamos al GameManager si podemos ganar
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager != null)
            {
                manager.IntentarEscapar();
            }
        }

        // Ejecutar eventos extra (Sonidos, Animaciones, UI, etc.)
        onInteract.Invoke();
    }
}