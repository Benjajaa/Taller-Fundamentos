using UnityEngine;

// Controla la extension visual del cable entre dos puntos
public class CableStretch : MonoBehaviour
{
    public Transform startPoint;   
    public Transform endPoint;     
    public float thickness = 0.03f;

    // Actualiza la posicion, rotacion y escala del cable
    void Update()
    {
        if (startPoint == null || endPoint == null) return;

        Vector3 p0 = startPoint.position;
        Vector3 p1 = endPoint.position;

        Vector3 dir = p1 - p0;
        float length = dir.magnitude;

        if (length < 0.0001f) return;

        
        transform.position = (p0 + p1) / 2f;

        
        transform.rotation = Quaternion.LookRotation(dir);

      
        Vector3 s = transform.localScale;
        s.x = thickness;
        s.y = thickness;
        s.z = length;
        transform.localScale = s;
    }
}
