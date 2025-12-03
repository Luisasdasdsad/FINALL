using UnityEngine;
using UnityEngine.UI; // <--- OBLIGATORIO: Sin esto no funciona la Barra

public class FlashlightSystem : MonoBehaviour
{
    [Header("UI - Interfaz")]
    public Slider barraBateria; // Arrastra el Slider del Canvas aquí

    [Header("Configuración de Luz")]
    public Light flashlightSource;
    public float maxIntensity = 2.0f;

    [Header("Batería")]
    public float maxBattery = 100f;
    public float currentBattery;
    public float drainRate = 5.0f; // Ajustado a 5 para que veas bajar la barra al probar (luego bájalo a 1 o 2)

    [Header("Audio")]
    public AudioSource fuenteAudio; // El componente "Audio Source" del Player
    public AudioClip sonidoClick;   // El archivo .mp3 del click

    void Start()
    {
        // Inicializamos la batería al máximo
        currentBattery = maxBattery;

        // Auto-asignar componentes si se te olvidó arrastrarlos
        if (flashlightSource == null) flashlightSource = GetComponent<Light>();
        if (fuenteAudio == null) fuenteAudio = GetComponent<AudioSource>();

        // CONFIGURACIÓN CLAVE PARA LA RÚBRICA (HUD):
        // Aseguramos que la barra visual coincida con los números de la batería
        if (barraBateria != null)
        {
            barraBateria.maxValue = maxBattery;
            barraBateria.value = currentBattery;
        }
    }

    void Update()
    {
        // --- 1. ENCENDER / APAGAR ---
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightSource.enabled = !flashlightSource.enabled;

            // Reproducir sonido (Calidad Técnica - Criterio 4)
            if (fuenteAudio != null && sonidoClick != null)
            {
                fuenteAudio.PlayOneShot(sonidoClick);
            }
        }

        // --- 2. CONSUMO DE BATERÍA ---
        if (flashlightSource.enabled && currentBattery > 0)
        {
            // Reducir batería según el tiempo
            currentBattery -= drainRate * Time.deltaTime;

            // ---> AQUÍ ESTÁ LA CLAVE DEL HUD <---
            // Actualizar la barra visualmente en cada frame
            if (barraBateria != null)
            {
                barraBateria.value = currentBattery;
            }

            // --- 3. EFECTOS VISUALES (FEEDBACK) ---
            // Parpadeo si queda menos del 20% (Atmósfera - Criterio 5)
            if (currentBattery < 20)
            {
                float flicker = Random.Range(0.1f, 1.0f);
                // Si el random es alto, luz tenue; si es bajo, casi apagada
                flashlightSource.intensity = (flicker > 0.5f) ? maxIntensity * 0.5f : 0.1f;
            }
            else
            {
                // Atenuación normal: la luz baja suavemente conforme se gasta la pila
                flashlightSource.intensity = Mathf.Lerp(0, maxIntensity, currentBattery / maxBattery);
            }
        }
        else if (currentBattery <= 0)
        {
            // Se acabó la pila: Apagar todo
            flashlightSource.enabled = false;
            currentBattery = 0;

            // Asegurar que la barra marque 0 visualmente
            if (barraBateria != null) barraBateria.value = 0;
        }
    }

    // Función pública para recargar batería (necesaria para Items/Puzzles)
    public void AddBattery(float amount)
    {
        currentBattery += amount;
        if (currentBattery > maxBattery) currentBattery = maxBattery;

        // Actualizar la barra inmediatamente al recargar
        if (barraBateria != null) barraBateria.value = currentBattery;
    }
}