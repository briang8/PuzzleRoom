using UnityEngine;

public class ColorPad : MonoBehaviour
{
    public Color cubeColorToAccept = Color.red;
    public Renderer padRenderer;
    public Color inactiveColor = Color.gray;
    public Color activeColor = Color.green;
    public Color inactiveEmission = Color.black;
    public Color activeEmission = Color.green;

    bool isComplete = false;

    void Start()
    {
        if (padRenderer == null)
            padRenderer = GetComponent<Renderer>();

        if (padRenderer != null)
        {
            padRenderer.material.color = inactiveColor;
            padRenderer.material.SetColor("_EmissionColor", inactiveEmission);
            DynamicGI.SetEmissive(padRenderer, inactiveEmission);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ColorCube cube = other.GetComponent<ColorCube>();
        if (cube != null && cube.cubeColor == cubeColorToAccept && cube.isOnPad)
        {
            if (!isComplete)
            {
                isComplete = true;
                SetActiveVisual(true);
                PuzzleManager.Instance.SetPuzzleSolved(1, true);
                Debug.Log("ColorPad puzzle solved");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        ColorCube cube = other.GetComponent<ColorCube>();
        if (cube != null && isComplete)
        {
            isComplete = false;
            SetActiveVisual(false);
            PuzzleManager.Instance.SetPuzzleSolved(1, false);
            Debug.Log("ColorPad puzzle no longer solved");
        }
    }

    void SetActiveVisual(bool active)
    {
        if (padRenderer == null) return;

        if (active)
        {
            padRenderer.material.color = activeColor;
            padRenderer.material.SetColor("_EmissionColor", activeEmission);
            DynamicGI.SetEmissive(padRenderer, activeEmission);
        }
        else
        {
            padRenderer.material.color = inactiveColor;
            padRenderer.material.SetColor("_EmissionColor", inactiveEmission);
            DynamicGI.SetEmissive(padRenderer, inactiveEmission);
        }
    }
}