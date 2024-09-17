using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class InteractionScript : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnComputerInteraction()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactOBJ))
            {
                interactOBJ.Interact();
            }
        }
    }
}
