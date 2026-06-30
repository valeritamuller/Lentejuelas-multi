using UnityEngine;

public class ManchaAparece : MonoBehaviour
{
    public GameObject mancha;
    public Transform jugador;
    public Transform zona;

    public float distancia = 10f;

    bool activado = false;

    void Update()
    {
        if (activado)
            return;

        if (jugador == null || zona == null || mancha == null)
            return;

        if (Vector3.Distance(jugador.position, zona.position) < distancia)
        {
            activado = true;
            mancha.SetActive(true);
        }
    }
}