using UnityEngine;

public class ColorCube : MonoBehaviour, IInteractable
{
    public Transform padPosition;
    public Transform offPosition;
    public bool isOnPad = false;
    public Color cubeColor = Color.red;

    public void Interact()
    {
        isOnPad = !isOnPad;

        if (isOnPad && padPosition != null)
        {
            transform.position = padPosition.position;
        }
        else if (!isOnPad && offPosition != null)
        {
            transform.position = offPosition.position;
        }

        Debug.Log($"ColorCube ({cubeColor}) isOnPad = {isOnPad}");
    }
}