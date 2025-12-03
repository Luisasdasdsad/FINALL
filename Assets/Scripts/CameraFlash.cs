using UnityEngine;
using System.Collections;

public class CameraFlash : MonoBehaviour
{
    [Header("Configuración")]
    public Light flashLight; // Una Point Light muy potente
    public float flashDuration = 0.2f;
    public float flashCooldown = 3.0f;
    public float revealRadius = 10f; // Distancia a la que revela pistas
    public LayerMask hiddenLayer; // Capa de los objetos ocultos

    private bool canFlash = true;

    void Start()
    {
        if (flashLight != null) flashLight.enabled = false;
    }

    void Update()
    {
        // Clic Izquierdo para tomar foto
        if (Input.GetMouseButtonDown(0) && canFlash)
        {
            StartCoroutine(DoFlash());
        }
    }

    IEnumerator DoFlash()
    {
        canFlash = false;

        // 1. El Flash Visual
        if (flashLight != null) flashLight.enabled = true;

        // 2. Detectar objetos ocultos en el área
        Collider[] hits = Physics.OverlapSphere(transform.position, revealRadius, hiddenLayer);
        foreach (Collider hit in hits)
        {
            HiddenObject hiddenObj = hit.GetComponent<HiddenObject>();
            if (hiddenObj != null)
            {
                hiddenObj.Reveal();
            }
        }

        // 3. Apagar luz y esperar cooldown
        yield return new WaitForSeconds(flashDuration);
        if (flashLight != null) flashLight.enabled = false;

        yield return new WaitForSeconds(flashCooldown);
        canFlash = true;
    }
}