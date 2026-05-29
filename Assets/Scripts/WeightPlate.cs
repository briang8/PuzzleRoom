using UnityEngine;

public class WeightPlate : MonoBehaviour
{
    [Header("What counts as weight")]
    public string requiredTag = "HeavyObject"; // tag for the heavy object

    [Header("Visuals")]
    public Renderer plateRenderer;
    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.green;

    bool isSolved = false;

    void Start()
    {
        if (plateRenderer == null)
            plateRenderer = GetComponent<Renderer>();

        SetVisual(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        if (!isSolved)
        {
            isSolved = true;
            SetVisual(true);
            Debug.Log("WeightPlate: puzzle 5 solved (weight on plate).");

            if (PuzzleManager.Instance != null)
                PuzzleManager.Instance.ShowStatusMessage("Weight in place", 2f);

            PuzzleManager.Instance.SetPuzzleSolved(5, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        if (isSolved)
        {
            isSolved = false;
            SetVisual(false);
            Debug.Log("WeightPlate: puzzle 5 no longer solved (weight left).");

            if (PuzzleManager.Instance != null)
                PuzzleManager.Instance.ShowStatusMessage("Weight removed", 2f);

            PuzzleManager.Instance.SetPuzzleSolved(5, false);
        }
    }

    void SetVisual(bool active)
    {
        if (plateRenderer == null) return;

        plateRenderer.material.color = active ? activeColor : inactiveColor;
    }
}