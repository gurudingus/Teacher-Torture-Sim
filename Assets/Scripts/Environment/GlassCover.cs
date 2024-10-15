using UnityEngine;

public class GlassCover : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Breaker") Destroy(gameObject);
    }
}
