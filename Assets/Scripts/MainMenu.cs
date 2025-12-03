using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Esta función va en el botón "JUGAR"
    public void IniciarJuego()
    {
        // Carga la escena siguiente en la lista (que será tu nivel 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Esta función va en el botón "SALIR"
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}