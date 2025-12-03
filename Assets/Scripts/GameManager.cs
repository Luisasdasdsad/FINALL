using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // <--- AGREGAMOS ESTA LIBRERÍA IMPORTANTE

public class GameManager : MonoBehaviour
{
    [Header("Estado del Jugador")]
    public bool tieneLlave = false;

    [Header("UI Feedback")]
    public GameObject panelVictoria;
    public TextMeshProUGUI textoMensajes; // <--- CAMBIAMOS "Text" POR "TextMeshProUGUI"

    public void RecogerLlave()
    {
        tieneLlave = true;
        MostrarMensaje("¡Has encontrado la Llave del Ala Oeste!");
    }

    public void IntentarEscapar()
    {
        if (tieneLlave)
        {
            // ¡GANASTE!
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
        }
        else
        {
            MostrarMensaje("La puerta está cerrada. Busca la llave.");
        }
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoMensajes != null)
        {
            textoMensajes.text = mensaje;
            Invoke("BorrarMensaje", 3f);
        }
    }

    void BorrarMensaje()
    {
        if (textoMensajes != null) textoMensajes.text = "";
    }
}