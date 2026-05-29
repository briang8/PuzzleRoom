using UnityEngine;

public class WeightPlate : MonoBehaviour
{
    [Header("What counts as weight")]
    public string requiredTag = "HeavyObject";

    [Header("Visuals")]
    public Renderer plateRenderer;
    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.green;

    [Header("Reset")]
    public Transform weightObject;        // drag the heavy object here in Inspector
    public Transform weightStartPosition; // drag an empty GameObject marking its start here

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
            Debug.Log("WeightPlate: puzzle 5 solved.");

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.ShowStatusMessage("Weight in place", 2f);
                PuzzleManager.Instance.SetPuzzleSolved(5, true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        if (isSolved)
        {
            isSolved = false;
            SetVisual(false);
            Debug.Log("WeightPlate: puzzle 5 no longer solved.");

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.ShowStatusMessage("Weight removed", 2f);
                PuzzleManager.Instance.SetPuzzleSolved(5, false);
            }
        }
    }

    public void ResetPuzzle()
    {
        isSolved = false;
        SetVisual(false);

        // Move weight object back to its start position
        if (weightObject != null && weightStartPosition != null)
        {
            Rigidbody rb = weightObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            weightObject.position = weightStartPosition.position;
            weightObject.rotation = weightStartPosition.rotation;
        }
    }

    void SetVisual(bool active)
    {
        if (plateRenderer == null) return;
        plateRenderer.material.color = active ? activeColor : inactiveColor;
    }
}