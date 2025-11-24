using UnityEngine;

// Controla la animacion del boton fisico en el modelo 3D
public class ButtonFisico : MonoBehaviour
{
    public Transform pulsador;  
    public float pressDepth = 2f;
    public float pressDuration = 0.1f;

    bool isPressing = false;
    float pressTime = 0f;
    Vector3 originalLocalPos;

    // Guarda la posicion inicial del pulsador
    void Start()
    {
        if (pulsador != null)
        {
            originalLocalPos = pulsador.localPosition;
        }
    }

    // Ejecuta la animacion de presion del boton
    public void Press()
    {
        if (pulsador == null) return;

    
        isPressing = true;
        pressTime = 0f;
        pulsador.localPosition = originalLocalPos - new Vector3(0f, pressDepth, 0f);
    }

    // Retorna el boton a su posicion original cuando termina la animacion
    void Update()
    {
        if (!isPressing || pulsador == null) return;

        pressTime += Time.deltaTime;
        if (pressTime >= pressDuration)
        {
            pulsador.localPosition = originalLocalPos;
            isPressing = false;
        }
    }
}
