using UnityEngine;
using TMPro;

public class TuringMachine : MonoBehaviour
{
    public HeadController head;
    public TextMeshProUGUI stateLabel;

    // --- NUEVO ---
    public FollowHead followCamera;   // arrastrar CameraRig
    public FreeCamera freeCamera;     // arrastrar CameraRig también
    // --------------

    // false = suma, true = resta
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
        public int move;  // -1 = L, 0 = S, 1 = R
    }

    public Rule[] rules;
    public Rule[] subtractionRules;

    void Start()
    {
        currentState = initialState;
        UpdateStateLabel();
    }

    void UpdateStateLabel()
    {
        if (stateLabel == null) return;

        string modo = subtractionMode ? "Resta" : "Suma";
        string runStatus = autoRunning ? "Running" : "Stopped";

        stateLabel.text = $"Estado: {currentState} ({modo}) [{runStatus}]";
    }

    // -------------------------
    // LOGICA PRINCIPAL DE PASOS
    // -------------------------
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

    // -------------------------
    // RESET
    // -------------------------
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


    // -------------------------
    // MODO SUMA / RESTA
    // -------------------------
    public void SetModeSum()
    {
        subtractionMode = false;
        autoRunning = false;
        timer = 0f;

        DisableFollowCamera();

        ResetMachine();

        // asegurar estado inicial otra vez
        currentState = initialState;
        UpdateStateLabel();
    }


    public void SetModeSubtraction()
    {
        subtractionMode = true;
        autoRunning = false;
        timer = 0f;

        DisableFollowCamera();

        ResetMachine();

        // mover el cabezal a la segunda celda
        if (head != null)
        {
            head.index = 1;
            head.SnapToCurrent();
        }

        // asegurar estado inicial
        currentState = initialState;
        UpdateStateLabel();
    }


    // -------------------------
    // AUTO RUN
    // -------------------------
    [Header("Auto Run")]
    public bool autoRunning = false;
    public float stepDelay = 0.5f;
    private float timer = 0f;

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

    public void ToggleAutoRun()
    {
        autoRunning = !autoRunning;
        timer = 0f;
        UpdateStateLabel();

        if (autoRunning) EnableFollowCamera();
        else DisableFollowCamera();
    }

    // -------------------------
    // CAMARA
    // -------------------------
    void EnableFollowCamera()
    {
        if (followCamera != null)
            followCamera.EnableFollow(true);

        if (freeCamera != null)
            freeCamera.SetFreeMode(false);
    }

    void DisableFollowCamera()
    {
        if (followCamera != null)
            followCamera.EnableFollow(false);

        if (freeCamera != null)
            freeCamera.SetFreeMode(true);
    }

    public void SetSpeed(float value)
    {
        stepDelay = Mathf.Lerp(0.05f, 1.0f, 1f - value);
    }
}
