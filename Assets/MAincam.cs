using UnityEngine;

public class SeleccionMouseLentes : MonoBehaviour
{
    public float distancia = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(rayo, out RaycastHit hit, distancia))
            {
                Debug.Log("Apunté a: " + hit.collider.name);

                LenteSeleccionable lente = hit.collider.GetComponent<LenteSeleccionable>();

                if (lente != null)
                    lente.Seleccionar();
            }
        }
    }
}