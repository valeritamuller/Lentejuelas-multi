using UnityEngine;

public class ClickLenteDaltonismo : MonoBehaviour
{
    public float distanciaMaxima = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayo = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)
            );

            if (Physics.Raycast(rayo, out RaycastHit hit, distanciaMaxima))
            {
                Debug.Log("Le pegué a: " + hit.collider.name);

                LensCorrectoMiopia miopia = hit.collider.GetComponentInParent<LensCorrectoMiopia>();
                if (miopia != null)
                {
                    miopia.Seleccionar();
                    return;
                }

                LensCorrectoDaltonismo daltonismo = hit.collider.GetComponentInParent<LensCorrectoDaltonismo>();
                if (daltonismo != null)
                {
                    daltonismo.Seleccionar();
                    return;
                }

                LensCorrectoAstigmatismo astigmatismo = hit.collider.GetComponentInParent<LensCorrectoAstigmatismo>();
                if (astigmatismo != null)
                {
                    astigmatismo.Seleccionar();
                    return;
                }

                LensCorrectoDegeneracion degeneracion = hit.collider.GetComponentInParent<LensCorrectoDegeneracion>();
                if (degeneracion != null)
                {
                    degeneracion.Seleccionar();
                    return;
                }

                Debug.Log("Ese objeto no tiene script de lente.");
            }
            else
            {
                Debug.Log("No le pegué a nada.");
            }
        }
    }
}