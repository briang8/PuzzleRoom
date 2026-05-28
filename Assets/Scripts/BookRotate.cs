using UnityEngine;

public class BookRotate : MonoBehaviour, IInteractable
{
    public int bookIndex = 0;
    public float stepAngle = 90f;
    public int stepsCount = 4;
    public int targetStep = 1;

    int currentStep = 0;
    Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    public void Interact()
    {
        currentStep = (currentStep + 1) % stepsCount;

        float angle = currentStep * stepAngle;

        // Rotate relative to initial rotation
        transform.rotation = initialRotation * Quaternion.Euler(0f, angle, 0f);

        Debug.Log($"Book {bookIndex} rotated to step {currentStep} (angle {angle})");

        if (BookPuzzle.Instance != null)
        {
            BookPuzzle.Instance.OnBookRotated(bookIndex, currentStep);
        }
    }

    public bool IsAtTarget()
    {
        return currentStep == targetStep;
    }

    public void ResetBook()
    {
        currentStep = 0;
        transform.rotation = initialRotation;
    }
}