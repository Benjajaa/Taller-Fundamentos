using UnityEngine;

public class CableStretch : MonoBehaviour
{
    public Transform startPoint;   // AnchorCableHead
    public Transform endPoint;     // AnchorCableFixed
    public float thickness = 0.03f;

    void Update()
    {
        if (startPoint == null || endPoint == null) return;

        Vector3 p0 = startPoint.position;
        Vector3 p1 = endPoint.position;

        Vector3 dir = p1 - p0;
        float length = dir.magnitude;

        if (length < 0.0001f) return;

        // posición = punto medio
        transform.position = (p0 + p1) / 2f;

        // rotación = mirar hacia el punto fijo
        transform.rotation = Quaternion.LookRotation(dir);

        // escala = grosor + largo
        Vector3 s = transform.localScale;
        s.x = thickness;
        s.y = thickness;
        s.z = length;
        transform.localScale = s;
    }
}
