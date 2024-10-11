using UnityEngine;

public class ItemOutOfBounds : MonoBehaviour {
    [SerializeField] private Vector3 itemSpawnLocation = new(4f, 8f, 0f);
    private Vector3 ItemSpawnLocation => transform.rotation * itemSpawnLocation + transform.position; //I would use transform.localToWorldMatrix if it weren't affected by scale
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private int numberOfItemsToSpawn = 2;

    [SerializeField] private int maxNumberSpawned = 2500;
    private int numberSpawned = 0;

    private void Awake()
    {
        if (itemToSpawn == null) Debug.LogWarning("No item set for the killplane");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 3) return; //Only allow objects on layer 3 to trigger the collision box

        Destroy(other.gameObject);
        numberSpawned += numberOfItemsToSpawn;

        if (numberSpawned < maxNumberSpawned) for (int i = 0; i < numberOfItemsToSpawn; i++) Instantiate(itemToSpawn, ItemSpawnLocation, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() => Gizmos.DrawSphere(ItemSpawnLocation, 0.5f);
}
