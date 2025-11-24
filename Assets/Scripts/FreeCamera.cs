using UnityEngine;

// Controla la camara libre permitiendo moverla con WASD y mirar con el mouse
public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float fastMultiplier = 3f;
    public float mouseSensitivity = 2f;

    [Header("Referencia al FollowHead del mismo rig")]
    public FollowHead followRig;

    bool freeMode = true;   
    float rotX;
    float rotY;

    // Inicializa la rotacion inicial de la camara
    void Start()
    {
        Vector3 angles = transform.localEulerAngles;
        rotX = angles.y;
        rotY = angles.x;
    }

    // Activa o desactiva el modo de camara libre
    public void SetFreeMode(bool free)
    {
        freeMode = free;
    }

    // Actualiza el movimiento y la rotacion de la camara libre
    void Update()
    {
        
        if (!freeMode) return;

       
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotX += mouseX;
            rotY -= mouseY;
            rotY = Mathf.Clamp(rotY, -80f, 80f);

            transform.localRotation = Quaternion.Euler(rotY, rotX, 0f);
        }

        
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMultiplier;

        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");   

        float upDown = 0f;
        if (Input.GetKey(KeyCode.E)) upDown += 1f;
        if (Input.GetKey(KeyCode.Q)) upDown -= 1f;

        Vector3 dir =
            (transform.forward * v +
             transform.right * h +
             transform.up * upDown).normalized;

       
        transform.localPosition += dir * speed * Time.deltaTime;
    }
}
