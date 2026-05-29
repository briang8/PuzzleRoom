using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 6f;
    public LayerMask interactLayer = ~0;

    ColorCube heldCube;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (playerCamera == null) return;

        // If already holding a cube, drop it directly without needing a raycast
        if (heldCube != null && heldCube.isHeld)
        {
            heldCube.Interact();
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();

                // Cache the cube reference if we just picked one up
                heldCube = hit.collider.GetComponent<ColorCube>();
            }
        }
    }
}