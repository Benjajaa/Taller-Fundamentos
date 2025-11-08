using UnityEngine;

public class Cell : MonoBehaviour
{
    public Renderer rend;

    // 0 = B (apagado), 1 = 1, 2 = 0 (separador), 3 = X (tachado)
    [Range(0, 3)]
    public int symbol = 0;

    public Material matB;      // B
    public Material matOne;    // 1
    public Material matZero;   // 0 (separador)
    public Material matX;      // X (tachado)

    void Awake()
    {
        // siempre usar el renderer del propio cubo
        rend = GetComponent<Renderer>();
        Refresh();
    }

    public int Read()
    {
        return symbol;
    }

    public void Write(int s)
    {
        symbol = Mathf.Clamp(s, 0, 3);
        Refresh();
    }

    void Refresh()
    {
        if (!rend) return;

        if (symbol == 0 && matB != null)
            rend.material = matB;
        else if (symbol == 1 && matOne != null)
            rend.material = matOne;
        else if (symbol == 2 && matZero != null)
            rend.material = matZero;
        else if (symbol == 3 && matX != null)
            rend.material = matX;
    }
}
