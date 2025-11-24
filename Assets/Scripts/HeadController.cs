using UnityEngine;

// Controla el movimiento del cabezal y la lectura y escritura sobre las celdas de la cinta
public class HeadController : MonoBehaviour
{
    
    public Cell[] cells;      

    // indice de la celda actual
    public int index = 0;     

    // velocidad de movimiento
    public float speed = 5f;   

    
    Vector3 targetPos;

    // Ubica el cabezal en la celda inicial al comenzar
    void Start()
    {
        SnapToCurrent(); 
    }

    // Mueve el cabezal hacia su posicion objetivo
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            speed * Time.deltaTime
        );
    }

    // Coloca el cabezal directamente sobre la celda indicada por index
    public void SnapToCurrent()
    {
       
        if (cells == null || cells.Length == 0) return;

        Vector3 p = cells[index].transform.position;
        targetPos = new Vector3(p.x, transform.position.y, transform.position.z);
        transform.position = targetPos;
    }

    // Actualiza la posicion segun el indice actual
    void SetTargetFromIndex()
    {
      
        if (cells == null || cells.Length == 0) return;

        Vector3 p = cells[index].transform.position;
        targetPos = new Vector3(p.x, transform.position.y, transform.position.z);
    }


    public void MoveLeft()
    {
        
        if (index > 0)
        {
            index--;
            SetTargetFromIndex();
        }
    }

    public void MoveRight()
    {
       
        if (cells != null && index < cells.Length - 1)
        {
            index++;
            SetTargetFromIndex();
        }
    }

    public int Read()
    {
       
        return cells[index].Read();
    }

    public void Write(int symbol)
    {
     
        cells[index].Write(symbol);
    }
}
