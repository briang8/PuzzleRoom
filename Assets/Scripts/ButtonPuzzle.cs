using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public static ButtonPuzzle Instance;

    public int[] correctSequence = new int[] { 0, 1, 2 };
    public int maxInputs = 3;

    private List<int> inputs = new List<int>();
    bool isSolved = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Press(int index)
    {
        inputs.Add(index);
        Debug.Log("Sequence so far: " + string.Join(",", inputs));

        if (inputs.Count >= maxInputs)
        {
            CheckSequence();
        }
    }

    void CheckSequence()
    {
        bool ok = true;
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (i >= inputs.Count || inputs[i] != correctSequence[i])
            {
                ok = false;
                break;
            }
        }

        if (ok)
        {
            if (!isSolved)
            {
                isSolved = true;
                Debug.Log("Button puzzle solved");
                PuzzleManager.Instance.SetPuzzleSolved(2, true);
            }
        }
        else
        {
            if (isSolved)
            {
                isSolved = false;
                Debug.Log("Button puzzle no longer solved");
                PuzzleManager.Instance.SetPuzzleSolved(2, false);
            }
            else
            {
                Debug.Log("Button puzzle failed attempt");
            }
        }

        inputs.Clear();
    }

    public void ResetPuzzle()
    {
        isSolved = false;
        inputs.Clear();
        PuzzleManager.Instance.SetPuzzleSolved(2, false);
    }
}