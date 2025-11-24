using UnityEngine;

// Representa una celda de la cinta y actualiza su material segun el simbolo 
public class Cell : MonoBehaviour
{
    public Renderer rend;

    
    [Range(0, 3)]
    public int symbol = 0;

    public Material matB;      // B
    public Material matOne;    // 1
    public Material matZero;   // 0 (separador)
    public Material matX;      // X (Led Tachado)

    // Actualiza el material inicial
    void Awake()
    {
        rend = GetComponent<Renderer>();
        Refresh();
    }

    // Lee el simbolo actual de la celda
    public int Read()
    {
        return symbol;
    }

    // Escribe un simbolo en la celda y refresca su material
    public void Write(int s)
    {
        symbol = Mathf.Clamp(s, 0, 3);
        Refresh();
    }

    // Actualiza el material segun el simbolo actual
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
