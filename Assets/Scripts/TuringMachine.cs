using UnityEngine;
using TMPro;

public class TuringMachine : MonoBehaviour
{
    public HeadController head;
    public TextMeshProUGUI stateLabel;

    // false = suma, true = resta
    public bool subtractionMode = false;

    // estado inicial (usa el mismo para suma y resta si quieres)
    public string initialState = "q0";
    public string currentState;

    [System.Serializable]
    public class Rule
    {
        public string state;      // qi
        public int readSymbol;    // simbolo leido (0,1,2,3)
        public string newState;   // qj
        public int writeSymbol;   // simbolo escrito
        public int move;          // -1 = L, 0 = S, 1 = R
    }

    // Reglas de SUMA (las que ya tienes funcionando)
    public Rule[] rules;

    // Reglas de RESTA (las nuevas que hiciste con B,1,0,X)
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


    public void Step()
    {
        Rule[] activeRules = subtractionMode ? subtractionRules : rules;
        if (head == null || activeRules == null || activeRules.Length == 0) return;

        int s = head.Read();

        // buscar regla δ(state, symbol)
        Rule ruleToApply = null;
        foreach (Rule r in activeRules)
        {
            if (r.state == currentState && r.readSymbol == s)
            {
                ruleToApply = r;
                break;
            }
        }

        // si no hay regla, la máquina se detiene en este estado
        if (ruleToApply == null)
        {
            Debug.Log("No hay regla para (" + currentState + ", " + s + "). La maquina se detiene.");
            return;
        }

        // 1) escribir simbolo
        head.Write(ruleToApply.writeSymbol);

        // 2) mover cabezal
        if (ruleToApply.move == -1) head.MoveLeft();
        else if (ruleToApply.move == 1) head.MoveRight();
        // 0 = quieto

        // 3) cambiar estado
        currentState = ruleToApply.newState;

        // 4) actualizar texto
        UpdateStateLabel();
    }

    public void ResetMachine()
    {
        // detener ejecución automática
        autoRunning = false;
        timer = 0f;

        currentState = initialState;
        UpdateStateLabel();

        if (head != null && head.cells != null)
        {
            for (int i = 0; i < head.cells.Length; i++)
            {
                head.cells[i].Write(0); // B
            }

            head.index = 0;
            head.SnapToCurrent();
        }
    }


    public void SetModeSum()
    {
        subtractionMode = false;
        autoRunning = false;  // detener
        timer = 0f;

        ResetMachine();
    }

    public void SetModeSubtraction()
    {
        subtractionMode = true;
        autoRunning = false;  // detener
        timer = 0f;

        ResetMachine();

        // mover el cabezal a la segunda celda
        if (head != null)
        {
            head.index = 1;
            head.SnapToCurrent();
        }
    }

    // ---- MODO AUTO ----
    [Header("Auto Run")]
    public bool autoRunning = false;
    public float stepDelay = 0.5f; // segundos entre pasos
    private float timer = 0f;

    void Update()
    {
        // si estamos en modo automático
        if (autoRunning)
        {
            timer += Time.deltaTime;
            if (timer >= stepDelay)
            {
                Step();       // ejecutar un paso
                timer = 0f;   // reiniciar temporizador
            }
        }
    }

    // iniciar o detener ejecución automática
    public void ToggleAutoRun()
    {
        autoRunning = !autoRunning;
        timer = 0f;
        UpdateStateLabel(); // ← actualiza el texto inmediatamente
    }

    public void SetSpeed(float value)
    {
        // invertir para que valores altos signifiquen más velocidad
        stepDelay = Mathf.Lerp(0.05f, 1.0f, 1f - value);
    }

}
