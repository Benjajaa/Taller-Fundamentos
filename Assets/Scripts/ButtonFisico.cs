using UnityEngine;

public class ButtonFisico : MonoBehaviour
{
    public Transform pulsador;   // cilindro que hace de boton
    public float pressDepth = 2f;
    public float pressDuration = 0.1f;

    bool isPressing = false;
    float pressTime = 0f;
    Vector3 originalLocalPos;

    void Start()
    {
        if (pulsador != null)
        {
            originalLocalPos = pulsador.localPosition;
        }
    }

    public void Press()
    {
        if (pulsador == null) return;

        // bajar el pulsador en eje Y local
        isPressing = true;
        pressTime = 0f;
        pulsador.localPosition = originalLocalPos - new Vector3(0f, pressDepth, 0f);
    }

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
