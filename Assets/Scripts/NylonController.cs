using UnityEngine;

// Controla la rotacion del carrete y el estiramiento del nylon segun la posicion del carrito
public class NylonController : MonoBehaviour
{
    public Transform carrete;        
    public Transform carrito;        
    public Transform nylon;          
    public float giroPorPaso = 45f;  // grados que gira el carrete por movimiento

    private Vector3 inicioCarrete;

    // Guarda la posicion inicial desde donde sale el nylon
    void Start()
    {
        inicioCarrete = carrete.position;
    }

    // Actualiza el estiramiento y rotacion del nylon cada frame
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

    // Calcula la posicion, rotacion y escala del nylon segun la distancia al carrito
    private void ActualizarNylon()
    {
      
        Vector3 finCarrito = carrito.position;

   
        Vector3 direccion = finCarrito - inicioCarrete;
        float distancia = direccion.magnitude;

        if (distancia < 0.001f) return;

      
        nylon.position = inicioCarrete + direccion / 2f;

    
        Vector3 escala = nylon.localScale;
        escala.y = distancia / 2f;
        nylon.localScale = escala;

      
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, direccion);
        nylon.rotation = rot;
    }
}
