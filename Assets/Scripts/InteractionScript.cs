using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
}

[RequireComponent(typeof(PlayerInput))] public class InteractionScript : MonoBehaviour
{
    /*
    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnInteraction()
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
    */

    [SerializeField] private Transform interactorSource;
    [SerializeField] [Tooltip("Overrides the interactor source to automatically select the camera attached to this gameobject")] private bool useCameraAsSource = true;
    [SerializeField] private float interactRange = 2.5f;

    private void Awake()
    {
        if (!useCameraAsSource) return;
        
        Camera camera = GetComponentInChildren<Camera>();
        if (camera != null) interactorSource = camera.transform;
    }

    //Feel free to delete this. I rewrote this because I thought that the old code had some issue but no, this is just a more concise rewrite of the code.
    private void OnInteraction()
    {
        if (!Physics.Raycast(interactorSource.position, interactorSource.forward, out RaycastHit rayHit, interactRange, ~(1 << 6))) return; //Return if it hits nothing. Also has a layermask exclusion so that it doesn't pick up the player and cancel interactions

        rayHit.transform.gameObject.GetComponentInChildren<IInteractable>()?.Interact(); //? operator to reduce verbosity. If it has an IInteractable implementing object, interact with it.
    }
}
