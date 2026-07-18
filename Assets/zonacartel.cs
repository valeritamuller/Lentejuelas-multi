using UnityEngine;

public class MostrarCartelPorDistancia : MonoBehaviour
{
    [SerializeField] private Transform jugador;
    [SerializeField] private GameObject cartel;
    [SerializeField] private float distanciaParaMostrar = 4f;

    private void Start()
    {
        if (cartel != null)
            cartel.SetActive(false);
    }

    private void Update()
    {
        if (jugador == null || cartel == null)
            return;

        float distancia = Vector3.Distance(
            jugador.position,
            transform.position
        );

        cartel.SetActive(distancia <= distanciaParaMostrar);
    }
}