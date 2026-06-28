using UnityEngine;

public class LensCorrectoDaltonismo : MonoBehaviour
{
    public bool esCorrecto = false;
    public DaltonismoManager daltonismoManager;
    public Transform jugador;
    public float distanciaActivacion = 10f;

    private bool yaActivado = false;

    void Update()
    {
        Debug.Log("Update corriendo en: " + gameObject.name);

        if (jugador == null)
        {
            Debug.Log("JUGADOR ES NULL");
            return;
        }

        float distancia = Vector3.Distance(transform.position, jugador.position);
        Debug.Log("Distancia a " + gameObject.name + ": " + distancia);

        if (distancia < distanciaActivacion && !yaActivado)
        {
            yaActivado = true;
            Debug.Log("ACTIVADO: " + gameObject.name);

            if (esCorrecto)
                daltonismoManager.LenteCorrecto();
            else
                daltonismoManager.LenteIncorrecto();
        }

        if (distancia > distanciaActivacion && yaActivado)
        {
            yaActivado = false;
        }
    }
}