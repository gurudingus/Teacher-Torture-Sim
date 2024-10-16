using UnityEngine;

public class GlassCover : MonoBehaviour
{
    [SerializeField] private GameObject brokenGlass;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Breaker") return;
        Destroy(gameObject);
        Instantiate(brokenGlass, transform.parent, false);
    }
}
