using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Rotation")]
    public Vector3 closedRotation = Vector3.zero;           // local rotation when closed
    public Vector3 openRotation = new Vector3(0f, 90f, 0f); // local rotation when open
    public float openSpeed = 2f;

    bool isOpen = false;

    void Start()
    {
        // Make sure door starts closed
        transform.localEulerAngles = closedRotation;
    }

    void Update()
    {
        // Smoothly rotate towards target rotation
        Vector3 target = isOpen ? openRotation : closedRotation;
        transform.localEulerAngles = Vector3.Lerp(
            transform.localEulerAngles,
            target,
            Time.deltaTime * openSpeed
        );
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("DoorController: OpenDoor called");
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            Debug.Log("DoorController: CloseDoor called");
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}