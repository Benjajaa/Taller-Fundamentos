using UnityEngine;

public class NylonController : MonoBehaviour
{
    public Transform carrete;        // Carrete del motor
    public Transform carrito;        // CarritoRail (pieza gris que se mueve con el head)
    public Transform nylon;          // Cilindro largo del nylon
    public float giroPorPaso = 45f;  // grados que gira el carrete por movimiento

    private Vector3 inicioCarrete;   // Punto fijo del motor

    void Start()
    {
        // Guardamos el punto donde sale el nylon (centro del carrete)
        inicioCarrete = carrete.position;
    }

    void Update()
    {
        ActualizarNylon();
    }

    public void GirarCarreteDerecha()
    {
        carrete.Rotate(0f, 0f, giroPorPaso);
    }

    public void GirarCarreteIzquierda()
    {
        carrete.Rotate(0f, 0f, -giroPorPaso);
    }

    private void ActualizarNylon()
    {
        // Fin del nylon = posición del carrito
        Vector3 finCarrito = carrito.position;

        // Vector desde el carrete hasta el carrito
        Vector3 direccion = finCarrito - inicioCarrete;
        float distancia = direccion.magnitude;

        if (distancia < 0.001f) return;

        // Posición del nylon = punto medio entre carrete y carrito
        nylon.position = inicioCarrete + direccion / 2f;

        // Escala: el cilindro se estira en Y
        Vector3 escala = nylon.localScale;
        escala.y = distancia / 2f;
        nylon.localScale = escala;

        // Rotación: hacer que el eje Y del cilindro apunte hacia el carrito
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, direccion);
        nylon.rotation = rot;
    }
}
