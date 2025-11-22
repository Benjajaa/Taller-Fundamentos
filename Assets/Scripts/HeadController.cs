using UnityEngine;

public class HeadController : MonoBehaviour
{
    // arreglo de celdas de la cinta en orden
    public Cell[] cells;       // arrastrar Cell_0 hasta Cell_19

    // indice de la celda actual
    public int index = 0;      // comienza en la primera celda

    // velocidad de movimiento
    public float speed = 5f;   // unidades por segundo

    // posicion objetivo del cabezal
    Vector3 targetPos;

    void Start()
    {
        SnapToCurrent(); // colocar el cabezal en la celda actual
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }


    public void SnapToCurrent()
    {
        // colocar el cabezal directamente sobre la celda actual
        if (cells == null || cells.Length == 0) return;

        Vector3 p = cells[index].transform.position;
        targetPos = new Vector3(p.x, transform.position.y, transform.position.z);
        transform.position = targetPos;
    }

    void SetTargetFromIndex()
    {
        // actualizar la posicion objetivo segun el indice
        if (cells == null || cells.Length == 0) return;

        Vector3 p = cells[index].transform.position;
        targetPos = new Vector3(p.x, transform.position.y, transform.position.z);
    }

    public void MoveLeft()
    {
        // mover una celda a la izquierda
        if (index > 0)
        {
            index--;
            SetTargetFromIndex();
        }
    }

    public void MoveRight()
    {
        // mover una celda a la derecha
        if (cells != null && index < cells.Length - 1)
        {
            index++;
            SetTargetFromIndex();
        }
    }

    public int Read()
    {
        // leer simbolo de la celda actual
        return cells[index].Read();
    }

    public void Write(int symbol)
    {
        // escribir simbolo en la celda actual
        cells[index].Write(symbol);
    }
}
