using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    public int buttonIndex = 0;
    public Renderer buttonRenderer;
    public Color pressColor = Color.yellow;
    Color originalColor;

    void Start()
    {
        if (buttonRenderer == null)
            buttonRenderer = GetComponent<Renderer>();
        if (buttonRenderer != null)
            originalColor = buttonRenderer.material.color;
    }

    public void Interact()
    {
        // Visual feedback
        if (buttonRenderer != null)
        {
            buttonRenderer.material.color = pressColor;
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.2f);
        }

        // Notify button puzzle
        if (ButtonPuzzle.Instance != null)
        {
            ButtonPuzzle.Instance.Press(buttonIndex);
        }

        Debug.Log($"Button {buttonIndex} pressed");
    }

    void ResetColor()
    {
        if (buttonRenderer != null)
            buttonRenderer.material.color = originalColor;
    }
}