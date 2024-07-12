using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset;    

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("El objetivo no está asignado en el script CameraFollow.");
            return;
        }
        transform.position = target.position + offset;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}