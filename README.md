# 👻 Mansión Espectral

> **Proyecto Final - Desarrollo de Videojuegos**
> Un juego de terror y supervivencia en primera persona desarrollado en Unity (URP).

![Estado](https://img.shields.io/badge/Estado-Finalizado-green)
![Unity](https://img.shields.io/badge/Unity-2022.3%2B-blue)
![Render](https://img.shields.io/badge/Render-URP-orange)

## 📖 Descripción
**Mansión Espectral** es una experiencia de horror atmosférico donde el jugador debe explorar el "Ala Oeste" de una mansión abandonada. El objetivo es encontrar la llave de salida y escapar antes de ser atrapado por el "Espectro Guardián", una entidad que patrulla los pasillos oscuros.

El juego destaca por su uso de iluminación dinámica, gestión de recursos (batería) y audio 3D inmersivo.

## 🎮 Mecánicas Principales
* **Sistema de Iluminación y Batería:** El jugador depende de una linterna con batería limitada. Si se agota, queda en total oscuridad.
* **Inteligencia Artificial (IA):** Enemigo ("Espectro") con sistema de navegación NavMesh que patrulla puntos clave y persigue al jugador si se acerca demasiado.
* **Sistema de Interacción:** Uso de Raycasting para detectar e interactuar con:
    * 🔦 **Baterías:** Recargan la energía.
    * 📄 **Notas:** Narrativa ambiental en UI.
    * 🗝️ **Llaves y Puertas:** Sistema de inventario simple para desbloquear el final.
* **Gráficos URP:** Uso de Shaders personalizados (Fantasma con emisión/transparencia) y Post-Procesado (Bloom, Viñeta, Grano).

## 🕹️ Controles

| Tecla | Acción |
| :---: | :--- |
| **W, A, S, D** | Moverse |
| **Mouse** | Mirar alrededor |
| **F** | Encender / Apagar Linterna |
| **Clic Izq (Mouse)** | Interactuar (Recoger objetos, Leer notas) |
| **Esc** | Pausar / Salir (si aplica) |

## 🛠️ Arquitectura Técnica (Scripts)
El proyecto sigue una arquitectura modular en C#:

* `GameManager.cs`: Controla el estado del juego (Victoria/Derrota), gestión de llaves y flujo de escenas.
* `FlashlightSystem.cs`: Gestiona la lógica de la luz, el consumo de batería y la actualización del HUD (Slider).
* `SpecterAI.cs`: Máquina de estados simple para el enemigo (Patrulla -> Persecución) usando NavMeshAgent.
* `PlayerInteraction.cs` & `Interactable.cs`: Sistema flexible basado en Enums para definir tipos de interacción (Nota, Batería, Puerta).



