using UnityEngine;

public class ActivadorMacular : MonoBehaviour
{
    public GameObject manchaMacular;
    public Transform jugador;
    public Transform zonaTarea;
    public float distanciaActivacion = 10f;

    private bool activado = false;

    void Start()
    {
        if (manchaMacular != null)
            manchaMacular.SetActive(false);
    }

    void Update()
    {
        if (activado) return;
        if (jugador == null || zonaTarea == null) return;

        float distancia = Vector3.Distance(jugador.position, zonaTarea.position);
        Debug.Log("Distancia macular: " + distancia);

        if (distancia < distanciaActivacion)
        {
            activado = true;

            if (manchaMacular != null)
                manchaMacular.SetActive(true);

            Debug.Log("MANCHA ACTIVADA");
        }
    }
}