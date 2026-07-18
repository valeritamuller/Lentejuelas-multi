using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensCorrectoMiopia : MonoBehaviour
{
    public bool esCorrecto = false;

    public Volume globalVolume;

    [Header("Mensajes")]
    public GameObject cartelCorrecto;
    public GameObject cartelIncorrecto;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    private AudioSource audioSource;
    private DepthOfField dof;

    // Guarda cuál fue la última opción seleccionada
    private static LensCorrectoMiopia ultimaSeleccion;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (globalVolume != null)
            globalVolume.profile.TryGet(out dof);
    }

    public void Seleccionar()
    {
        // Cancela el temporizador de la selección anterior
        if (ultimaSeleccion != null)
        {
            ultimaSeleccion.CancelInvoke();
            ultimaSeleccion.OcultarTodosLosCarteles();
        }

        ultimaSeleccion = this;

        // Evita que los dos mensajes se vean al mismo tiempo
        OcultarTodosLosCarteles();

        if (esCorrecto)
        {
            if (dof != null)
            {
                dof.gaussianStart.value = 50f;
                dof.gaussianEnd.value = 100f;
                dof.gaussianMaxRadius.value = 0f;
            }

            if (cartelCorrecto != null)
            {
                cartelCorrecto.SetActive(true);
                Invoke(nameof(OcultarCartelCorrecto), 5f);
            }

            if (audioSource != null && sonidoCorrecto != null)
                audioSource.PlayOneShot(sonidoCorrecto);
        }
        else
        {
            if (cartelIncorrecto != null)
            {
                cartelIncorrecto.SetActive(true);
                Invoke(nameof(OcultarCartelIncorrecto), 5f);
            }

            if (audioSource != null && sonidoIncorrecto != null)
                audioSource.PlayOneShot(sonidoIncorrecto);
        }
    }

    private void OcultarTodosLosCarteles()
    {
        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);

        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);
    }

    private void OcultarCartelCorrecto()
    {
        if (cartelCorrecto != null)
            cartelCorrecto.SetActive(false);
    }

    private void OcultarCartelIncorrecto()
    {
        if (cartelIncorrecto != null)
            cartelIncorrecto.SetActive(false);
    }
}