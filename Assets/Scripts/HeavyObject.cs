using UnityEngine;

public class HeavyObject : MonoBehaviour, IInteractable
{
    public Transform platePosition;
    public Transform startPosition;

    public bool isOnPlate = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (startPosition == null)
        {
            GameObject start = new GameObject(gameObject.name + "_Start");
            start.transform.position = transform.position;
            start.transform.rotation = transform.rotation;
            startPosition = start.transform;
        }

        SnapTo(startPosition);
        EnablePhysics(false);
    }

    public void Interact()
    {
        isOnPlate = !isOnPlate;

        if (isOnPlate && platePosition != null)
            SnapTo(platePosition);
        else if (startPosition != null)
            SnapTo(startPosition);

        EnablePhysics(false);
        Debug.Log($"HeavyObject isOnPlate = {isOnPlate}");
    }

    public void ResetPuzzle()
    {
        isOnPlate = false;
        SnapTo(startPosition);
        EnablePhysics(false);
    }

    void SnapTo(Transform target)
    {
        if (target == null) return;
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void EnablePhysics(bool enabled)
    {
        if (rb == null) return;
        rb.isKinematic = !enabled;
        rb.useGravity = enabled;
    }
}