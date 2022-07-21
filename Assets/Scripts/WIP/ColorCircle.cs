using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using KpattGames.Interaction;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorCircle : NetworkBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject prefab;

    [SyncVar(OnChange = nameof(OnColorChange))]
    private Color color;

    
    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            ChangeColor();
        }
    }

    public void PerformAction()
    {
        //ChangeColor();
        Duplicate();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeColor()
    {
        Color c = Random.ColorHSV();
        c.a = 1f;

        color = c;
    }

    [ServerRpc(RequireOwnership = false)]
    private void Duplicate()
    {
        Vector3 pos = Random.insideUnitCircle * Random.Range(-5f, 5f);
        GameObject clone = Instantiate(prefab, pos, Quaternion.identity);
        InstanceFinder.ServerManager.Spawn(clone, base.Owner);
    }

    private void OnColorChange(Color prev, Color next, bool asServer)
    {
        spriteRenderer.color = next;
    }
}
