using UnityEngine;

public class ClickLenteDaltonismo : MonoBehaviour
{
    public float distanciaMaxima = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hice click");

            Ray rayo = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
            );

            if (Physics.Raycast(rayo, out RaycastHit hit, distanciaMaxima))
            {
                Debug.Log("Le pegué a: " + hit.collider.name);

                LensCorrectoDaltonismo lente = hit.collider.GetComponent<LensCorrectoDaltonismo>();

                if (lente != null)
                    lente.Seleccionar();
                else
                    Debug.Log("Ese objeto no tiene LensCorrectoDaltonismo");
            }
            else
            {
                Debug.Log("No le pegué a nada");
            }
        }
    }
}