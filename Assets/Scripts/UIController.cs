using UnityEngine;

// Controla la comunicacion entre la interfaz y la maquina ( Botones y acciones del cabezal)
public class UIController : MonoBehaviour
{
    public HeadController head;          
    public NylonController nylonController;
    public TuringMachine turing;           


    public ButtonFisico botonFisicoStep;
    public ButtonFisico botonFisicoReset;
    public ButtonFisico botonFisicoSuma;
    public ButtonFisico botonFisicoResta;
    public ButtonFisico botonFisicoRun;


    public ButtonFisico botonFisicoLeft;
    public ButtonFisico botonFisicoRight;
    public ButtonFisico botonFisicoToggle;


    public void BtnLeft()
    {
        if (head != null)
        {
            head.MoveLeft();
            if (nylonController != null) nylonController.GirarCarreteIzquierda();
        }


        if (botonFisicoLeft != null)
        {
            botonFisicoLeft.Press();
        }
    }


    public void BtnRight()
    {
        if (head != null)
        {
            head.MoveRight();
            if (nylonController != null) nylonController.GirarCarreteDerecha();
        }

 
        if (botonFisicoRight != null)
        {
            botonFisicoRight.Press();
        }
    }


    public void BtnToggle()
    {
        if (head == null || head.cells == null || head.cells.Length == 0) return;

        int s = head.Read();
        int nuevo = (s + 1) % 4; 
        head.Write(nuevo);

        if (botonFisicoToggle != null)
        {
            botonFisicoToggle.Press();
        }
    }


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
