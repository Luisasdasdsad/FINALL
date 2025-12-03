using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SpecterAI : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform jugador;
    public Transform[] puntosPatrulla; // Waypoints

    private int indiceActual = 0;
    public float distanciaVision = 10f;
    public float distanciaAtrapado = 1.5f;

    void Start()
    {
        // Si no asignaste el agente manual, lo busca
        if (agente == null) agente = GetComponent<NavMeshAgent>();

        // Empieza a patrullar hacia el primer punto
        MoverAlSiguientePunto();
    }

    void Update()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

        // LÓGICA BÁSICA:
        // Si ve al jugador -> Perseguir
        // Si no -> Seguir patrullando

        if (distanciaAlJugador < distanciaVision)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }
    }

    void PerseguirJugador()
    {
        agente.destination = jugador.position;
        if (Vector3.Distance(transform.position, jugador.position) < distanciaAtrapado)
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Aquí reiniciaremos el nivel o quitaremos vida luego
        }
    }

    void Patrullar()
    {
        // Si el agente llegó a su destino (o está muy cerca)
        if (!agente.pathPending && agente.remainingDistance < 0.5f)
        {
            MoverAlSiguientePunto();
        }
    }

    void MoverAlSiguientePunto()
    {
        if (puntosPatrulla.Length == 0) return;

        // Mueve al agente al punto actual
        agente.destination = puntosPatrulla[indiceActual].position;

        // Pasa al siguiente punto (y vuelve a 0 si llega al final)
        indiceActual = (indiceActual + 1) % puntosPatrulla.Length;
    }
}
