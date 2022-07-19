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

    public void RequestSpawnBlob()
    {
        if (base.IsServer)
        {
            Vector3 pos = GetRandomPos();
            var clone = Instantiate(blobPrefab, pos, Quaternion.identity).gameObject;
        
            base.Spawn(clone, base.Owner);
        }
        else
        {
            SpawnBlob();   
        }
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void SpawnBlob()
    {
        Vector3 pos = GetRandomPos();
        var clone = Instantiate(blobPrefab, pos, Quaternion.identity).gameObject;
        
        base.Spawn(clone, base.Owner);
    }
}
