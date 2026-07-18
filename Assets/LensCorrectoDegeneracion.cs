using UnityEngine;

public class LensCorrectoDegeneracion : MonoBehaviour
{
    public bool esCorrecto = false;

    public GameObject mancha;

    [Header("Mensajes")]
    public GameObject cartelCorrecto;
    public GameObject cartelIncorrecto;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;

    private AudioSource audioSource;

    // Guarda la última opción seleccionada
    private static LensCorrectoDegeneracion ultimaSeleccion;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Seleccionar()
    {
        // Cancela el temporizador y oculta el cartel de la opción anterior
        if (ultimaSeleccion != null)
        {
            ultimaSeleccion.CancelInvoke();
            ultimaSeleccion.OcultarTodosLosCarteles();
        }

        ultimaSeleccion = this;

        // Evita que los dos carteles estén visibles juntos
        OcultarTodosLosCarteles();

        if (esCorrecto)
        {
            if (mancha != null)
                mancha.SetActive(false);

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