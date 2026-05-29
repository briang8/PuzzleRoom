using UnityEngine;

public class LightSwitchInteract : MonoBehaviour, IInteractable
{
    public int switchIndex = 0;
    public Light targetLight;
    public Renderer switchRenderer;
    public Color onColor = Color.green;
    public Color offColor = Color.red;

    bool isOn = true;

    void Start()
    {
        if (switchRenderer == null)
            switchRenderer = GetComponent<Renderer>();

        UpdateVisual();
    }

    public void Interact()
    {
        isOn = !isOn;

        if (targetLight != null)
            targetLight.enabled = isOn;

        UpdateVisual();

        if (LightPuzzle.Instance != null)
            LightPuzzle.Instance.OnSwitchToggled(switchIndex, isOn);

        Debug.Log($"Switch {switchIndex} toggled. isOn = {isOn}");
    }

    // Called by LightPuzzle.ResetPuzzle() — snaps switch back to ON visually and logically
    public void ResetSwitch()
    {
        isOn = true;

        if (targetLight != null)
            targetLight.enabled = true;

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (switchRenderer != null)
            switchRenderer.material.color = isOn ? onColor : offColor;
    }

    public bool IsOn() => isOn;
}