using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public static LightPuzzle Instance;

    [Header("Target switch states (true = ON, false = OFF)")]
    public bool switch0Target = true;
    public bool switch1Target = false;
    public bool switch2Target = true;

    // Starting states — switches all begin ON to match LightSwitchInteract default
    bool switch0Current = true;
    bool switch1Current = true;
    bool switch2Current = true;

    bool isSolved = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnSwitchToggled(int index, bool isOn)
    {
        if (index == 0) switch0Current = isOn;
        else if (index == 1) switch1Current = isOn;
        else if (index == 2) switch2Current = isOn;

        Debug.Log($"LightPuzzle: switch {index} now {(isOn ? "ON" : "OFF")}");
        CheckState();
    }

    void CheckState()
    {
        bool nowSolved =
            switch0Current == switch0Target &&
            switch1Current == switch1Target &&
            switch2Current == switch2Target;

        if (nowSolved && !isSolved)
        {
            isSolved = true;
            Debug.Log("Light puzzle solved");

            if (PuzzleManager.Instance != null)
                PuzzleManager.Instance.SetPuzzleSolved(4, true);
        }
        else if (!nowSolved && isSolved)
        {
            isSolved = false;
            Debug.Log("Light puzzle no longer solved");

            if (PuzzleManager.Instance != null)
                PuzzleManager.Instance.SetPuzzleSolved(4, false);
        }
    }

    public void ResetPuzzle()
    {
        isSolved = false;

        // Reset every switch back to ON (the default starting state)
        LightSwitchInteract[] switches = FindObjectsByType<LightSwitchInteract>(FindObjectsSortMode.None);
        foreach (LightSwitchInteract sw in switches)
            sw.ResetSwitch();

        // Sync internal state back to default (all ON)
        switch0Current = true;
        switch1Current = true;
        switch2Current = true;

        Debug.Log("LightPuzzle: all switches reset.");
    }
}