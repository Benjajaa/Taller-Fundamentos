using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float fastMultiplier = 3f;
    public float mouseSensitivity = 2f;

    [Header("Referencia al FollowHead del mismo rig")]
    public FollowHead followRig;

    bool freeMode = true;   // empezamos en modo libre
    float rotX;
    float rotY;

    void Start()
    {
        Vector3 angles = transform.localEulerAngles;
        rotX = angles.y;
        rotY = angles.x;
    }

    public void SetFreeMode(bool free)
    {
        freeMode = free;
    }

    void Update()
    {
        // si NO estamos en modo libre, esta camara no toca nada
        if (!freeMode) return;

        // mirar con el mouse (botón derecho)
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotX += mouseX;
            rotY -= mouseY;
            rotY = Mathf.Clamp(rotY, -80f, 80f);

            transform.localRotation = Quaternion.Euler(rotY, rotX, 0f);
        }

        // mover con WASD + Q/E
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMultiplier;

        float h = Input.GetAxis("Horizontal"); // A / D
        float v = Input.GetAxis("Vertical");   // W / S

        float upDown = 0f;
        if (Input.GetKey(KeyCode.E)) upDown += 1f;
        if (Input.GetKey(KeyCode.Q)) upDown -= 1f;

        Vector3 dir =
            (transform.forward * v +
             transform.right * h +
             transform.up * upDown).normalized;

        Vector3 newLocalPos = transform.localPosition + dir * speed * Time.deltaTime;

        // limites (ajusta si quieres)
        newLocalPos.x = Mathf.Clamp(newLocalPos.x, -15f, 15f);
        newLocalPos.z = Mathf.Clamp(newLocalPos.z, -15f, 15f);
        newLocalPos.y = Mathf.Clamp(newLocalPos.y, -2f, 5f);

        transform.localPosition = newLocalPos;
    }
}
