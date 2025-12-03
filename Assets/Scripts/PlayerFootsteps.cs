using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource fuenteAudio;
    public AudioClip[] sonidosPasos;

    [Header("Configuración")]
    public float velocidadPasos = 0.5f;
    public float volumenMin = 0.8f;
    public float volumenMax = 1.0f;

    private CharacterController controller;
    private float timer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Busca el audio si no se asignó
        if (fuenteAudio == null) fuenteAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 1. Calculamos la velocidad horizontal (ignorando si cae o salta en el eje Y)
        float velocidadHorizontal = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        // 2. Solo sonar si estamos en el suelo Y nos movemos horizontalmente
        // Subí el umbral a 0.2f para evitar que suene por pequeños deslizamientos
        if (controller.isGrounded && velocidadHorizontal > 0.2f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                ReproducirPaso();
                timer = velocidadPasos;
            }
        }
        else
        {
            // Resetear el timer para que el primer paso suene inmediato al empezar a caminar
            timer = 0;
        }
    }

    void ReproducirPaso()
    {
        if (sonidosPasos.Length == 0) return;

        int n = Random.Range(0, sonidosPasos.Length);

        // Variación aleatoria para que no suene robótico
        fuenteAudio.volume = Random.Range(volumenMin, volumenMax);
        fuenteAudio.pitch = Random.Range(0.85f, 1.1f);

        fuenteAudio.PlayOneShot(sonidosPasos[n]);
    }
}