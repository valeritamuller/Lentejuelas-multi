using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Transform doorPivot;
    public float openAngle = 90f;
    public float speed = 3f;

    public bool openInside = true;

    private Quaternion closedRot;
    private Quaternion openRot;
    private bool playerInside = false;

    void Start()
    {
        closedRot = doorPivot.rotation;

        float finalAngle = openInside ? -openAngle : openAngle;

        openRot = Quaternion.Euler(
            doorPivot.eulerAngles.x,
            doorPivot.eulerAngles.y + finalAngle,
            doorPivot.eulerAngles.z
        );
    }

    void Update()
    {
        doorPivot.rotation = Quaternion.Lerp(
            doorPivot.rotation,
            playerInside ? openRot : closedRot,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            playerInside = false;
    }
}