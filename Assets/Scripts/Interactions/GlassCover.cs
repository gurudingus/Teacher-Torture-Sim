using UnityEngine;

public class GlassCover : MonoBehaviour
{
    //enables destruction of glass cover game object when hammer object touches it
    [SerializeField] private GameObject brokenGlass;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Breaker") return;
        Destroy(gameObject);
        Instantiate(brokenGlass, transform.parent, false);
    }
}
