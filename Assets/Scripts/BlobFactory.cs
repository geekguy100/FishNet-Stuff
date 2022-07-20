using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlobFactory : NetworkBehaviour
{
    public static BlobFactory Instance { get; private set; }
    
    [SerializeField] private NetworkObject blobPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private static Vector3 GetRandomPos()
    {
        return Random.insideUnitCircle * Random.Range(-5f, 5f);
    }

    public void RequestSpawnBlob(NetworkConnection owner)
    {
        if (base.IsServer)
        {
            Vector3 pos = GetRandomPos();
            var clone = Instantiate(blobPrefab, pos, Quaternion.identity).gameObject;

            base.Spawn(clone, owner);
        }
        else
        {
            SpawnBlob(owner);  
        } 
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void SpawnBlob(NetworkConnection owner)
    {
        Vector3 pos = GetRandomPos();
        var clone = Instantiate(blobPrefab, pos, Quaternion.identity).gameObject;
        
        // Rotating current transform to random angle.
        float angle = Random.value * 360f;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        clone.transform.localRotation = rot;
        
        base.Spawn(clone, owner);
    }
}
