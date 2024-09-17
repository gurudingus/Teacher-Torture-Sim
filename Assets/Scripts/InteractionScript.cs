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

    // Update is called once per frame
    void Update()

        //make sure to make the event in the interaction script "Interact" and add IInteractable after monobehavior
    {
        if (Input.GetKeyDown(KeyCode.E))
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
}
