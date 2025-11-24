using UnityEngine;
using TMPro;

// Controla la logica principal de la maquina de Turing 
// incluyendo modos suma y resta, estados, movimiento y control de camaras
public class TuringMachine : MonoBehaviour
{
    public HeadController head;
    public TextMeshProUGUI stateLabel;


    public FollowHead followCamera;   
    public FreeCamera freeCamera;    

    public bool subtractionMode = false;

    public string initialState = "q0";
    public string currentState;

    [System.Serializable]
    public class Rule
    {
        public string state;
        public int readSymbol;
        public string newState;
        public int writeSymbol;
        public int move; 
    }

    public Rule[] rules;
    public Rule[] subtractionRules;

    // Inicializa estado y texto
    void Start()
    {
        currentState = initialState;
        UpdateStateLabel();
    }

    // Actualiza el texto de estado en pantalla
    void UpdateStateLabel()
    {
        if (stateLabel == null) return;

        string modo = subtractionMode ? "Resta" : "Suma";
        string runStatus = autoRunning ? "Running" : "Stopped";

        stateLabel.text = $"Estado: {currentState} ({modo}) [{runStatus}]";
    }

    // Ejecuta un paso de la maquina segun las reglas
    public void Step()
    {
        Rule[] activeRules = subtractionMode ? subtractionRules : rules;
        if (head == null || activeRules == null || activeRules.Length == 0) return;

        int s = head.Read();

        Rule ruleToApply = null;
        foreach (Rule r in activeRules)
        {
            if (r.state == currentState && r.readSymbol == s)
            {
                ruleToApply = r;
                break;
            }
        }

        if (ruleToApply == null)
        {
            Debug.Log("No hay regla para (" + currentState + ", " + s + ").");
            autoRunning = false;
            UpdateStateLabel();
            DisableFollowCamera();
            return;
        }

        head.Write(ruleToApply.writeSymbol);

        if (ruleToApply.move == -1) head.MoveLeft();
        else if (ruleToApply.move == 1) head.MoveRight();

        currentState = ruleToApply.newState;
        UpdateStateLabel();
    }

    // Reinicia la maquina a su estado inicial
    public void ResetMachine()
    {
        autoRunning = false;
        timer = 0f;

        DisableFollowCamera();

        currentState = initialState;
        UpdateStateLabel();

        if (head != null && head.cells != null)
        {
            for (int i = 0; i < head.cells.Length; i++)
                head.cells[i].Write(0);

            head.index = 0;
            head.SnapToCurrent();
        }
    }


    // Configura el modo suma
    public void SetModeSum()
    {
        subtractionMode = false;
        autoRunning = false;
        timer = 0f;

        DisableFollowCamera();

        ResetMachine();

       
        currentState = initialState;
        UpdateStateLabel();
    }

    // Configura el modo resta
    public void SetModeSubtraction()
    {
        subtractionMode = true;
        autoRunning = false;
        timer = 0f;

        DisableFollowCamera();

        ResetMachine();

       
        if (head != null)
        {
            head.index = 1;
            head.SnapToCurrent();
        }

    
        currentState = initialState;
        UpdateStateLabel();
    }


 
    [Header("Auto Run")]

    public bool autoRunning = false;
    public float stepDelay = 0.5f;
    private float timer = 0f;
    // Controla el modo automatico ejecutando pasos cada intervalo
    void Update()
    {
        if (autoRunning)
        {
            timer += Time.deltaTime;
            if (timer >= stepDelay)
            {
                Step();
                timer = 0f;
            }
        }
    }

    // Alterna la ejecucion automatica
    public void ToggleAutoRun()
    {
        autoRunning = !autoRunning;
        timer = 0f;
        UpdateStateLabel();

        if (autoRunning) EnableFollowCamera();
        else DisableFollowCamera();
    }

    // Activa la camara que sigue al cabezal
    void EnableFollowCamera()
    {
        if (followCamera != null)
            followCamera.EnableFollow(true);

        if (freeCamera != null)
            freeCamera.SetFreeMode(false);
    }

    // Desactiva la camara de seguimiento y activa la camara libre
    void DisableFollowCamera()
    {
        if (followCamera != null)
            followCamera.EnableFollow(false);

        if (freeCamera != null)
            freeCamera.SetFreeMode(true);
    }

    // Cambia la velocidad del modo automatico
    public void SetSpeed(float value)
    {
        stepDelay = Mathf.Lerp(0.05f, 1.0f, 1f - value);
    }
}
