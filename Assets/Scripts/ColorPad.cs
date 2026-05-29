using UnityEngine;

public class ColorPad : MonoBehaviour
{
    [Header("Cube reference")]
    public ColorCube targetCube;

    [Header("Pad visuals")]
    public Renderer padRenderer;
    public Color activeColor = Color.green;
    public Color inactiveColor = Color.gray;

    [Header("Pad trigger")]
    public Collider padTrigger;

    bool cubeOnPad = false;
    bool isSolved = false;

    void Start()
    {
        if (padRenderer == null)
            padRenderer = GetComponent<Renderer>();

        if (padTrigger == null)
            padTrigger = GetComponent<Collider>();

        if (padTrigger != null)
            padTrigger.isTrigger = true;

        SetVisible(false);
        SetColor(false);
    }

    void OnTriggerEnter(Collider other)
    {
        ColorCube cube = other.GetComponent<ColorCube>();
        if (cube == null || cube != targetCube) return;

        cubeOnPad = true;
        UpdateState();
    }

    void OnTriggerExit(Collider other)
    {
        ColorCube cube = other.GetComponent<ColorCube>();
        if (cube == null || cube != targetCube) return;

        if (cube.isHeld) return;

        cubeOnPad = false;
        UpdateState();
    }

    void UpdateState()
    {
        if (cubeOnPad && !isSolved)
        {
            isSolved = true;
            SetVisible(true);
            SetColor(true);
            Debug.Log("Color pad solved.");

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.SetPuzzleSolved(1, true);
                PuzzleManager.Instance.ShowStatusMessage("Color pad solved", 2f);
            }
        }
        else if (!cubeOnPad && isSolved)
        {
            isSolved = false;
            SetVisible(false);
            SetColor(false);
            Debug.Log("Color pad reset.");

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.SetPuzzleSolved(1, false);
                PuzzleManager.Instance.ShowStatusMessage("Color pad reset", 2f);
            }
        }
    }

    public void NotifyCubeDropped()
    {
        if (!cubeOnPad)
        {
            Debug.Log("Cube dropped in wrong spot.");
            if (PuzzleManager.Instance != null)
                PuzzleManager.Instance.ShowStatusMessage("Wrong spot", 2f);
        }
    }

    // Called by PuzzleManager on full reset
    public void ResetPuzzle()
    {
        cubeOnPad = false;
        isSolved = false;
        SetVisible(false);
        SetColor(false);

        // Snap cube back to its start position
        if (targetCube != null)
            targetCube.SnapTo(targetCube.startPosition);
    }

    void SetVisible(bool visible)
    {
        if (padRenderer != null)
            padRenderer.enabled = visible;
    }

    void SetColor(bool active)
    {
        if (padRenderer != null)
            padRenderer.material.color = active ? activeColor : inactiveColor;
    }
}