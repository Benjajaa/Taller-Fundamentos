using UnityEngine;

public class FollowHead : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -2f);   // altura y distancia
    public float followSpeed = 5f;

    [Header("Limites en mundo")]
    public float minX = -5f;
    public float maxX = 30f;
    public float minZ = -5f;
    public float maxZ = 10f;

    [Header("Rotacion al seguir")]
    public Vector3 followRotationEuler = new Vector3(80f, 0f, 0f); // vista cenital inclinada

    [HideInInspector]
    public bool followEnabled = false;

    public void EnableFollow(bool on)
    {
        followEnabled = on;

        if (on)
        {
            SnapNow();
        }
    }

    public void SnapNow()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        desired.x = Mathf.Clamp(desired.x, minX, maxX);
        desired.z = Mathf.Clamp(desired.z, minZ, maxZ);

        transform.position = desired;
        transform.rotation = Quaternion.Euler(followRotationEuler);
    }

    void LateUpdate()
    {
        if (!followEnabled || target == null) return;

        Vector3 desired = target.position + offset;
        desired.x = Mathf.Clamp(desired.x, minX, maxX);
        desired.z = Mathf.Clamp(desired.z, minZ, maxZ);

        // mover suave
        transform.position = Vector3.Lerp(
            transform.position,
            desired,
            followSpeed * Time.deltaTime
        );

        // siempre misma rotacion cuando seguimos
        transform.rotation = Quaternion.Euler(followRotationEuler);
    }
}
