using UnityEngine;

public class UIController : MonoBehaviour
{
    public HeadController head; // arrastrar el objeto Head aqui

    // llamado por el boton Left
    public void BtnLeft()
    {
        if (head != null) head.MoveLeft();
    }

    // llamado por el boton Right
    public void BtnRight()
    {
        if (head != null) head.MoveRight();
    }

    // llamado por el boton Toggle
    public void BtnToggle()
    {
        if (head == null || head.cells == null || head.cells.Length == 0) return;

        int s = head.Read();
        int nuevo = (s + 1) % 4; // 0->1->2->3->0
        head.Write(nuevo);
    }

}
