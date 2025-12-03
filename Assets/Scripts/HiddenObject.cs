using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float visibleTime = 5.0f; // Cuánto tiempo se queda visible tras el flash

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Hide(); // Empezar invisible
    }

    public void Reveal()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
            // Reiniciar el contador de ocultarse si le volvemos a dar
            CancelInvoke("Hide");
            Invoke("Hide", visibleTime);
        }
    }

    void Hide()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }
}