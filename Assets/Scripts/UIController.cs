using UnityEngine;

public class UIController : MonoBehaviour
{
    public HeadController head;             // arrastrar el objeto Head aqui
    public NylonController nylonController; // arrastrar el Motor con NylonController aqui
    public TuringMachine turing;            // arrastrar el objeto World que tiene TuringMachine

    // botones fisicos
    public ButtonFisico botonFisicoStep;
    public ButtonFisico botonFisicoReset;
    public ButtonFisico botonFisicoSuma;
    public ButtonFisico botonFisicoResta;
    public ButtonFisico botonFisicoRun;

    // 🆕 botones fisicos extra
    public ButtonFisico botonFisicoLeft;
    public ButtonFisico botonFisicoRight;
    public ButtonFisico botonFisicoToggle;

    // llamado por el boton Left del canvas
    public void BtnLeft()
    {
        if (head != null)
        {
            head.MoveLeft();
            if (nylonController != null) nylonController.GirarCarreteIzquierda();
        }

        // animar boton fisico
        if (botonFisicoLeft != null)
        {
            botonFisicoLeft.Press();
        }
    }

    // llamado por el boton Right del canvas
    public void BtnRight()
    {
        if (head != null)
        {
            head.MoveRight();
            if (nylonController != null) nylonController.GirarCarreteDerecha();
        }

        // animar boton fisico
        if (botonFisicoRight != null)
        {
            botonFisicoRight.Press();
        }
    }

    // llamado por el boton Toggle del canvas
    public void BtnToggle()
    {
        if (head == null || head.cells == null || head.cells.Length == 0) return;

        int s = head.Read();
        int nuevo = (s + 1) % 4; // 0->1->2->3->0
        head.Write(nuevo);

        // animar boton fisico
        if (botonFisicoToggle != null)
        {
            botonFisicoToggle.Press();
        }
    }

    // llamado por el boton Step del canvas
    public void BtnStep()
    {
        if (turing != null)
        {
            turing.Step();
        }

        if (botonFisicoStep != null)
        {
            botonFisicoStep.Press();
        }
    }

    // llamado por el boton Reset del canvas
    public void BtnReset()
    {
        if (turing != null)
        {
            turing.ResetMachine();
        }

        if (botonFisicoReset != null)
        {
            botonFisicoReset.Press();
        }
    }

    // llamado por el boton Suma del canvas
    public void BtnSum()
    {
        if (turing != null)
        {
            turing.SetModeSum();
        }

        if (botonFisicoSuma != null)
        {
            botonFisicoSuma.Press();
        }
    }

    // llamado por el boton Resta del canvas
    public void BtnSub()
    {
        if (turing != null)
        {
            turing.SetModeSubtraction();
        }

        if (botonFisicoResta != null)
        {
            botonFisicoResta.Press();
        }
    }

    // llamado por el boton Run / Stop del canvas
    public void BtnRunStop()
    {
        if (turing != null)
        {
            turing.ToggleAutoRun();
        }

        if (botonFisicoRun != null)
        {
            botonFisicoRun.Press();
        }
    }
}
