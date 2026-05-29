using UnityEngine;

public class ColorCube : MonoBehaviour, IInteractable
{
    public Transform startPosition;
    public Transform holdPoint;

    [Header("Movement")]
    public float moveSpeed = 15f;

    public bool isHeld = false;
    public Color cubeColor = Color.red;

    Rigidbody rb;
    Collider col;
    ColorPad pad;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        pad = FindFirstObjectByType<ColorPad>();

        if (startPosition == null)
        {
            GameObject start = new GameObject(gameObject.name + "_Start");
            start.transform.position = transform.position;
            start.transform.rotation = transform.rotation;
            startPosition = start.transform;
        }

        SnapTo(startPosition);
        SetPhysics(false);
    }

    void Update()
    {
        if (isHeld && holdPoint != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                holdPoint.position,
                Time.deltaTime * moveSpeed
            );
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                holdPoint.rotation,
                Time.deltaTime * moveSpeed
            );
        }
    }

    public void Interact()
    {
        if (!isHeld)
            PickUp();
        else
            DropHere();

        Debug.Log($"ColorCube ({cubeColor}) isHeld = {isHeld}");
    }

    void PickUp()
    {
        isHeld = true;
        SetPhysics(false);
        SetCollider(false);
    }

    void DropHere()
    {
        isHeld = false;
        SetCollider(true);
        SetPhysics(true);

        if (pad != null)
            pad.NotifyCubeDropped();
    }

    public void SnapTo(Transform target)
    {
        if (target == null) return;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void SetPhysics(bool enablePhysics)
    {
        if (rb == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = !enablePhysics;
        rb.useGravity = enablePhysics;
    }

    void SetCollider(bool enabled)
    {
        if (col == null) return;
        col.enabled = enabled;
    }
}